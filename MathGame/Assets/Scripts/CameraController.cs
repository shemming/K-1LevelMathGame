using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//Public variable to store a reference to the player game object
	public GameObject player;   

	// Public variable to store a reference to the camera following the game object
	public Camera myCamera;

	//Private variable to store the offset distance between the player and camera
	private Vector3 offset;         

	// The size of the map in terms of x and y axis
	float mapX = 17.92f;
	float mapY = 10.24f;

	// Stores the min and max X and Y coordinates for the camera
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	// Use this for initialization
	void Start () 
	{
		var vertExtent = myCamera.orthographicSize;
		var horzExtent = vertExtent * Screen.width / Screen.height;

		// Calculations assume map is position at the origin
		minX = horzExtent - mapX / 2.0f;
		maxX = mapX / 2.0f - horzExtent;
		minY = vertExtent - mapY / 2.0f;
		maxY = mapY / 2.0f - vertExtent;

		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - player.transform.position;
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		
		// Set the position of the camera's transform to be the same as the player's, 
		// but offset by the calculated offset distance.
		transform.position = player.transform.position + offset;

		// make sure the camera doesn't leave the map area
		var v3 = transform.position;
		v3.x = Mathf.Clamp(v3.x, minX, maxX);
		v3.y = Mathf.Clamp(v3.y, minY, maxY);
		transform.position = v3;
	}
}
