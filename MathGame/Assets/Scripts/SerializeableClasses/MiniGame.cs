using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;

namespace AssemblyCSharp
{
	/// <name>
	/// MiniGame
	/// </name>
	/// <summary>
	/// Holds data for one mini game (adding, subtracting, etc.)
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/13/18
	/// </date>
	[System.Serializable]
	public class MiniGame
	{
		public int correctAnswers;
		public int level;
		public int increaseRange;

		public MiniGame ()
		{
			this.correctAnswers = 0;
			this.level = 1;
			this.increaseRange = 10;
		}
	}
}

