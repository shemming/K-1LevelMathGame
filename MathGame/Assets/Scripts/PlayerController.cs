using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	/// <summary>
	/// Floating point variable to store the player's movement speed.
	/// </summary>
	public float speed;             

	/// <summary>
	/// Store a reference to the Rigidbody2D component required to use 2D Physics.
	/// </summary>
	private Rigidbody2D rb2d;  

	/// <summary>
	/// Store a reference to the animator controller to animate movement
	/// </summary>
	private Animator ani;

	/// <summary>
	/// Bools left and right hold if the character was last moving right or left
	/// </summary>
	private bool left, right;


	/// <summary>
	/// Used for initialization
	/// </summary>
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component 
		rb2d = GetComponent<Rigidbody2D> ();

		// Get and store a reference to the animator controller
		ani = GetComponent<Animator> ();

		// set the direction the player will face on first move if player moves
		// up or down first
		left = false;
		right = true;
	}

	/// <summary>
	/// Update is called once per frame, used for player animation
	/// </summary>
	void Update() 
	{
		// set bool to know the last left or right arrow clicked if going up or down
		if (Input.GetKeyDown (KeyCode.LeftArrow))
		{
			left = true;
			right = false;
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow))
		{
			right = true;
			left = false;
		}

		// choose which direction to face for animation and play it
		// if left arrow is being clicked or up/down arrow is being clicked and left arrow was clicked last
		if (Input.GetKey (KeyCode.LeftArrow) ||
			((Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) && left))
		{
			rb2d.transform.eulerAngles = new Vector3(0, 180, 0);
			ani.SetBool ("walkRight", true);
		}
		// if right arrow is being clicked or up/down arrow is being clicked and right arrow was clicked last
		else if (Input.GetKey (KeyCode.RightArrow) ||
			((Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) && right))
		{
			rb2d.transform.eulerAngles = new Vector3(0, 0, 0);
			ani.SetBool ("walkRight", true);
		}
		// character is not moving
		else
		{
			ani.SetBool ("walkRight", false);
		}
	}

	/// <summary>
	/// Called every fixed framerate frame. Should be used instead of Update when dealing with Rigidbody.
	/// </summary>
	void FixedUpdate() 
	{
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		rb2d.AddForce (movement * speed);
	}

}
