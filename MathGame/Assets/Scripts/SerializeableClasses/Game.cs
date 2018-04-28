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

		// Keeps track of if instructions were shown for the next
		// time the player logs on
		public bool instructionsShown;

		// keeps track of how many times user starts over
		public int gamesCompleted;

		// Keeps track of progress in all mini games
		public MiniGame addition;
		public MiniGame subtraction;
		public MiniGame counting;
		public MiniGame equality;

		// Keeps track of progress in all challenges
		public Challenge additionChallenge;
		public Challenge subtractionChallenge;

		public Game ()
		{
			this.instructionsShown = false;
			this.gamesCompleted = 0;

			this.addition = new MiniGame ();
			this.subtraction = new MiniGame ();
			this.counting = new MiniGame ();
			this.equality = new MiniGame ();

			this.additionChallenge = new Challenge ();
			this.subtractionChallenge = new Challenge ();
		}
	}
}

