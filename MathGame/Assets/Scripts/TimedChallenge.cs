using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using UnityEngine.SceneManagement;

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
	public Text score;
	public Text gameHighScore;

	public Text timerText;
	private float timer;

	private GameObject gameScreen;
	private GameObject startScreen;
	private GameObject chooseLevelScreen;
	private GameObject endGameScreen;

	private MathEquation equation;
	private MathEquation.EquationType equationType;
	private int level;
	private int correctAnswers;

	private InputField inputFieldCO;
	private bool isFocused;

	// used to update game information
	public GameObject gameStatsGO;
	private GlobalControl gameStats;
	private int highScore;
	Text highScoreText;

	/// <summary>
	/// Used for initialization
	/// </summary>
	void Start () 
	{
		
		additionGame
			.onClick
			.AddListener (SetAddition);

		subtractionGame
			.onClick
			.AddListener (SetSubtraction);

		level1
			.onClick
			.AddListener (SetLevel1);

		level2
			.onClick
			.AddListener (SetLevel2);

		level3
			.onClick
			.AddListener (SetLevel3);

		submitAnswer
			.onClick
			.AddListener (CheckAnswer);

		exitButton
			.onClick
			.AddListener (ExitToMainMenu);

		storyModeButton
			.onClick
			.AddListener (SwitchToStoryMode);

		replayButton
			.onClick
			.AddListener (PlayAgain);
		
		// set default values for these variables
		level = 1;
		correctAnswers = 0;
		equationType = MathEquation.EquationType.Addition;
		isFocused = false;

		// get the input field as an input field object
		inputFieldCO = GameObject.Find (Constants.INPUT).GetComponent<InputField> ();

		// get the text element to display high score
		highScoreText = GameObject.Find ("HighScoreText").GetComponent<Text>();

		// get references to the different screens used in this scene
		gameScreen = GameObject.Find ("Game");
		startScreen = GameObject.Find ("OpeningScreen");
		chooseLevelScreen = GameObject.Find ("ChooseLevelScreen");
		endGameScreen = GameObject.Find ("GameOverScreen");


		// turn off screens other than opening screen
		gameScreen.SetActive (false);
		chooseLevelScreen.SetActive (false);
		endGameScreen.SetActive (false);

		// get access to saved challenge game info to update
		gameStats = gameStatsGO.GetComponent<GlobalControl> ();
		highScore = gameStats.savedGameData.additionChallenge.l1HighScore;

		// update score on the screen
		score.text = correctAnswers.ToString();



		// ADD A COUNTDOWN

	}

	void UpdateTimer() {
		if (timerText != null)
		{
			if (timer > 0.0f)
			{
				// getting called every .5sec, so reduce timer by .5
				timer -= .5f;

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

		GetHighScore ();

		gameHighScore.text = highScore.ToString ();

		timer = 10;
		timerText.text = "Time: 1:00";
		InvokeRepeating ("UpdateTimer", 0.0f, 0.5f);

	}

	private void EndGame() 
	{

		// show end game screen 
		startScreen.SetActive (false);
		gameScreen.SetActive (false);
		chooseLevelScreen.SetActive (false);
		endGameScreen.SetActive (true);

		// turn off text saying you got the new high score
		GameObject newHighScoreText = GameObject.Find ("NewHighScoreText");

		// check if user beat their high score
		if (correctAnswers > highScore)
		{
			Debug.Log ("set high score");

			newHighScoreText.SetActive (true);
			// store new high score (both in game and for save file)
			highScore = correctAnswers;
			SetStoredHighScore ();
			gameStats.SavePlayer ();
			GlobalControl.Save ();
		}
		else
		{
			newHighScoreText.SetActive (false);
		}

		// display the score earned
		Text finalScore = GameObject.Find ("ScoreText").GetComponent<Text> ();
		finalScore.text = "Score: " + correctAnswers.ToString ();

		// display the high score
		highScoreText.text = "High Score: " + highScore.ToString ();
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
			score.text = correctAnswers.ToString();

		}
		else 
		{
			// highlights incorrect input so user can immediately type new answer
			inputFieldCO.ActivateInputField();
		}
	}
		
	private void ExitToMainMenu()
	{
		SceneManager.LoadScene(Constants.SceneNames.MAIN_MENU);
	}

	private void SwitchToStoryMode() 
	{
		SceneManager.LoadScene(Constants.SceneNames.MAIN_AREA);
	}

	private void PlayAgain()
	{
		// turn off screens other than opening screen
		gameScreen.SetActive (false);
		chooseLevelScreen.SetActive (false);
		endGameScreen.SetActive (false);
		startScreen.SetActive (true);

		// reset current score
		correctAnswers = 0;
		score.text = correctAnswers.ToString ();

		// set the high score in the game screen
		gameHighScore.text = highScore.ToString ();

		// clear the input field and have it ready to be typed in
		inputFieldCO.text = string.Empty;
		inputFieldCO.ActivateInputField();
	}


	/// <summary>
	/// Sets the equation type to addition and sets up
	/// screen to choose level of difficulty.
	/// </summary>
	private void SetAddition()
	{
		equationType = MathEquation.EquationType.Addition;

		// show screen to pick difficulty level
		startScreen.SetActive (false);
		gameScreen.SetActive (false);
		endGameScreen.SetActive (false);
		chooseLevelScreen.SetActive (true);
	}

	/// <summary>
	/// Sets the equation type to subtraction and sets up
	/// screen to choose level of difficulty.
	/// </summary>
	private void SetSubtraction() 
	{
		equationType = MathEquation.EquationType.Subtraction;

		// show screen to pick difficulty level
		startScreen.SetActive (false);
		gameScreen.SetActive (false);
		endGameScreen.SetActive (false);
		chooseLevelScreen.SetActive (true);
	}

	/// <summary>
	/// Sets the level to 1 and starts game.
	/// </summary>
	private void SetLevel1() 
	{
		level = 1;
		StartGame ();
	}

	/// <summary>
	/// Sets the level to 2 and starts game.
	/// </summary>
	private void SetLevel2()
	{
		level = 2;
		StartGame ();
	}

	/// <summary>
	/// Sets the level to 3 and starts game.
	/// </summary>
	private void SetLevel3() 
	{
		level = 3;
		StartGame ();
	}

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
