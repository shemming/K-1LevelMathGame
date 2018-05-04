using System;

namespace AssemblyCSharp
{
	/// <summary>
	/// Constants used throughout the game
	/// </summary>
	public class Constants
	{
		/// <summary>
		/// Name of user input game object.
		/// </summary>
		public const string INPUT = "userInput";
		public const string PLAYER = "Player";

		/// <name>
		/// SceneNames
		/// </name>
		/// <summary>
		/// Names of the scenes used in the game.
		/// </summary>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 4/20/18
		/// </date>
		public class SceneNames
		{
			public const string MAIN_MENU = "MainMenu";
			public const string MAIN_AREA = "Main Area";
			public const string COUNTING = "Counting";
			public const string ADDITION = "Addition";
			public const string SUBTRACTION = "Subtraction";
			public const string CHALLENGE = "Challenge";
		}	

		/// <name>
		/// MainArea
		/// </name>
		/// <summary>
		/// Constants used exclusively in the main area scene
		/// </summary>
		/// <author>
		/// Sabrina Hemming
		/// </author>
		/// <date>
		/// 4/20/18
		/// </date>
		public class MainArea 
		{
			public const string SORTING_LAYER = "Decorations";

			/// <summary>
			/// Constants used in reference to the addition mini game
			/// </summary>
			public class Addition 
			{
				public const string SIGN_TEXT = "AdditionTextMesh";
				public const string INCENTIVES_1 = "Addition1";
				public const string INCENTIVES_2 = "Addition2";
				public const string INCENTIVES_3 = "Addition3";
			}

			/// <summary>
			/// Constants used in reference to the subtraction mini game
			/// </summary>
			public class Subtraction 
			{
				public const string SIGN_TEXT = "SubtractionTextMesh";
				public const string INCENTIVES_1 = "Subtraction1";
				public const string INCENTIVES_2 = "Subtraction2";
				public const string INCENTIVES_3 = "Subtraction3";
			}

			/// <summary>
			/// Constants used in reference to the counting mini game
			/// </summary>
			public class Counting 
			{
				public const string SIGN_TEXT = "CountingTextMesh";
				public const string INCENTIVES_1 = "Counting1";
				public const string INCENTIVES_2 = "Counting2";
				public const string INCENTIVES_3 = "Counting3";
			}

			/// <summary>
			/// Constants used in reference to the equality mini game
			/// </summary>
			public class Equality 
			{
				public const string SIGN_TEXT = "EqualityTextMesh";
				public const string INCENTIVES_1 = "Equality1";
				public const string INCENTIVES_2 = "Equality2";
				public const string INCENTIVES_3 = "Equality3";
			}

		}

		/// <summary>
		/// Constants used exclusivey in the counting mini game scene.
		/// </summary>
		public class Counting 
		{
			public const string FLOWER_TAG = "Flower";
		}

		/// <summary>
		/// Constants used exclusively in the addition mini game
		/// </summary>
		public class Addition 
		{
			public const string CHEST_OPEN_ANIMATION = "ChestAnimation";
			public const string CHEST_LOCKED_ANIMATION = "ChestLocked";
			public const string COIN_EARNED_ANIMATION = "CoinAnimation";
		}

		/// <summary>
		/// Constants used exclusively in the subtraction mini game
		/// </summary>
		public class Subtraction 
		{
			public const string CHEST_OPEN_ANIMATION = "ChestAnimation";
			public const string CHEST_LOCKED_ANIMATION = "ChestLocked";
			public const string COIN_EARNED_ANIMATION = "CoinAnimation";
		}

		/// <summary>
		/// Constants used exclusively in the main menu
		/// </summary>
		public class MainMenu 
		{
			/// <summary>
			/// extension used for saved game files
			/// </summary>
			public const string FILE_EXTENSION = @".gd";

			/// <summary>
			/// The name of the game object that holds the error message
			/// </summary>
			public const string ERROR_TEXT_GO = "ErrorMessage";

			/// <summary>
			/// Different screens available for different user flows
			/// </summary>
			public class Views
			{
				public const string WELCOME_SCREEN = "StartingScreen";
				public const string CONTINUE_SCREEN = "ContinueGameScreen";
				public const string NEW_SCREEN = "NewGameScreen";
			}

			/// <summary>
			/// Error messages that can occur when trying to save a new game
			/// </summary>
			public class Error 
			{
				public const string EXISTS = "A game already exists with this name.";
				public const string GENERIC = "Sorry, that file name can not be used. Try again.";
			}
		}

		/// <summary>
		/// Constants used exclusively in the PlayerController file
		/// </summary>
		public class PlayerController 
		{
			public const string WALK_ANIMATION = "walk";
			public const string HORIZ_AXIS = "Horizontal";
			public const string VERT_AXIS = "Vertical";
		}

		/// <summary>
		/// Constants used exclusively in TimedChallenge file
		/// </summary>
		public class TimedChallenge
		{
			/// <summary>
			/// Game objects associated with the game over screen
			/// </summary>
			public class GameOverGO
			{
				public const string FINAL_SCORE = "ScoreText";
				public const string HIGH_SCORE = "HighScoreText";
				public const string NEW_HIGH_SCORE_MSG_GO = "NewHighScoreText";
			}

			/// <summary>
			/// Game objects associated with the current game screen
			/// </summary>
			public class CurrentGameGO
			{
				public const string HIGH_SCORE = "HighScore";
				public const string CURRENT_SCORE = "Score";
				public const float TIMER_VALUE = 60f;
				public const float TIMER_REFRESH_RATE = 0.5f;
			}

			/// <summary>
			/// game objects that hold the different possible views for the timed challenge
			/// </summary>
			public class Views
			{
				public const string GAME_SCREEN = "Game";
				public const string START_SCREEN = "OpeningScreen";
				public const string LEVEL_SCREEN = "ChooseLevelScreen";
				public const string END_GAME_SCREEN = "GameOverScreen";
			}
		}
	}
}

