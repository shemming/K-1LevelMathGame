using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class GlobalControl : MonoBehaviour {

	public static GlobalControl Instance = null;
	public Game savedGameData;

	void Awake() 
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			// if there's another copy of the object with this script attached, 
			//the other object will be destroyed and this one saved
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	void Start() 
	{
		savedGameData = new Game ();
		savedGameData.addition.correctAnswers = GlobalControl.Instance.savedGameData.addition.correctAnswers;
		savedGameData.addition.increaseRange = GlobalControl.Instance.savedGameData.addition.increaseRange;
		savedGameData.addition.level = GlobalControl.Instance.savedGameData.addition.level;

		savedGameData.subtraction.correctAnswers = GlobalControl.Instance.savedGameData.subtraction.correctAnswers;
		savedGameData.subtraction.increaseRange = GlobalControl.Instance.savedGameData.subtraction.increaseRange;
		savedGameData.subtraction.level = GlobalControl.Instance.savedGameData.subtraction.level;

		savedGameData.counting.correctAnswers = GlobalControl.Instance.savedGameData.counting.correctAnswers;
		savedGameData.counting.increaseRange = GlobalControl.Instance.savedGameData.counting.increaseRange;
		savedGameData.counting.level = GlobalControl.Instance.savedGameData.counting.level;
	}

	public void SavePlayer() {
		GlobalControl.Instance.savedGameData.addition.correctAnswers = savedGameData.addition.correctAnswers;
		GlobalControl.Instance.savedGameData.addition.increaseRange = savedGameData.addition.increaseRange;
		GlobalControl.Instance.savedGameData.addition.level = savedGameData.addition.level;

		GlobalControl.Instance.savedGameData.subtraction.correctAnswers = savedGameData.subtraction.correctAnswers;
		GlobalControl.Instance.savedGameData.subtraction.increaseRange = savedGameData.subtraction.increaseRange;
		GlobalControl.Instance.savedGameData.subtraction.level = savedGameData.subtraction.level;

		GlobalControl.Instance.savedGameData.counting.correctAnswers = savedGameData.counting.correctAnswers;
		GlobalControl.Instance.savedGameData.counting.increaseRange = savedGameData.counting.increaseRange;
		GlobalControl.Instance.savedGameData.counting.level = savedGameData.counting.level;

	}
}
