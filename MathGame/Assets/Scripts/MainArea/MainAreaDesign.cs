using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controls what incentives / pop up screens are shown
/// </summary>
public class MainAreaDesign : MonoBehaviour
{

	// used to update game information
	public GameObject gameStatsGO;
	private GlobalControl gameStats;

	// used to display instructions when player starts new game
	public GameObject instructions;
	public GameObject blackOutSheet;
	public GameObject resetPrompt;

	// holds a prefab of a yellow star to be generated along the top of the screen to 
	// represent the number of times the player reset theit game data
	public GameObject yellowStar;

	/// <name>
	/// OnEnable
	/// </name>
	/// <summary>
	/// Called when the object becomes enabled and active,
	/// aka every time the scene is loaded
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/23/18
	/// </date>
	void OnEnable ()
	{
		// load player info into savedGameData to transfer into different mini games
		gameStats = gameStatsGO.GetComponent<GlobalControl> ();
		gameStats.LoadPlayer ();

		// displays any incentives or stars earned by the player
		SetExtras ();

		// if player has already made progress in the game, seen the instructions, or completed a game
		// don't show the instructions automatically
		if (!gameStats.IsGameStarted () && !gameStats.savedGameData.instructionsShown && gameStats.savedGameData.gamesCompleted == 0)
		{
			instructions.SetActive (true);
			blackOutSheet.SetActive (true);
			gameStats.savedGameData.instructionsShown = true;
			gameStats.SavePlayer ();
			FreezePlayer ();
		}

		// if player has completed all the mini games and hasn't been prompted to reset their progress, prompt them
		// if they have ever been prompted, they are not ever prompted again.
		if (gameStats.IsGameComplete () && !gameStats.savedGameData.resetPromptShown)
		{
			resetPrompt.SetActive (true);
			resetPrompt.GetComponentInChildren<Text> ().text = "Would you like to reset your game? All of your game scores will be " +
				"reset to zero and you will earn a star. Your timed challenge progress will not be erased.";
			blackOutSheet.SetActive (true);
			gameStats.savedGameData.resetPromptShown = true;
			gameStats.SavePlayer ();
			FreezePlayer ();
		}


	}
	/* void OnEnable () */

	/// <name>
	/// SetExtras
	/// </name>
	/// <summary>
	/// Turns on any incentives or starts earned for the current player.
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/28/18
	/// </date>
	public void SetExtras() 
	{
		// only show incentives the player has earned
		SetIncentives ();

		// show a star for every time the player reset the game
		SetStars ();
	}
	/* public void SetExtras() */

	/// <name>
	/// FreezePlayer
	/// </name>
	/// <summary>
	/// Disables player movement through use of arrow keys
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/23/18
	/// </date>
	public void FreezePlayer() 
	{
		GameObject varGameObject = GameObject.FindWithTag(Constants.PLAYER);
		varGameObject.GetComponent<PlayerController>().enabled = false;
	}
	/* public void FreezePlayer() */

	/// <name>
	/// UnfreezePlayer
	/// </name>
	/// <summary>
	/// Enables player movement through use of arrow keys
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/23/18
	/// </date>
	public void UnfreezePlayer() 
	{
		GameObject varGameObject = GameObject.FindWithTag(Constants.PLAYER);
		varGameObject.GetComponent<PlayerController>().enabled = true;
	}
	/* public void UnfreezePlayer() */

	/// <name>
	/// Start
	/// </name>
	/// <summary>
	/// Use this for initialization
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/20/18
	/// </date>
	void Start ()
	{
		// makes addition text mesh object visible on screen
		GameObject additionText = GameObject.Find (Constants.MainArea.Addition.SIGN_TEXT);
		additionText.GetComponent<TextMesh>().GetComponent<MeshRenderer>().sortingLayerName = Constants.MainArea.SORTING_LAYER;
		additionText.GetComponent<TextMesh>().GetComponent<MeshRenderer>().sortingOrder = 2;

		// makes subtraction text mesh object visible on screen
		GameObject subtractionText = GameObject.Find (Constants.MainArea.Subtraction.SIGN_TEXT);
		subtractionText.GetComponent<TextMesh>().GetComponent<MeshRenderer>().sortingLayerName = Constants.MainArea.SORTING_LAYER;
		subtractionText.GetComponent<TextMesh>().GetComponent<MeshRenderer>().sortingOrder = 2;

		// makes counting text mesh object visible on screen
		GameObject countingText = GameObject.Find (Constants.MainArea.Counting.SIGN_TEXT);
		countingText.GetComponent<TextMesh>().GetComponent<MeshRenderer>().sortingLayerName = Constants.MainArea.SORTING_LAYER;
		countingText.GetComponent<TextMesh>().GetComponent<MeshRenderer>().sortingOrder = 2;

		// makes equality text mesh object visible on screen
		GameObject equalityText = GameObject.Find (Constants.MainArea.Equality.SIGN_TEXT);
		equalityText.GetComponent<TextMesh>().GetComponent<MeshRenderer>().sortingLayerName = Constants.MainArea.SORTING_LAYER;
		equalityText.GetComponent<TextMesh>().GetComponent<MeshRenderer>().sortingOrder = 2;
	}
	/* void Start () */

