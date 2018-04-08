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
	public void Animate(string state) 
	{
		ani.Play (state, -1, 0f);
	}

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

		//var length = ani.GetCurrentAnimatorClipInfo (0).LongLength;
		Debug.Log (length);
		yield return new WaitForSeconds (length);
	}
}
