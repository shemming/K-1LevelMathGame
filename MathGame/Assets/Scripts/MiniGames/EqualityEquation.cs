using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates new equality equations and any logic that goes with it
/// </summary>
public class EqualityEquation {

	/// <summary>
	/// Determines what to do with the equation
	/// </summary>
	public enum EqualityType {
		GreaterThan,
		LessThan,
		EqualTo
	}

	/// <summary>
	/// Number used in equality equation
	/// </summary>
	private int num1, num2;

	private EqualityType sign;

	/// <summary>
	/// Generates random numbers
	/// </summary>
	private System.Random rnd;

	/// <summary>
	/// Used to determine the difficulty of the equation.
	/// </summary>
	private int level, increaseRange;

	/// <summary>
	/// The max level that the game can go to.
	/// </summary>
	private const int MAX_LEVEL = 3;

	/// <name>
	/// EqualityEquation
	/// </name>
	/// <summary>
	/// Initializes a new instance of the EqualityEquation class.
	/// </summary>
	/// <param name="level">The level the player is currently at in this game</param>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/26/18
	/// </date>
	public EqualityEquation(int level) 
	{
		Level = level;
		increaseRange = 6;

		// randomly generate 2 numbers for the math problem
		rnd = new System.Random ();
		GenerateNewEquation ();

	}

	/// <name>
	/// IncreaseLevel
	/// </name>
	/// <summary>
	/// Increases to the next level of difficulty.
	/// Doesn't allow level to pass the max set level
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/26/18
	/// </date>
	public void IncreaseLevel() 
	{
		if (level < MAX_LEVEL)
		{
			level++;
		}
	}

	/// <name>
	/// GenerateNewEquation
	/// </name>
	/// <summary>
	/// Generates a new equation and makes sure it isn't the same equation 
	/// that was just generated. Sets the answer to the equation as well.
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/26/18
	/// </date>
	public void GenerateNewEquation()
	{
		// get new random numbers for the math problem
		int tmpNum1 = 0, tmpNum2 = 0;

		// make sure it is not the same problem as was just given
		do
		{
			tmpNum1 = rnd.Next (0, (level * increaseRange) + 2);
			tmpNum2 = rnd.Next (0, (level * increaseRange) + 2);
		}
		while(tmpNum1 == num1 && tmpNum2 == num2);

		// set variables to reflect the new numbers
		num1 = tmpNum1;
		num2 = tmpNum2;

		// set the sign that makes this problem correct
		if (num1 == num2)
		{
			sign = EqualityType.EqualTo;
		}
		else if (num1 < num2)
		{
			sign = EqualityType.LessThan;
		}
		else
		{
			sign = EqualityType.GreaterThan;
		}
	}

	/// <name>
	/// Num1
	/// </name>
	/// <summary>
	/// returns num1
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/26/18
	/// </date>
	public int Num1
	{
		get { return num1; }
	}

	/// <name>
	/// Num2
	/// </name>
	/// <summary>
	/// returns num2
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/26/18
	/// </date>
	public int Num2 
	{
		get { return num2; }
	}

	/// <name>
	/// Level
	/// </name>
	/// <summary>
	/// Gets or sets the level. Won't let level be set lower than 1.
	/// </summary>
	/// <value>The level the player is currently at.</value>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/26/18
	/// </date>
	public int Level 
	{
		get { return level; }
		set { 
			if (value > 0)
				level = value;
			else
				level = 1;
		}
	}

	/// <name>
	/// Sign
	/// </name>
	/// <summary>
	/// The sign that makes the problem true
	/// </summary>
	/// <value>The sign.</value>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/26/18
	/// </date>
	public EqualityType Sign
	{
		get { return sign; }
	}

	/// <name>
	/// EquationString
	/// </name>
	/// <summary>
	/// Creates the equation string for display purposes.
	/// </summary>
	/// <value>The equation string.</value>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/26/18
	/// </date>
	public string EquationString
	{
		get { return num1.ToString () + "  ?  " + num2.ToString (); }
	}
}
