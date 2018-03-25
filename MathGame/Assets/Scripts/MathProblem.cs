using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;


public class MathProblem : MonoBehaviour {

	#region Variable Declararion
	public Text mathProblem, userInput;
	public Button enterButton;
	public Text score;
	private MathEquation equation;
	private const string INPUT = "userInput";
	private GameObject inputFieldGO;
	private InputField InputFieldCO;
	private bool isFocused;
	private int correctAnswers;
	private int level;
	private int increaseRange;
	Animator ani;
	#endregion

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start ()
	{
		// add a listener for when user clicks enter button
		enterButton
			.onClick
			.AddListener (CheckAnswer);

		// get the input field as a game object and an input field object
		inputFieldGO = GameObject.Find (INPUT);
		InputFieldCO = inputFieldGO.GetComponent<InputField> ();

		isFocused = false;

		correctAnswers = 0;
		level = 1;

		score.text = correctAnswers.ToString();
		increaseRange = 10;

		// get the first math equation and set the text
		equation = new MathEquation (increaseRange, level, MathEquation.EquationType.Addition);
		mathProblem.text = equation.Num1 + " + " + equation.Num2 + " = ";

		ani = GetComponent<Animator> ();
	}

	/// <summary>
	/// Update is called once per frame, checks user input
	/// </summary>
	void Update () 
	{
		inputFieldGO = GameObject.Find (INPUT);

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
	/// Checks the answer to the math problem given by the user.
	/// If correct, a new math problem is generated.
	/// </summary>
	void CheckAnswer() 
	{

		// get the number entered by the user.
		// don't really need to validate it's a number because the input
		// field only allows integers
		int input;
		int.TryParse(InputFieldCO.text, out input);

		if (input == equation.Sum) 
		{ // user answered correctly
			
			// play animation of treasure chest opening
			ani.Play ("ChestAnimation", -1, 0f);

			// generate a new math problem & update display
			correctAnswers++;
			equation.GenerateNewEquation ();
			mathProblem.text = equation.Num1 + " + " + equation.Num2 + " = ";
			InputFieldCO.text = string.Empty;
			InputFieldCO.ActivateInputField();

			if (correctAnswers % 10 == 0)
			{
				equation.IncreaseLevel ();
			}

			score.text = correctAnswers.ToString();
		} 
		else 
		{ // user answered incorrectly
			InputFieldCO.ActivateInputField();
		}

	}

}
