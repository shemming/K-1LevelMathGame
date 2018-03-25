using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour 
{

	private Animator ani;
	public string stateName;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		ani = GetComponent<Animator> ();
	}

	/// <summary>
	/// Play animation associated with the GameObject
	/// this script is attached to
	/// </summary>
	public void Animate() 
	{
		ani.Play (stateName, -1, 0f);
	}
}
