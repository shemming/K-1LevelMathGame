﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace AssemblyCSharp
{
	/// <summary>
	/// Script that runs the equality mini game. Attached to the canvas object.
	/// </summary>
	public class Equality : MonoBehaviour 
	{
		// breings user back to the main story area
		public Button exitButton;

		// displays equation to the user
		public Text equationText;

		// used for user to pick what would make the comparison correct
		public Button greaterButton;
		public Button lessButton;
		public Button equalButton;

		// displays the player's game score on screen
		public Text score;

		// used to update game information
		public GameObject gameStatsGO;
		private GlobalControl gameStats;
		private MiniGame equalityGame;

		// holds logic for the equality equations player is to solve
		private EqualityEquation equation;

		/// <name>
		/// Start
		/// </name>
		/// <summary>
		/// Use this for initialization. 
		/// </summary>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 4/26/18
		/// </date>
		void Start ()
		{
			// get access to saved addition game info to update
			gameStats = gameStatsGO.GetComponent<GlobalControl> ();
			equalityGame = gameStats.savedGameData.equality;

			equation = new EqualityEquation (equalityGame.level);

			equationText.text = equation.EquationString;
			score.text = "Score: " + equalityGame.correctAnswers.ToString();

			exitButton
				.onClick
				.AddListener (ExitToMainMenu);

			greaterButton
				.onClick
				.AddListener (delegate{CheckAnswer(EqualityEquation.EqualityType.GreaterThan);});
			
			lessButton
				.onClick
				.AddListener (delegate{CheckAnswer(EqualityEquation.EqualityType.LessThan);});
			
			equalButton
				.onClick
				.AddListener (delegate{CheckAnswer(EqualityEquation.EqualityType.EqualTo);});
		}
		/* void Start () */

		/// <name>
		/// ExitToMainMenu
		/// </name>
		/// <summary>
		/// Brings user back to the main area
		/// </summary>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 4/26/18
		/// </date>
		void ExitToMainMenu () 
		{
			gameStats.SavePlayer ();
			SceneManager.LoadScene(Constants.SceneNames.MAIN_AREA);
		}
		/* void ExitToMainMenu () */

		/// <name>
		/// CheckAnswer
		/// </name>
		/// <summary>
		///  checks if the user's choice makes the comparison correct
		/// </summary>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 4/26/18
		/// </date>
		void CheckAnswer(EqualityEquation.EqualityType sign) 
		{
			if (sign == equation.Sign)
			{
				equalityGame.correctAnswers++;
				equation.GenerateNewEquation ();
				equationText.text = equation.EquationString;

				// if player answers 10 questions right, they move to the next level
				if (equalityGame.correctAnswers % 10 == 0)
				{

					equation.IncreaseLevel ();
					equalityGame.level = equation.Level;
				}

				// update score on the screen
				score.text = "Score: " + equalityGame.correctAnswers.ToString();
			}
		}
		/* void CheckAnswer(EqualityEquation.EqualityType sign) */
	}
}

