using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Script that runs the addition mini game. Attached to the canvas object.
/// </summary>
public class AdditionProblem : MonoBehaviour {
	
	#region Variable Declaration
	//displays the current math problem
	public Text mathProblem;

	// displays what the user has currently typed
	public Text userInput;

	// triggers evaluation of what the user has entered
	public Button enterButton;

	// brings player back to the main area
	public Button exitButton;

	// displays the current score
	public Text score;

	// holds logic for the math equations player is to solve
	private MathEquation equation;

	// holds input field component to get information on focus
	// and set the visible text
	private InputField InputFieldCO;

	// holds whether the input field was in focus the previous frame
	// allows user to press enter to submit their answer
	private bool isFocused;

	// used to update game information
	public GameObject gameStatsGO;
	private GlobalControl gameStats;
	private MiniGame additionGame;

	// used to allow coin animation on correct answers
	public GameObject coinGO;
	private PlayAnimation coinScript;

	// used to allow chest animation on answer submission
	public GameObject chestGO;
	private PlayAnimation chestScript;
	#endregion

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
	/// 2/12/18
	/// </date>
	void Start ()
	{
		
		// get access to script on Chest and Coin object to play it's animation
		coinScript = coinGO.GetComponent<PlayAnimation> ();
		chestScript = chestGO.GetComponent<PlayAnimation> ();

		// get access to saved addition game info to update
		gameStats = gameStatsGO.GetComponent<GlobalControl> ();
		additionGame = gameStats.savedGameData.addition;

		// add a listener for when user clicks enter button
		enterButton
			.onClick
			.AddListener (CheckAnswer);

		// add a listener for when user clicks exit button
		exitButton
			.onClick
			.AddListener (ExitGame);

		// get the input field as a game object and an input field object
		GameObject inputFieldGO = GameObject.Find (Constants.INPUT);
		InputFieldCO = inputFieldGO.GetComponent<InputField> ();

		isFocused = false;


		score.text = additionGame.correctAnswers.ToString();

		// get the first math equation and set the text
		equation = new MathEquation (additionGame.increaseRange, additionGame.level, MathEquation.EquationType.Addition);
		mathProblem.text = equation.EquationString;
	}

	/// <name>
	/// Update
	/// </name>
	/// <summary>
	/// Update is called once per frame, checks user input
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 2/12/18
	/// </date>
	void Update () 
	{

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

	/// <name>
	/// CheckAnswer
	/// </name>
	/// <summary>
	/// Checks the answer to the math problem given by the user.
	/// If correct, a new math problem is generated.
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 3/2/18
	/// </date>
	void CheckAnswer() 
	{

		// get the number entered by the user.
		// don't really need to validate it's a number because the input
		// field only allows integers
		int input;
		int.TryParse(InputFieldCO.text, out input);

		if (input == equation.Sum) 
		{ 
			// user answered correctly

			// play animation of treasure chest opening & coin going into scor
			chestScript.Animate(Constants.Addition.CHEST_OPEN_ANIMATION);

			// play animation of coin going into score - wait until animation finishes
			StartCoroutine(coinScript.AnimateAndWait (Constants.Addition.COIN_EARNED_ANIMATION));

			// generate a new math problem & update display
			additionGame.correctAnswers++;
			equation.GenerateNewEquation ();
			mathProblem.text = equation.EquationString;
			InputFieldCO.text = string.Empty;
			InputFieldCO.ActivateInputField();

			// if player answers 10 questions right, they move to the next level
			if (additionGame.correctAnswers % 10 == 0)
			{
				
				equation.IncreaseLevel ();
				additionGame.level = equation.Level;
			}

			// update score on the screen
			score.text = additionGame.correctAnswers.ToString();
		} 
		else 
		{ 
			// user answered incorrectly
			InputFieldCO.ActivateInputField();
			chestScript.Animate (Constants.Addition.CHEST_LOCKED_ANIMATION);
		}
	}

	/// <name>
	/// ExitGame
	/// </name>
	/// <summary>
	/// Save game data and return to the welcome screen of the main menu
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/20/18
	/// </date>
	void ExitGame() 
	{
		gameStats.SavePlayer ();
		SceneManager.LoadScene(Constants.SceneNames.MAIN_AREA);
	}
}
