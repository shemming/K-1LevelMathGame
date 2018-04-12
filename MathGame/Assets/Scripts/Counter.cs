using System;

namespace AssemblyCSharp
{
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

		/// <summary>
		/// Initializes a new instance of the Counter class.
		/// </summary>
		/// <param name="level">Level of equation difficulty. Can be > 1 if user has saved data.</param>
		public Counter (int level)
		{
			
			Level = level;
			this.increaseRange = 20;

			// randomly generate a number for the user to count to
			rnd = new System.Random ();
			GenerateNewNumber ();
		}

		/// <summary>
		/// Generates a new number for the user to find
		/// </summary>
		public void GenerateNewNumber() 
		{
			num = rnd.Next (0, level * increaseRange);
		}

		/// <summary>
		/// returns number to be guessed
		/// </summary>
		public int Num
		{
			get { return num; }
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

	}
}

