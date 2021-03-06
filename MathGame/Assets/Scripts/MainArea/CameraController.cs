﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls where the camera moves during gameplay
/// </summary>
public class CameraController : MonoBehaviour {

	/// <summary>
	/// Public variable to store a reference to the player game object
	/// </summary>
	public GameObject player;   

	/// <summary>
	/// Public variable to store a reference to the camera following the game object
	/// </summary>
	public Camera myCamera;

	/// <summary>
	/// Private variable to store the offset distance between the player and camera
	/// </summary>
	private Vector3 offset;         

	/// <summary>
	/// The size of the map in terms of X axis
	/// </summary>
	private const float MAP_X = 17.92f;

	/// <summary>
	/// The size of the map in terms of Y axis
	/// </summary>
	private const float MAP_Y = 10.24f;

	/// <summary>
	/// Stores the min X coordinates for the camera
	/// </summary>
	private float minX;

	/// <summary>
	/// Stores the max X coordinates for the camera
	/// </summary>
	private float maxX;

	/// <summary>
	/// Stores the min Y coordinates for the camera
	/// </summary>
	private float minY;

	/// <summary>
	/// Stores the max Y coordinates for the camera
	/// </summary>
	private float maxY;

	/// <name>
	/// Start
	/// </name>
	/// <summary>
	/// Use this for initialization
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/12/18
	/// </date>
	void Start () 
	{
		var vertExtent = myCamera.orthographicSize;
		var horzExtent = vertExtent * Screen.width / Screen.height;

		// Calculations assume map is position at the origin
		minX = horzExtent - MAP_X / 2.0f;
		maxX = MAP_X / 2.0f - horzExtent;
		minY = vertExtent - MAP_Y / 2.0f;
		maxY = MAP_Y / 2.0f - vertExtent;

		// Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - player.transform.position;
	}
	/* void Start () */

	/// <name>
	/// LateUpdate
	/// </name>
	/// <summary>
	/// LateUpdate is called after Update each frame.
	/// Ensures character movement already happened that frame for the camera to follow
	/// </summary>
	/// <author>
	/// Sabrina Hemming
	/// </author>
	/// <date>
	/// 4/12/18
	/// </date>
	void LateUpdate () 
	{
		
		// Set the position of the camera's transform to be the same as the player's, 
		// but offset by the calculated offset distance.
		transform.position = player.transform.position + offset;

		// make sure the camera doesn't leave the map area
		Vector3 v3 = transform.position;
		v3.x = Mathf.Clamp(v3.x, minX, maxX);
		v3.y = Mathf.Clamp(v3.y, minY, maxY);
		transform.position = v3;
	}
	/* void LateUpdate () */
}
