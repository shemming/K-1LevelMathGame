﻿using System;

namespace AssemblyCSharp
{
	/// <summary>
	/// Holds data for one game
	/// </summary>
	[System.Serializable]
	public class Game
	{
		public static Game current;
		public MiniGame addition;
		public MiniGame subtraction;
		public MiniGame counting;

		public Game ()
		{
			this.addition = new MiniGame ();
			this.subtraction = new MiniGame ();
			this.counting = new MiniGame ();
		}
	}
}

