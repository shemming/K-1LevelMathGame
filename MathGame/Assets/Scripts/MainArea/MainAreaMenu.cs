using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainAreaMenu : MonoBehaviour {

	// the buttons involved in the main menu
	public Button saveQuitButton;
	public Button incentivesButton;
	public Button challengeButton;
	public Button instructionsButton;
	public Button resetButton;

	// objects affected by choices in the main menu
	public GlobalControl gameManager;
	public GameObject instructions;
	public GameObject incentives;
	public GameObject blackOutSheet;
	public GameObject resetResultScreen;
	public Text scores;

	// used to allow chest animation on answer submission
	public GameObject incentiveDisplay;
	private MainAreaDesign incentiveDisplayScript;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {
		
		saveQuitButton
			.onClick
			.AddListener (SaveQuit);

		incentivesButton
			.onClick
			.AddListener (DisplayIncentives);

		challengeButton
			.onClick
			.AddListener (Challenge);

		instructionsButton
			.onClick
			.AddListener (DisplayInstructions);

		// get access to script on that displays the earned animations
		incentiveDisplayScript = incentiveDisplay.GetComponent<MainAreaDesign> ();

	}

	/// <summary>
	/// If the player has completed all levels of all games and has reset their progress
	/// under 10 times, the restart button should not be shown. Otherwise it is.
	/// </summary>
	public void CheckProgress()
	{
		if (!gameManager.isGameComplete() || gameManager.savedGameData.gamesCompleted >= 10)
		{
			resetButton.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// Saves user's progress and returns to the main menu
	/// </summary>
	private void SaveQuit() 
	{
		// if they haven't made any progess, have instructions show when
		// they come back to play
		if (!gameManager.isGameStarted ())
		{
			gameManager.savedGameData.instructionsShown = false;
		}
		gameManager.SavePlayer();
		GlobalControl.Save ();
		SceneManager.LoadScene (Constants.SceneNames.MAIN_MENU);
	}

	/// <summary>
	/// Goes to the main menu of the timed challenges
	/// </summary>
	private void Challenge() 
	{
		gameManager.SavePlayer ();
		GlobalControl.Save ();
		SceneManager.LoadScene (Constants.SceneNames.CHALLENGE);
	}

	/// <summary>
	/// Shows user's progress in all games
	/// </summary>
	private void DisplayIncentives() 
	{
		// display scores and gray out the scene behind 
		incentives.SetActive (true);
		blackOutSheet.SetActive (true);
		SetStoryText ();

		// prevent user from moving around the player while instructions are shown
		GameObject varGameObject = GameObject.FindWithTag(Constants.PLAYER);
		varGameObject.GetComponent<PlayerController>().enabled = false;
	}

	/// <summary>
	/// Shows instructions if user wants to refer back to them
	/// </summary>
	private void DisplayInstructions() 
	{
		// display instructions and gray out the scene behind 
		instructions.SetActive (true);
		blackOutSheet.SetActive (true);

		// prevent user from moving around the player while instructions are shown
		GameObject varGameObject = GameObject.FindWithTag(Constants.PLAYER);
		varGameObject.GetComponent<PlayerController>().enabled = false;
	}

	/// <summary>
	/// Set displayed text to show the mini game progress
	/// </summary>
	public void SetStoryText() 
	{
		string result = "Addition: \n\tLevel: ";
		result += gameManager.savedGameData.addition.level + "\n\tCorrect Answers: ";
		result += gameManager.savedGameData.addition.correctAnswers;

		result += "\n\nSubtraction: \n\tLevel: ";
		result += gameManager.savedGameData.subtraction.level + "\n\tCorrect Answers: ";
		result += gameManager.savedGameData.subtraction.correctAnswers;

		result += "\n\nCounting: \n\tLevel: ";
		result += gameManager.savedGameData.counting.level + "\n\tCorrect Answers: ";
		result += gameManager.savedGameData.counting.correctAnswers;

		scores.text = result;
	}

	/// <summary>
	/// Set displayed text to show the timed challenge progress
	/// </summary>
	public void SetTimedText() 
	{
		string result = "Addition High Scores: \n\tLevel 1: ";
		result += gameManager.savedGameData.additionChallenge.l1HighScore + "\n\tLevel 2: ";
		result += gameManager.savedGameData.additionChallenge.l2HighScore + "\n\tLevel 3: ";
		result += gameManager.savedGameData.additionChallenge.l3HighScore;

		result += "\n\nSubtraction High Scores: \n\tLevel 1: ";
		result += gameManager.savedGameData.subtractionChallenge.l1HighScore + "\n\tLevel 2: ";
		result += gameManager.savedGameData.subtractionChallenge.l2HighScore + "\n\tLevel 3: ";
		result += gameManager.savedGameData.subtractionChallenge.l3HighScore;

		scores.text = result;
	}

	/// <summary>
	/// Either resets game or allows user to back out of resetting game
	/// </summary>
	/// <param name="response">user's response to resetting the game. controlled by button input</param>
	public void ResetGame(string response) 
	{
		// make the prompt pop up go away
		GameObject promptScreen = GameObject.Find ("ConfirmResetScreen");
		promptScreen.SetActive (false);

		// reset progress or leave as is depending on the user's response
		if (response.ToLower () == "yes")
		{
			// make the number of correct answers of all mini games 0,
			// all levels of all mini games zero
			gameManager.savedGameData.addition.correctAnswers = 0;
			gameManager.savedGameData.addition.level = 1;

			gameManager.savedGameData.subtraction.correctAnswers = 0;
			gameManager.savedGameData.subtraction.level = 1;

			gameManager.savedGameData.counting.correctAnswers = 0;
			gameManager.savedGameData.counting.level = 1;

			gameManager.savedGameData.equality.correctAnswers = 0;
			gameManager.savedGameData.equality.level = 1;

			// increase the count of number of games completed
			gameManager.savedGameData.gamesCompleted++;

			// save the reset
			gameManager.SavePlayer ();
			GlobalControl.Save ();

			// tell user that their data has been reset
			Text result = resetResultScreen.GetComponentInChildren<Text> ();
			result.text = "Your game has been reset! You have been awarded a star for starting a new game.";

			// clears any incentives visible in the main area and adds a star for a completed game
			incentiveDisplayScript.SetExtras ();

			resetResultScreen.SetActive(true);
		}
		else
		{
			// display message that no changes have been made to the save data
			Text result = resetResultScreen.GetComponentInChildren<Text> ();
			result.text = "Your progress has not been changed.";
			resetResultScreen.SetActive(true);
		}
	}
}