	/// <name>
	/// SetIncentives
	/// </name>
	/// <summary>
	/// Turn off incentives not earned.
	/// Easier this way because if they start out turned off, they can't be found
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/20/18
	/// </date>
	private void SetIncentives() 
	{

		// display earned addition incentives
		int additionScore = gameStats.savedGameData.addition.correctAnswers;

		// turn off level 3 incentives if not reached
		if (additionScore < 30)
		{
			GameObject[] addition3 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Addition.INCENTIVES_3);
			foreach (GameObject animal in addition3) 
			{
				animal.SetActive (false);
			}
		}

		// turn off level 2 incentives if not reached
		if (additionScore < 20)
		{
			GameObject[] addition2 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Addition.INCENTIVES_2);
			foreach (GameObject animal in addition2) 
			{
				animal.SetActive (false);
			}
		}

		// turn off level 1 incentives if not reached
		if (additionScore < 10)
		{
			GameObject[] addition1 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Addition.INCENTIVES_1);
			foreach (GameObject animal in addition1) 
			{
				animal.SetActive (false);
			}
		}


		// display earned subtraction incentives
		int subtractionScore = gameStats.savedGameData.subtraction.correctAnswers;

		// turn off level 3 incentives if not reached
		if (subtractionScore < 30)
		{
			GameObject[] subtraction1 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Subtraction.INCENTIVES_3);
			foreach (GameObject animal in subtraction1) 
			{
				animal.SetActive (false);
			}
		}

		// turn off level 2 incentives if not reached
		if (subtractionScore < 20)
		{
			GameObject[] subtraction2 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Subtraction.INCENTIVES_2);
			foreach (GameObject animal in subtraction2) 
			{
				animal.SetActive (false);
			}
		}

		// turn off level 1 incentives if not reached
		if (subtractionScore < 10)
		{
			GameObject[] subtraction1 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Subtraction.INCENTIVES_1);
			foreach (GameObject animal in subtraction1) 
			{
				animal.SetActive (false);
			}
		}


		// display earned counting incentives
		int countingScore = gameStats.savedGameData.counting.correctAnswers;

		// turn off level 3 incentives if not reached
		if (countingScore < 30)
		{
			GameObject[] counting1 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Counting.INCENTIVES_3);
			foreach (GameObject animal in counting1) 
			{
				animal.SetActive (false);
			}
		}

		// turn off level 2 incentives if not reached
		if (countingScore < 20)
		{
			GameObject[] counting2 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Counting.INCENTIVES_2);
			foreach (GameObject animal in counting2) 
			{
				animal.SetActive (false);
			}
		}

		// turn off level 1 incentives if not reached
		if (countingScore < 10)
		{
			GameObject[] counting3 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Counting.INCENTIVES_1);
			foreach (GameObject animal in counting3) 
			{
				animal.SetActive (false);
			}
		}

		// display earned equality incentives
		int equalityScore = gameStats.savedGameData.equality.correctAnswers;

		// turn off level 3 incentives if not reached
		if (equalityScore < 30)
		{
			GameObject[] equality3 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Equality.INCENTIVES_3);
			foreach (GameObject animal in equality3) 
			{
				animal.SetActive (false);
			}
		}

		// turn off level 2 incentives if not reached
		if (equalityScore < 20)
		{
			GameObject[] equality2 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Equality.INCENTIVES_2);
			foreach (GameObject animal in equality2) 
			{
				animal.SetActive (false);
			}
		}

		// turn off level 3 incentives if not reached
		if (equalityScore < 10)
		{
			GameObject[] equality1 = GameObject.FindGameObjectsWithTag (Constants.MainArea.Equality.INCENTIVES_1);
			foreach (GameObject animal in equality1) 
			{
				animal.SetActive (false);
			}
		}
	}
	/* private void SetIncentives() */

	/// <name>
	/// SetStars
	/// </name>
	/// <summary>
	/// Display a star for every time the user restarts the game.
	/// Game can only restart if they beat all levels
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/28/18
	/// </date>
	private void SetStars() 
	{
		Transform parent = GameObject.Find ("Canvas").transform;

		for (int i = 0; i < gameStats.savedGameData.gamesCompleted; i++)
		{
			GameObject moduleGo = (GameObject)Instantiate (yellowStar);
			moduleGo.transform.SetParent (parent, false);

			moduleGo.transform.Translate(new Vector3 (i/2f + .5f, -0.5f));
		}
	}
	/* private void SetStars() */
}
