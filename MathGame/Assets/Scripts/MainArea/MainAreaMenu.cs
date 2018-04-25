using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainAreaMenu : MonoBehaviour {

	public Button saveQuitButton;
	public Button saveButton;
	public Button challengeButton;
	public GlobalControl gameManager;

	// Use this for initialization
	void Start () {
		saveQuitButton
			.onClick
			.AddListener (SaveQuit);

		saveButton
			.onClick
			.AddListener (Save);

		challengeButton
			.onClick
			.AddListener (Challenge);
	}
	
	private void SaveQuit() 
	{
		gameManager.SavePlayer();
		GlobalControl.Save ();
		SceneManager.LoadScene (Constants.SceneNames.MAIN_MENU);
	}

	private void Challenge() 
	{
		gameManager.SavePlayer ();
		GlobalControl.Save ();
		SceneManager.LoadScene (Constants.SceneNames.CHALLENGE);
	}

	private void Save() 
	{
		GlobalControl.Save ();
		gameManager.SavePlayer ();
	}
}
