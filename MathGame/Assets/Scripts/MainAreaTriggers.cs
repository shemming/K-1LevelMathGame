using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AssemblyCSharp;

public class MainAreaTriggers : MonoBehaviour {
	public string area;

	// triggers SaveQuit function, which saves current game info and 
	// loads the main menu screen
	public Button saveQuit;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	public void Start() {
		// add a listener for when user clicks enter button
		saveQuit
			.onClick
			.AddListener (SaveQuit);
	}

	/// <summary>
	/// Loads the scene the player is walking into 
	/// </summary>
	public void OnTriggerEnter2D () 
	{
		SceneManager.LoadScene(area);
	}

	/// <summary>
	/// Save game progress and return to the main menu
	/// </summary>
	private void SaveQuit() {
		GlobalControl.Save ();
		SceneManager.LoadScene ("MainMenu");
	}
}
