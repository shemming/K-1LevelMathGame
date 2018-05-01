using System;

namespace AssemblyCSharp
{
	/// <name>
	/// Challenge
	/// </name>
	/// <summary>
	/// Holds data for the challenges
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 
	/// </date>
	[System.Serializable]
	public class Challenge
	{
		public int l1HighScore;
		public int l2HighScore;
		public int l3HighScore;

		public Challenge ()
		{
			l1HighScore = 0;
			l2HighScore = 0;
			l3HighScore = 0;
		}
	}
}

