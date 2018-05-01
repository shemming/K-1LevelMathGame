using System;
using UnityEngine;

namespace AssemblyCSharp
{
	/// <summary>
	/// holds math equation
	/// </summary>
	public class MathEquation
	{

		/// <summary>
		/// Determines what to do with the equation
		/// </summary>
		public enum EquationType {
			Addition,
			Subtraction
		}

		/// <summary>
		/// Number used in addition equation
		/// </summary>
		private int num1, num2;

		/// <summary>
		/// Generates random numbers
		/// </summary>
		private System.Random rnd;

		/// <summary>
		/// Determines what type of equation is getting evaluated
		/// </summary>
		private EquationType equationType;

		/// <summary>
		/// Used to determine the difficulty of the equation.
		/// </summary>
		private int level, increaseRange;

		/// <summary>
		/// The max level that the game can go to.
		/// </summary>
		private const int MAX_LEVEL = 3;

		/// <name>
		/// MathEquation
		/// </name>
		/// <summary>
		/// Initializes a new instance of the MathEquation class.
		/// </summary>
		/// <param name="level">Level of equation difficulty. Can be > 1 if user has saved data</param>
		/// <param name="increaseRange">how much each level increases by</param>
		/// <param name="type">type of equation</param> 
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 
		/// </date>
		public MathEquation (int increaseRange, int level, EquationType type)
		{
			this.equationType = type;
			Level = level;
			this.increaseRange = increaseRange;

			// randomly generate 2 numbers for the math problem
			rnd = new System.Random ();
			GenerateNewEquation ();
		}

		/// <name>
		/// Num1
		/// </name>
		/// <summary>
		/// returns num1
		/// </summary>
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
		public int Num2 
		{
			get { return num2; }
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
		/// 
		/// </date>
		public string EquationString 
		{
			get 
			{ 
				if (equationType == EquationType.Addition)
				{
					return num1.ToString () + " + " + num2.ToString () + " = "; 
				}
				else
				{
					return num1.ToString () + " - " + num2.ToString () + " = "; 
				}
			}
		}

		/// <name>
		/// Sum
		/// </name>
		/// <summary>
		/// Gets the sum.
		/// </summary>
		/// <value>The sum.</value>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 
		/// </date>
		public int Sum 
		{
			get { return num1 + num2; }
		}

		/// <name>
		/// Difference
		/// </name>
		/// <summary>
		/// Gets the difference.
		/// </summary>
		/// <value>The difference.</value>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 
		/// </date>
		public int Difference 
		{
			get { return num1 - num2; }
		}

		/// <name>
		/// Level
		/// </name>
		/// <summary>
		/// Gets or sets the level. Won't let level be set lower than 1.
		/// </summary>
		/// <value>The level.</value>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 
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
		/// 
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
		/// Gets new numbers for the math problem.
		/// Makes sure exact equations aren't repeated.
		/// num1 and num2 can exchange values and will still 
		/// be considered a new equation.
		/// </summary>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 
		/// </date>
		public void GenerateNewEquation()
		{
			// get new random numbers for the math problem
			int tmpNum1 = 0, tmpNum2 = 0;

			if (equationType == EquationType.Addition)
			{
				// make sure it is not the same problem as was just given
				do
				{
					tmpNum1 = rnd.Next (0, level * increaseRange);
					tmpNum2 = rnd.Next (0, level * increaseRange);
				}
				while(tmpNum1 == num1 && tmpNum2 == num2);
			}
			else if (equationType == EquationType.Subtraction)
			{
				// make sure it is not the same problem as was just given
				// and make sure the answer won't be negative
				do
				{
					tmpNum1 = rnd.Next (0, level * increaseRange);
					tmpNum2 = rnd.Next (0, level * increaseRange);
				}
				while ((tmpNum1 == num1 && tmpNum2 == num2)
				       || tmpNum1 < tmpNum2);
			}

			// set variables to reflect the new numbers
			num1 = tmpNum1;
			num2 = tmpNum2;
		}
	}
}

