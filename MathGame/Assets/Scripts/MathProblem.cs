using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MathProblem : MonoBehaviour {

	public Text mathProblem, userInput;
	private int num1, num2, sum;
	private System.Random rnd;
	public Button enterButton;

	// Use this for initialization
	void Start ()
	{
		// add a listener for when user clicks enter button
		enterButton
			.onClick
			.AddListener (TaskOnClick);

		// randomly generate 2 numbers between 0-10 for the math problem
		rnd = new System.Random ();
		num1 = rnd.Next (0, 10);
		num2 = rnd.Next (0, 10);
		sum = num1 + num2;
		mathProblem.text = num1 + " + " + num2 + " = ";
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject inputFieldGO = GameObject.Find ("userInput");
		InputField InputFieldCO = inputFieldGO.GetComponent<InputField> ();
		userInput.text = InputFieldCO.text;
	}

	void TaskOnClick() 
	{
		int input;

		GameObject inputFieldGO = GameObject.Find ("userInput");
		InputField InputFieldCO = inputFieldGO.GetComponent<InputField> ();
		int.TryParse(InputFieldCO.text, out input);

		if (input == sum) 
		{
			Debug.Log ("success");
			num1 = rnd.Next (0, 10);
			num2 = rnd.Next (0, 10);
			sum = num1 + num2;
			mathProblem.text = num1 + " + " + num2 + " = ";
		} 
		else 
		{
			Debug.Log ("failure");
		}

	}
}
