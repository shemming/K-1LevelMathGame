using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayAnimation : MonoBehaviour 
{

	private Animator ani;

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
	/// <param name="state">name of the state (animation) to be played</param>
	public void Animate(string state) 
	{
		ani.Play (state, -1, 0f);
	}

	/// <summary>
	/// Play animation associated with the GameObject
	/// this script is attached to & waits the length of the clip
	/// </summary>
	/// <returns>yeild instruction to wait for a given number of seconds</returns>
	/// <param name="stateName">name of the state (animation) to be played</param>
	public IEnumerator AnimateAndWait(string stateName) 
	{
		ani.Play (stateName, -1, 0f);

		float length = 0;
		AnimationClip[] clips = ani.runtimeAnimatorController.animationClips;

		var clip = clips.Where ((AnimationClip arg) => (arg.name == stateName)).SingleOrDefault();
		if (clip != null)
		{
			length = clip.length;
		}

		Debug.Log (length);
		yield return new WaitForSeconds (length);
	}
}
