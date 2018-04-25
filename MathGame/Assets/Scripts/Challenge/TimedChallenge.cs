using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimedChallenge : MonoBehaviour 
{

	public Button additionGame;
	public Button subtractionGame;

	public Button level1;
	public Button level2;
	public Button level3;

	public Button submitAnswer;

	public Button exitButton;
	public Button storyModeButton;
	public Button replayButton;

	//displays the current math problem
	public Text mathProblem;

	// displays what the user has currently typed
	public Text userInput;

	// displays the current score
	private Text currentScore;
	private Text gameHighScore;

	// displays time left & holds num version of time left
	public Text timerText;
	private float timer;

	// different screens available for challenge portion of game
	private GameObject gameScreen;
	private GameObject startScreen;
	private GameObject chooseLevelScreen;
	private GameObject endGameScreen;

	// controls equations generated and level of difficulty
	private MathEquation equation;
	private MathEquation.EquationType equationType;
	private int level;

	// number of correct answers for a given timed challenge
	private int correctAnswers;

	private InputField inputFieldCO;
	private bool isFocused;

	// used to update game information
	public GameObject gameStatsGO;
	private GlobalControl gameStats;
	private int highScore;

	Text finalScore;
	private Text gameOverHighScore;
	private GameObject newHighScoreMessage;

	/// <summary>
	/// Used for initialization
	/// </summary>
	void Start () 
	{
		
		// used to set if the player wants to do addition or subtraction
		additionGame
			.onClick
			.AddListener (delegate{SetEquationType(MathEquation.EquationType.Addition);});

		subtractionGame
			.onClick
			.AddListener (delegate{SetEquationType(MathEquation.EquationType.Subtraction);});

		// used to set the level of difficulty of the problems given
		level1
			.onClick
			.AddListener (delegate{SetLevel(1);});

		level2
			.onClick
			.AddListener (delegate{SetLevel(2);});

		level3
			.onClick
			.AddListener (delegate{SetLevel(3);});

		// user can click to enter the answer for an equation
		submitAnswer
			.onClick
			.AddListener (CheckAnswer);

		// brings user to the main menu to choose to continue a game 
		// or start a new one
		exitButton
			.onClick
			.AddListener (ExitToMainMenu);

		// brings the user to the main area where they can pick mini games
		storyModeButton
			.onClick
			.AddListener (SwitchToStoryMode);

		// brings user through menus to pick equation type and difficulty
		replayButton
			.onClick
			.AddListener (PlayAgain);
		

		// get the text/GO elements for game over screen
		gameOverHighScore = GameObject.Find (Constants.TimedChallenge.GameOverGO.HIGH_SCORE).GetComponent<Text>();
		newHighScoreMessage = GameObject.Find (Constants.TimedChallenge.GameOverGO.NEW_HIGH_SCORE_MSG_GO);
		finalScore = GameObject.Find (Constants.TimedChallenge.GameOverGO.FINAL_SCORE).GetComponent<Text> ();

		// get the text/GO elements for the current game screen
		currentScore = GameObject.Find (Constants.TimedChallenge.CurrentGameGO.CURRENT_SCORE).GetComponent<Text> ();
		currentScore.text = correctAnswers.ToString();
		gameHighScore = GameObject.Find (Constants.TimedChallenge.CurrentGameGO.HIGH_SCORE).GetComponent<Text> ();
		inputFieldCO = GameObject.Find (Constants.INPUT).GetComponent<InputField> ();

		// get references to the different views used in this scene
		gameScreen = GameObject.Find (Constants.TimedChallenge.Views.GAME_SCREEN);
		startScreen = GameObject.Find (Constants.TimedChallenge.Views.START_SCREEN);
		chooseLevelScreen = GameObject.Find (Constants.TimedChallenge.Views.LEVEL_SCREEN);
		endGameScreen = GameObject.Find (Constants.TimedChallenge.Views.END_GAME_SCREEN);


		// set default values for these variables
		level = 1;
		correctAnswers = 0;
		equationType = MathEquation.EquationType.Addition;
		isFocused = false;

		// turn off screens other than opening screen
		gameScreen.SetActive (false);
		chooseLevelScreen.SetActive (false);
		endGameScreen.SetActive (false);

		// get access to saved challenge game info to update
		gameStats = gameStatsGO.GetComponent<GlobalControl> ();
		highScore = gameStats.savedGameData.additionChallenge.l1HighScore;




		// ADD A COUNTDOWN

	}

	/// <summary>
	/// Updates the timer in half second intervals. When the timer hits
	/// zero it stops itself from being called anymore and displays results
	/// </summary>
	void UpdateTimer() {
		if (timerText != null)
		{
			if (timer > 0.0f)
			{
				// reduce timer at the same rate this function gets called
				timer -= Constants.TimedChallenge.CurrentGameGO.TIMER_REFRESH_RATE;

				// update minutes and seconds in string format
				string minutes = Mathf.Floor (timer / 60).ToString ("00");
				string seconds = (timer % 60).ToString ("00");
				string x = "Time: " + minutes + ":" + seconds;

				// set the timer string on screen
				timerText.text = x;
			}
			else
			{
				// stops this function from being repeatedly called
				CancelInvoke ();

				// end game! the timer is done
				EndGame ();
			}
		}
	}

	/// <summary>
	/// Update is called once per frame, checks user input
	/// </summary>
	void Update() 
	{
		// if the user presses enter, take that as if they clicked the enter button
		// check if the answer is correct
		if (Input.GetKeyDown (KeyCode.Return) && isFocused)
		{
			CheckAnswer ();
		}
		else
		{
			// update the number being displayed in the input box to reflect
			// what the user has entered into it
			userInput.text = inputFieldCO.text;
		}

		// keep tabs of if input box is in focus or not
		// needed to register if input box is in focus and user clicks enter
		// because when the user hits enter it immediately goes out of focus
		if (inputFieldCO.isFocused)
		{
			isFocused = true;
		}
		else
		{
			isFocused = false;
		}
	}


	/// <summary>
	/// Sets up screen to start timed game and initializes equation
	/// </summary>
	private void StartGame() 
	{
		// show screen to start game
		gameScreen.SetActive (true);
		startScreen.SetActive (false);
		chooseLevelScreen.SetActive (false);

		// create equation based on user's choices and set display string
		equation = new MathEquation (10, level, equationType);
		mathProblem.text = equation.EquationString;

		// find high score for current game and display it to the user
		GetHighScore ();
		gameHighScore.text = highScore.ToString ();

		// sets the timer and has the UpdateTimer function called every half second
		timer = Constants.TimedChallenge.CurrentGameGO.TIMER_VALUE;
		timerText.text = "Time: 1:00";
		InvokeRepeating ("UpdateTimer", 0.0f, Constants.TimedChallenge.CurrentGameGO.TIMER_REFRESH_RATE);

	}

	/// <summary>
	/// When the timer runs out the player gets to see the results
	/// </summary>
	private void EndGame() 
	{

		// show end game screen 
		startScreen.SetActive (false);
		gameScreen.SetActive (false);
		chooseLevelScreen.SetActive (false);
		endGameScreen.SetActive (true);

		// check if user beat their high score
		if (correctAnswers > highScore)
		{
			newHighScoreMessage.SetActive (true);
			// store new high score (both in game and for save file)
			highScore = correctAnswers;
			SetStoredHighScore ();
			gameStats.SavePlayer ();
			GlobalControl.Save ();
		}
		else
		{
			// turn off the message that says player got a high score
			newHighScoreMessage.SetActive (false);
		}

		// display the score earned
		finalScore.text = "Score: " + correctAnswers.ToString ();

		// display the high score
		gameOverHighScore.text = "High Score: " + highScore.ToString ();
	}

	/// <summary>
	/// Checks the answer to the math problem given by the user.
	/// If correct, a new math problem is generated.
	/// </summary>
	private void CheckAnswer() 
	{
		// get the number entered by the user.
		// don't really need to validate it's a number because the input
		// field only allows integers
		int input;
		int.TryParse(inputFieldCO.text, out input);

		if (input == equation.Sum)
		{ 

			// generate a new math problem & update display
			correctAnswers++;
			equation.GenerateNewEquation ();
			mathProblem.text = equation.EquationString;
			inputFieldCO.text = string.Empty;
			inputFieldCO.ActivateInputField();

			// update score on the screen
			currentScore.text = correctAnswers.ToString();

		}
		else 
		{
			// highlights incorrect input so user can immediately type new answer
			inputFieldCO.ActivateInputField();
		}
	}
		
	/// <summary>
	/// Leaves the current game and goes to main menu where you can start a 
	/// new game or continue a game
	/// </summary>
	private void ExitToMainMenu()
	{
		SceneManager.LoadScene(Constants.SceneNames.MAIN_MENU);
	}

	/// <summary>
	/// Leave challenge area and go back to mini games
	/// </summary>
	private void SwitchToStoryMode() 
	{
		SceneManager.LoadScene(Constants.SceneNames.MAIN_AREA);
	}

	/// <summary>
	/// resets current score and reprompts user for equation type and
	/// level of difficulty before starting a new round
	/// </summary>
	private void PlayAgain()
	{
		// turn off screens other than opening screen
		gameScreen.SetActive (false);
		chooseLevelScreen.SetActive (false);
		endGameScreen.SetActive (false);
		startScreen.SetActive (true);

		// reset current score
		correctAnswers = 0;
		currentScore.text = correctAnswers.ToString ();

		// set the high score in the game screen
		gameHighScore.text = highScore.ToString ();

		// clear the input field and have it ready to be typed in
		inputFieldCO.text = string.Empty;
		inputFieldCO.ActivateInputField();
	}

	/// <summary>
	/// Sets the type of the equation and sets scene to have user pick difficulty
	/// level - used with equation type buttons.
	/// </summary>
	/// <param name="eqType">The equation type the user wants to solve - addition, subtraction.</param>
	private void SetEquationType(MathEquation.EquationType eqType)
	{
		equationType = eqType;

		// show screen to pick difficulty level
		startScreen.SetActive (false);
		gameScreen.SetActive (false);
		endGameScreen.SetActive (false);
		chooseLevelScreen.SetActive (true);
	}

	/// <summary>
	/// Sets the level of difficulty and starts the game - used with level buttons.
	/// </summary>
	/// <param name="level">The level of difficulty the user wants to play at.</param>
	void SetLevel(int level)
	{
		this.level = level;
		StartGame ();
	}

	/// <summary>
	/// If a new high score is set, store it in gameStats so it can be saved
	/// in the player's save file
	/// </summary>
	private void SetStoredHighScore()
	{
		// set the high score for the current game
		if (MathEquation.EquationType.Addition == equationType)
		{
			if (level == 1)
			{
				gameStats.savedGameData.additionChallenge.l1HighScore = highScore;
			}
			else if (level == 2)
			{
				gameStats.savedGameData.additionChallenge.l2HighScore = highScore;
			}
			else
			{
				gameStats.savedGameData.additionChallenge.l3HighScore = highScore;
			}
		}
		else
		{
			if (level == 1)
			{
				gameStats.savedGameData.subtractionChallenge.l1HighScore = highScore;
			}
			else if (level == 2)
			{
				gameStats.savedGameData.subtractionChallenge.l2HighScore = highScore;
			}
			else
			{
				gameStats.savedGameData.subtractionChallenge.l3HighScore = highScore;
			}
		}
	}

	/// <summary>
	/// Find out what the high score is for the current game and level
	/// </summary>
	private void GetHighScore() 
	{
		// set the high score for the current game
		if (MathEquation.EquationType.Addition == equationType)
		{
			if (level == 1)
			{
				highScore = gameStats.savedGameData.additionChallenge.l1HighScore;
			}
			else if (level == 2)
			{
				highScore = gameStats.savedGameData.additionChallenge.l2HighScore;
			}
			else
			{
				highScore = gameStats.savedGameData.additionChallenge.l3HighScore;
			}
		}
		else
		{
			if (level == 1)
			{
				highScore = gameStats.savedGameData.subtractionChallenge.l1HighScore;
			}
			else if (level == 2)
			{
				highScore = gameStats.savedGameData.subtractionChallenge.l2HighScore;
			}
			else
			{
				highScore = gameStats.savedGameData.subtractionChallenge.l3HighScore;
			}
		}
	}
}
