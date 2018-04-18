using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainAreaSaveStats : MonoBehaviour {


	// used to update game information
	public GameObject gameStatsGO;
	private GlobalControl gameStats;

	// triggers SaveQuit function, which saves current game info and 
	// loads the main menu screen
	public Button saveQuit;

	// Use this for initialization
	void Start () {

		// add a listener for when user clicks enter button
		saveQuit
			.onClick
			.AddListener (SaveQuit);
	}

	/// <summary>
	/// Called when the object becomes enabled and active,
	/// aka every time the scene is loaded
	/// </summary>
	void OnEnable() {

		// load player info into savedGameData to transfer into different mini games
		gameStats = gameStatsGO.GetComponent<GlobalControl> ();
		gameStats.LoadPlayer ();
	}


	/// <summary>
	/// Save game progress and return to the main menu
	/// </summary>
	private void SaveQuit() {
		GlobalControl.Save ();
		SceneManager.LoadScene ("MainMenu");
	}
}
