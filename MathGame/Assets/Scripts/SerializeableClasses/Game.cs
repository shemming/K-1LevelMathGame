using System;

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
		public Challenge additionChallenge;
		public Challenge subtractionChallenge;

		public Game ()
		{
			this.addition = new MiniGame ();
			this.subtraction = new MiniGame ();
			this.counting = new MiniGame ();
			this.additionChallenge = new Challenge ();
			this.subtractionChallenge = new Challenge ();
		}
	}
}

