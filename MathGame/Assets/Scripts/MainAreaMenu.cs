﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using UnityEngine.SceneManagement;

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
		Debug.Log ("SaveQuit");
		gameManager.SavePlayer();
		GlobalControl.Save ();
		SceneManager.LoadScene (Constants.SceneNames.MAIN_MENU);
	}

	private void Challenge() 
	{
		Debug.Log ("challenge");
		gameManager.SavePlayer ();
		GlobalControl.Save ();
		SceneManager.LoadScene (Constants.SceneNames.CHALLENGE);
	}

	private void Save() 
	{
		Debug.Log ("save");
		GlobalControl.Save ();
		gameManager.SavePlayer ();
	}
}
