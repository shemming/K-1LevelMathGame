using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <name>
/// Loader
/// </name>
/// <summary>
/// Attach this to the camera for every scene and put the prefab for the game manager 
/// in the gameManager public variable. This allows game data to persist between scenes
/// </summary>
/// <author>
/// Sabrina Hemming
/// </author>
/// <date>
/// 4/14/18
/// </date>
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
