using System;

namespace AssemblyCSharp
{
	/// <summary>
	/// Generates a new number to be counted to
	/// </summary>
	public class Counter
	{
		
		/// <summary>
		/// Used to determine the difficulty of the equation.
		/// </summary>
		private int level, increaseRange;

		/// <summary>
		/// The number of objects being counted.
		/// </summary>
		private int num;

		/// <summary>
		/// Generates random numbers
		/// </summary>
		private System.Random rnd;

		/// <summary>
		/// The max level that the game can go to.
		/// </summary>
		private const int MAX_LEVEL = 3;

		/// <name>
		/// Counter
		/// </name>
		/// <summary>
		/// Initializes a new instance of the Counter class.
		/// </summary>
		/// <param name="level">Level of equation difficulty. Can be > 1 if user has saved data.</param>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 4/10/18
		/// </date>
		public Counter (int level)
		{
			
			Level = level;
			this.increaseRange = 20;

			// randomly generate a number for the user to count to
			rnd = new System.Random ();
			GenerateNewNumber ();
		}
		/* public Counter (int level) */

		/// <name>
		/// GenerateNewNumber
		/// </name>
		/// <summary>
		/// Generates a new number for the user to find
		/// </summary>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 4/10/18
		/// </date>
		public void GenerateNewNumber() 
		{
			int tmp = 0;
			do
			{
				tmp = rnd.Next (0, level * increaseRange);
			}
			while(tmp != num);
			num = tmp;
		}
		/* public void GenerateNewNumber() */

		/// <name>
		/// Num
		/// </name>
		/// <summary>
		/// returns number to be guessed
		/// </summary>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 4/10/18
		/// </date>
		public int Num
		{
			get { return num; }
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
		/// 4/10/18
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
		/// 4/10/18
		/// </date>
		public void IncreaseLevel() 
		{
			if (level < MAX_LEVEL)
			{
				level++;
			}
		}
		/* public void IncreaseLevel() */

	}
}

