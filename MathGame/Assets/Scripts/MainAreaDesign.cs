using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AssemblyCSharp;

public class MainAreaDesign : MonoBehaviour
{

	// used to update game information
	public GameObject gameStatsGO;
	private GlobalControl gameStats;


	// Use this for initialization
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

	}

	/// <summary>
	/// Called when the object becomes enabled and active,
	/// aka every time the scene is loaded
	/// </summary>
	void OnEnable ()
	{

		// load player info into savedGameData to transfer into different mini games
		gameStats = gameStatsGO.GetComponent<GlobalControl> ();
		gameStats.LoadPlayer ();

		// only show incentives the player has earned
		SetIncentives ();
	}

	/// <summary>
	/// Turn off incentives not earned.
	/// Easier this way because if they start out turned off, they can't be found
	/// </summary>
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
	}
}
