using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Loads a scene when a trigger is hit
/// </summary>
public class MainAreaTriggers : MonoBehaviour {

	/// <summary>
	/// holds the name of the scene to be loaded
	/// </summary>
	public string area;

	/// <name>
	/// OnTriggerEnter2D
	/// </name>
	/// <summary>
	/// Loads the scene the player is walking into 
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/14/18
	/// </date>
	public void OnTriggerEnter2D () 
	{
		SceneManager.LoadScene(area);
	}

}
