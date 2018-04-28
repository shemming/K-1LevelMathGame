﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public EqualityEquation(int level) 
	{
		Level = level;
		increaseRange = 6;

		// randomly generate 2 numbers for the math problem
		rnd = new System.Random ();
		GenerateNewEquation ();

	}

	/// <summary>
	/// Increases to the next level of difficulty.
	/// Doesn't allow level to pass the max set level
	/// </summary>
	public void IncreaseLevel() 
	{
		if (level < MAX_LEVEL)
		{
			level++;
		}
	}

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

	/// <summary>
	/// returns num1
	/// </summary>
	public int Num1
	{
		get { return num1; }
	}

	/// <summary>
	/// returns num2
	/// </summary>
	public int Num2 
	{
		get { return num2; }
	}

	/// <summary>
	/// Gets or sets the level. Won't let level be set lower than 1.
	/// </summary>
	/// <value>The level.</value>
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

	public EqualityType Sign
	{
		get { return sign; }
	}

	public string EquationString
	{
		get { return num1.ToString () + "  ?  " + num2.ToString (); }
	}
}