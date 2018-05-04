using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Script attached to canvas object in the main menu scene
/// </summary>
public class MainMenu : MonoBehaviour 
{

	// brings user to ContinueGameScreen to choose which instance to play
	public Button chooseContinueButton;

	// brings user to NewGameScreen to name new game instance
	public Button newGameButton;

	// input field for save name of new game
	public InputField userSaveNameInput;

	// submits name for new save game
	public Button submitNewGameButton;

	// brings player back to welcome screen
	public Button backButton;

	// triggers the loading of the file selected from dropdown menu and loads main area
	public Button continueButton;

	// gives access to the dropdown menu to see what file the user wants to load
	public GameObject dropdownGO;
	private Dropdown dropdown;
	private int dropdownValue;
	private string continueFromFilename;

	// holds the different screens available to easily turn on and off
	private GameObject newGameScreen;
	private GameObject welcomeScreen;
	private GameObject continueGameScreen;

	/// <name>
	/// Start
	/// </name>
	/// <summary>
	/// Use this for initialization
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/16/18
	/// </date>
	void Start () 
	{
		
		// get a reference to the different screens available
		welcomeScreen = GameObject.Find(Constants.MainMenu.Views.WELCOME_SCREEN);
		newGameScreen = GameObject.Find (Constants.MainMenu.Views.NEW_SCREEN);
		continueGameScreen = GameObject.Find (Constants.MainMenu.Views.CONTINUE_SCREEN);

		// only have continue button enabled if there are games to continue
		SetContinueButton ();

		// have only the welcome screen visible when starting
		backButton.gameObject.SetActive (false);
		continueGameScreen.SetActive (false);
		newGameScreen.SetActive (false);

		dropdown = dropdownGO.GetComponent<Dropdown>();

		// add a listener for when user wants to continue a game
		chooseContinueButton
			.onClick
			.AddListener (ChooseContinueGame);

		// add a listener for when user wants to start a new game
		newGameButton
			.onClick
			.AddListener (StartNewGame);

		// add a listener for when user wants to save a new game slot
		submitNewGameButton
			.onClick
			.AddListener (SaveAndContinue);

		// add a listener for when user wants to continue a game
		backButton
			.onClick
			.AddListener (GoBack);

		// add a listener for when user chooses a game to continue
		continueButton
			.onClick
			.AddListener (ContinueGame);
	}

	/// <name>
	/// Update
	/// </name>
	/// <summary>
	/// Use to keep track of the file name the user selects in the
	/// dropdown menu.
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/16/18
	/// </date>
	void Update()
	{
		//Keep the current index of the Dropdown in a variable
		dropdownValue = dropdown.value;
		//Change the message to say the name of the current Dropdown selection using the value
		continueFromFilename = dropdown.options[dropdownValue].text;

		if (userSaveNameInput.text == "")
		{
			submitNewGameButton.interactable = false;
		}
		else
		{
			submitNewGameButton.interactable = true;
		}
	}

	/// <name>
	/// StartNewGame
	/// </name>
	/// <summary>
	/// Prompt user for name to save game under
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/16/18
	/// </date>
	private void StartNewGame() 
	{
		
		// hide UI from other screens
		welcomeScreen.SetActive (false);
		continueGameScreen.SetActive (false);

		// show UI to make a new save file 
		newGameScreen.SetActive (true);
		backButton.gameObject.SetActive (true);

	}

	/// <name>
	/// SetContinueButton
	/// </name>
	/// <summary>
	/// Only have the continue button clickable if there are games to continue playing
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 5/14/18
	/// </date>
	public void SetContinueButton()
	{
		List<string> filenames = Directory.GetFiles(Application.persistentDataPath)
			.Select(file => Path.GetFileName(file))
			.Where(file => file.Contains(Constants.MainMenu.FILE_EXTENSION))
			.SkipWhile(file => file == "")
			.ToList();
		if (filenames.Count < 1)
		{
			chooseContinueButton.interactable = false;
		}
		else
		{
			chooseContinueButton.interactable = true;
		}
	}

