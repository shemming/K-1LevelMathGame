using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainAreaMenu : MonoBehaviour {
	
	public Button saveQuitButton;
	public Button incentivesButton;
	public Button challengeButton;
	public Button instructionsButton;
	public GlobalControl gameManager;
	public GameObject instructions;
	public GameObject incentives;
	public GameObject blackOutSheet;
	public Text scores;

	// Use this for initialization
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
}
