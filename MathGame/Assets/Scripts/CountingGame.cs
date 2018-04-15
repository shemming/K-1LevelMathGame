using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using UnityEngine.SceneManagement;


public class CountingGame : MonoBehaviour {


	#region Variable Declararion

	// displays what the user has currently typed
	public Text userInput;

	// triggers evaluation of what the user has entered
	public Button enterButton;

	// displays the current score
	public Text score;

	// brings player back to the main area
	public Button exitButton;

	// Holds an array of all the flower game objects on screen
	private GameObject[] flowers;

	// Holds logic for the number player has to figure out
	private Counter counter;

	// holds input field component to get information on focus
	// and set the visible text
	private InputField InputFieldCO;

	// holds whether the input field was in focus the previous frame
	// allows user to press enter to submit their answer
	private bool isFocused;

	// used to update game information
	public GameObject gameStatsGO;
	private GlobalControl gameStats;
	private MiniGame countingGame;

	// name of user input game object
	private const string INPUT = "userInput";

	// Generates random numbers
	private System.Random rnd;
	#endregion

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {
		
		// get access to saved addition game info to update
		gameStats = gameStatsGO.GetComponent<GlobalControl> ();
		countingGame = gameStats.savedGameData.counting;

		// get the input field as a game object and an input field object
		GameObject inputFieldGO = GameObject.Find (INPUT);
		InputFieldCO = inputFieldGO.GetComponent<InputField> ();


		isFocused = false;

		// add a listener for when user clicks enter button
		enterButton
			.onClick
			.AddListener (CheckAnswer);

		// add a listener for when user clicks exit button
		exitButton
			.onClick
			.AddListener (ExitGame);
		
		rnd = new System.Random ();

		//gets an array of all the flowers in the scene
		flowers = GameObject.FindGameObjectsWithTag ("Flower");


		counter = new Counter (level: countingGame.level);

		// sets up the scene
		score.text = countingGame.correctAnswers.ToString();
		TurnOnFlowers ();
	}

	/// <summary>
	/// Update is called once per frame, checks user input
	/// </summary>
	void Update () {

		// if the user presses enter, take that as if they clicked the enter button
		// check if the answer is correct
		if (Input.GetKeyDown (KeyCode.Return) && isFocused)
		{
			CheckAnswer ();
		} 
		else 
		{
			// update the number being displayed in the input box to reflect
			// what the user has entered into it
			userInput.text = InputFieldCO.text;
		}

		// keep tabs of if input box is in focus or not
		// needed to register if input box is in focus and user clicks enter
		// because when the user hits enter it immediately goes out of focus
		if (InputFieldCO.isFocused)
		{
			isFocused = true;
		}
		else
		{
			isFocused = false;
		}
	}

	/// <summary>
	/// Makes random flowers on screen visible to the user.
	/// The number of flowers made visible equals the number
	/// that the user is supposed to guess.
	/// </summary>
	private void TurnOnFlowers() 
	{
		// makes all the flowers in the scene invisible to start with a blank slate
		foreach (GameObject flower in flowers)
		{
			flower.SetActive (false);
		}

		Debug.Log ("Num to guess: " + counter.Num);
		// show the same number of flowers that the user is supposed to guess
		for (int i = 0; i < counter.Num; i++)
		{
			int index;

			// find the index of a random flower that has not yet been set to active (visible)
			do
			{
				index = rnd.Next (0, flowers.Length);
			}
			while(flowers [index].activeSelf);

			// make that flower active (visible)
			flowers [index].SetActive (true);

		}

	}

	/// <summary>
	/// Checks the answer given by the user.
	/// If correct, a new number to be counted is generated
	/// and flowers on screen are updated to match
	/// </summary>
	private void CheckAnswer() {
		// get the number entered by the user.
		// don't really need to validate it's a number because the input
		// field only allows integers
		int input;
		int.TryParse(userInput.text, out input);

		if (input == counter.Num) 
		{ 
			// user answered correctly
			countingGame.correctAnswers++;

			if (countingGame.correctAnswers % 10 == 0)
			{
				counter.IncreaseLevel ();
				countingGame.level = counter.Level;
			}

			score.text = countingGame.correctAnswers.ToString();

			InputFieldCO.text = string.Empty;
			InputFieldCO.ActivateInputField();

			counter.GenerateNewNumber ();
			TurnOnFlowers ();
		} 
		else 
		{ 
			// user answered incorrectly
			InputFieldCO.ActivateInputField();
		}
	}

	void ExitGame() 
	{
		gameStats.SavePlayer ();
		SceneManager.LoadScene("Main Area");
	}
}
