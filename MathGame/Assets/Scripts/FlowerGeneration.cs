using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;


public class FlowerGeneration : MonoBehaviour {


	#region Variable Declararion
	public GameObject input;
	public Text userInput;
	public Button enterButton;
	public Text score;

	private GameObject[] flowers;
	private Counter counter;
	private int correctAnswers;
	private int level;

	private GameObject inputFieldGO;
	private InputField InputFieldCO;
	private bool isFocused;
	#endregion


	/// <summary>
	/// Generates random numbers
	/// </summary>
	private System.Random rnd;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {

		InputFieldCO = input.GetComponent<InputField>();
		isFocused = false;

		// add a listener for when user clicks enter button
		enterButton
			.onClick
			.AddListener (CheckAnswer);
		
		rnd = new System.Random ();

		//gets an array of all the flowers in the scene
		flowers = GameObject.FindGameObjectsWithTag ("Flower");

		correctAnswers = 0;
		level = 1;

		counter = new Counter (level: level);

		// sets up the scene
		score.text = correctAnswers.ToString();
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
			correctAnswers++;

			if (correctAnswers % 10 == 0)
			{
				counter.IncreaseLevel ();
			}

			score.text = correctAnswers.ToString();

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
}
