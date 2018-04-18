using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.IO;
using AssemblyCSharp;
using UnityEngine.SceneManagement;
using System.Linq;

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

	// Use this for initialization
	void Start () 
	{
		
		// get a reference to the different screens available
		welcomeScreen = GameObject.Find("StartingScreen");
		newGameScreen = GameObject.Find ("NewGameScreen");
		continueGameScreen = GameObject.Find ("ContinueGameScreen");

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

	/// <summary>
	/// Use to keep track of the file name the user selects in the
	/// dropdown menu.
	/// </summary>
	void Update()
	{
		//Keep the current index of the Dropdown in a variable
		dropdownValue = dropdown.value;
		//Change the message to say the name of the current Dropdown selection using the value
		continueFromFilename = dropdown.options[dropdownValue].text;
	}

	/// <summary>
	/// Prompt user for name to save game under
	/// </summary>
	private void StartNewGame() 
	{
		
		// hide UI from other screens
		welcomeScreen.SetActive (false);
		continueGameScreen.SetActive (false);

		// show UI to make a new save file 
		newGameScreen.SetActive (true);
		backButton.gameObject.SetActive (true);

	}

	/// <summary>
	/// Set up screen to have user choose which game to continue
	/// </summary>
	public void ChooseContinueGame() 
	{
		// get access to dropdown menu of saved games
		dropdown = dropdownGO.GetComponent<Dropdown> ();

		// set the dropdown menu to hold all the file names for saved games
		dropdown.ClearOptions (); 


		List<string> filenames = Directory.GetFiles(Application.persistentDataPath)
			.Select(file => Path.GetFileName(file))
			.Where(file => file.Contains(".gd"))
			.ToList();

		// remove extension for display
		for (int i = 0; i < filenames.Count; i++) 
		{
			filenames[i] = filenames[i].Replace (".gd", "");
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

	/// <summary>
	/// Loads the file selected from the dropdown menu and loads 
	/// the main area scene
	/// </summary>
	private void ContinueGame() 
	{
		GlobalControl.Load (continueFromFilename);
		SceneManager.LoadScene("Main Area");
	}

	/// <summary>
	/// Confirm valid game name and then start the game
	/// </summary>
	private void SaveAndContinue() 
	{

		GameObject tmp = GameObject.Find("ErrorMessage");
		Text errorMsg = tmp.GetComponent<Text> ();
		string name = userSaveNameInput.text;

		// make sure there are no unacceptable characters in user input
		if (isValidGameName(name)) {
			
			string path = Application.persistentDataPath + "/" + name + ".gd";

			// checks if there is a file with the save name already in the directory
			if (!File.Exists (path))
			{
				// start a new game in case player just exited a different game
				GlobalControl.Instance.savedGameData = new Game ();

				// saves file and loads the main area to begin playing
				GlobalControl.Save (name);
				SceneManager.LoadScene("Main Area");
			}
			else
			{
				errorMsg.text = "A file already exists with this name.";
			}
		}
		else {
			// display error message to user
			errorMsg.text = "Sorry, that file name can not be used. Try again.";
		}
	}

	/// <summary>
	/// Set up screen for the main menu
	/// </summary>
	private void GoBack() 
	{
		
		// turns on UI objects for the welcome screen
		welcomeScreen.SetActive (true);

		// turns off all other UI elements
		backButton.gameObject.SetActive (false);
		newGameScreen.SetActive (false);
		continueGameScreen.SetActive (false);
	}

	/// <summary>
	/// Determines if the name submitted is a valid game name
	/// in terms of characters used.
	/// </summary>
	/// <returns>true if valid game name was used, false otherwise.</returns>
	/// <param name="name">the name of the game being created</param>
	private bool isValidGameName (string name) 
	{
		string pattern = "^[\\w ]+$";
		Regex regex = new Regex (pattern);
		return regex.IsMatch (name);
	}

}
