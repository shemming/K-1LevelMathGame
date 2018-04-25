using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainAreaTriggers : MonoBehaviour {

	/// <summary>
	/// holds the name of the scene to be loaded
	/// </summary>
	public string area;

	/// <summary>
	/// Loads the scene the player is walking into 
	/// </summary>
	public void OnTriggerEnter2D () 
	{
		SceneManager.LoadScene(area);
	}

}
