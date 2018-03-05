using System;

namespace AssemblyCSharp
{
	/// <summary>
	/// holds math equation
	/// </summary>
	public class MathEquation
	{
		/// <summary>
		/// Number used in addition equation
		/// </summary>
		private int num1, num2;

		/// <summary>
		/// Generates random numbers
		/// </summary>
		private System.Random rnd;

		private bool isAddition, isSubtraction;
		private int level, increaseRange;

		public MathEquation (int level, int increaseRange, bool isAddition = false, bool isSubtraction = false)
		{
			this.isAddition = isAddition;
			this.isSubtraction = isSubtraction;
			this.level = level;
			this.increaseRange = increaseRange;

			// randomly generate 2 numbers between 0-10 for the math problem
			rnd = new System.Random ();
			num1 = rnd.Next (0, level*increaseRange);
			num2 = rnd.Next (0, level*increaseRange);
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
		/// returns the sum of num1 and num2
		/// </summary>
		public int Sum 
		{
			get { return num1 + num2; }
		}

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

		public void IncreaseLevel() 
		{
			level++;
		}


		/// <summary>
		/// Gets new numbers for the math problem.
		/// Makes sure exact equations aren't repeated.
		/// num1 and num2 can exchange values and will still 
		/// be considered a new equation.
		/// </summary>
		public void GenerateNewEquation()
		{
			// get new random numbers for the math problem
			int tmpNum1 = rnd.Next (0, level*increaseRange);
			int tmpNum2 = rnd.Next (0, level*increaseRange);

			if (isAddition)
			{
				// make sure it is not the same problem as was just given
				while (tmpNum1 == num1 && tmpNum2 == num2)
				{
					tmpNum1 = rnd.Next (0, level*increaseRange);
					tmpNum2 = rnd.Next (0, level*increaseRange);
				}
			}
			else if (isSubtraction)
			{
				// make sure it is not the same problem as was just given
				// and make sure the answer won't be negative
				while ((tmpNum1 == num1 && tmpNum2 == num2)
					|| tmpNum1 < tmpNum2)
				{
					tmpNum1 = rnd.Next (0, level*increaseRange);
					tmpNum2 = rnd.Next (0, level*increaseRange);
				}
			}

			// set variables to reflect the new numbers
			num1 = tmpNum1;
			num2 = tmpNum2;
		}
	}
}

