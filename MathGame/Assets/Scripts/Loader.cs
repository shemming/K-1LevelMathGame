using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this to the camera for every scene and put the prefab for the game manager 
/// in the gameManager public variable. This allows game data to persist between scenes
/// </summary>
public class Loader : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Awake () {
		if (GlobalControl.Instance == null)
		{
			Instantiate (gameManager);
		}
	}
}