	/// <name>
	/// ChooseContinueGame
	/// </name>
	/// <summary>
	/// Set up screen to have user choose which game to continue
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/16/18
	/// </date>
	public void ChooseContinueGame() 
	{
		// get access to dropdown menu of saved games
		dropdown = dropdownGO.GetComponent<Dropdown> ();

		// set the dropdown menu to hold all the file names for saved games
		dropdown.ClearOptions (); 

		Debug.Log (Application.persistentDataPath);
		List<string> filenames = Directory.GetFiles(Application.persistentDataPath)
			.Select(file => Path.GetFileName(file))
			.Where(file => file.Contains(Constants.MainMenu.FILE_EXTENSION))
			.SkipWhile(file => file == "")
			.ToList();

		// remove extension for display
		for (int i = 0; i < filenames.Count; i++) 
		{
			filenames[i] = filenames[i].Replace (Constants.MainMenu.FILE_EXTENSION, "");
		}

		// add all the file names to the dropdown menu
		dropdown.AddOptions (filenames);


		// hide UI from other screens
		welcomeScreen.SetActive (false);
		newGameScreen.SetActive (false);

		// show UI to continue a game
		continueGameScreen.SetActive (true);
		backButton.gameObject.SetActive (true);
	}

	/// <name>
	/// ContinueGame
	/// </name>
	/// <summary>
	/// Loads the file selected from the dropdown menu and loads 
	/// the main area scene
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/16/18
	/// </date>
	private void ContinueGame() 
	{
		GlobalControl.Load (continueFromFilename);
		SceneManager.LoadScene(Constants.SceneNames.MAIN_AREA);
	}

	/// <name>
	/// SaveAndContinue
	/// </name>
	/// <summary>
	/// Confirm valid game name and then start the game
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/16/18
	/// </date>
	private void SaveAndContinue() 
	{

		Text errorMsg = GameObject.Find(Constants.MainMenu.ERROR_TEXT_GO).GetComponent<Text>();
		string filename = userSaveNameInput.text;

		// make sure there are no unacceptable characters in user input
		if (IsValidGameName(filename)) {
			
			string path = Application.persistentDataPath + "/" + filename + Constants.MainMenu.FILE_EXTENSION;

			// checks if there is a file with the save name already in the directory
			if (!File.Exists (path))
			{
				// start a new game in case player just exited a different game
				GlobalControl.Instance.savedGameData = new Game ();

				// saves file and loads the main area to begin playing
				GlobalControl.Save (filename);
				SceneManager.LoadScene(Constants.SceneNames.MAIN_AREA);
			}
			else
			{
				errorMsg.text = Constants.MainMenu.Error.EXISTS;
			}
		}
		else {
			// display error message to user
			errorMsg.text = Constants.MainMenu.Error.GENERIC;
		}
	}

	/// <name>
	/// GoBack
	/// </name>
	/// <summary>
	/// Set up screen for the main menu
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/16/18
	/// </date>
	private void GoBack() 
	{
		
		// turns on UI objects for the welcome screen
		welcomeScreen.SetActive (true);
		SetContinueButton ();

		// turns off all other UI elements
		backButton.gameObject.SetActive (false);
		newGameScreen.SetActive (false);
		continueGameScreen.SetActive (false);
	}

	/// <name>
	/// IsValidGameName
	/// </name>
	/// <summary>
	/// Determines if the name submitted is a valid game name
	/// in terms of characters used.
	/// </summary>
	/// <returns>true if valid game name was used, false otherwise.</returns>
	/// <param name="name">the name of the game being created</param>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/17/18
	/// </date>
	private bool IsValidGameName (string name) 
	{
		string pattern = "^[\\w ]+$";
		Regex regex = new Regex (pattern);
		return regex.IsMatch (name);
	}

}
