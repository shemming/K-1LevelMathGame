using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainAreaTriggers : MonoBehaviour {
//	public GameObject gameStatsGO;
	public string area;
	
	public void OnTriggerEnter2D (Collider2D other) 
	{
//		GlobalControl gameStats = gameStatsGO.GetComponent<GlobalControl> ();
//		gameStats.SavePlayer ();
		SceneManager.LoadScene(area);
	}
}
