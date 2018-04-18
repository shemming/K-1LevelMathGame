using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GlobalControl : MonoBehaviour {

	public static GlobalControl Instance = null;
	public Game savedGameData;
	string filename;

	/// <summary>
	/// Ensure there is only one instance in existence at a time
	/// </summary>
	void Awake() 
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else if (Instance != this)
		{
			// if there's another copy of the object with this script attached, 
			//the other object will be destroyed and this one saved
			Destroy (gameObject);
		}
	}

	/// <summary>
	/// Store all game data to carry around
	/// </summary>
	public void LoadPlayer() 
	{
		savedGameData = new Game ();

		// save all addition game data
		savedGameData.addition.correctAnswers = Instance.savedGameData.addition.correctAnswers;
		savedGameData.addition.increaseRange = Instance.savedGameData.addition.increaseRange;
		savedGameData.addition.level = Instance.savedGameData.addition.level;

		// save all subtraction game data
		savedGameData.subtraction.correctAnswers = Instance.savedGameData.subtraction.correctAnswers;
		savedGameData.subtraction.increaseRange = Instance.savedGameData.subtraction.increaseRange;
		savedGameData.subtraction.level = Instance.savedGameData.subtraction.level;

		// save all counting game data
		savedGameData.counting.correctAnswers = Instance.savedGameData.counting.correctAnswers;
		savedGameData.counting.increaseRange = Instance.savedGameData.counting.increaseRange;
		savedGameData.counting.level = Instance.savedGameData.counting.level;
	}

	/// <summary>
	/// update game data before moving between scenes
	/// </summary>
	public void SavePlayer() {
		
		// save all addition game data
		Instance.savedGameData.addition.correctAnswers = savedGameData.addition.correctAnswers;
		Instance.savedGameData.addition.increaseRange = savedGameData.addition.increaseRange;
		Instance.savedGameData.addition.level = savedGameData.addition.level;

		// save all subtraction game data
		Instance.savedGameData.subtraction.correctAnswers = savedGameData.subtraction.correctAnswers;
		Instance.savedGameData.subtraction.increaseRange = savedGameData.subtraction.increaseRange;
		Instance.savedGameData.subtraction.level = savedGameData.subtraction.level;

		// save all counting game data
		Instance.savedGameData.counting.correctAnswers = savedGameData.counting.correctAnswers;
		Instance.savedGameData.counting.increaseRange = savedGameData.counting.increaseRange;
		Instance.savedGameData.counting.level = savedGameData.counting.level;

	}

	/// <summary>
	/// Saves the game data to the file the game corresponds to
	/// </summary>
	/// <param name="s_filename">S filename.</param>
	public static void Save(string s_filename = null) 
	{
		
		// set the instance's file name if not already for future use
		if (s_filename != null)
		{
			Instance.filename = s_filename;
		}

		// creates file, creates json string out of game data, and writes json string to the new file
		string path = Application.persistentDataPath + "/" + Instance.filename + ".gd";
		string json = JsonUtility.ToJson (Instance.savedGameData);
		File.WriteAllText (@path, json);
	}

	/// <summary>
	/// Load game data that corresponds to the file name passed in
	/// </summary>
	/// <param name="s_filename">the name of the file being loaded</param>
	public static void Load(string s_filename = null) {

		// set the instance's file name if not already for future use
		if (s_filename != null)
		{
			Instance.filename = s_filename;
		}

		string path = Application.persistentDataPath + "/" + Instance.filename + ".gd";

		// ensures the file was found
		if (File.Exists (path))
		{
			// reads file to get json object and converts into game object to store data
			string json = File.ReadAllText (path);
			Instance.savedGameData = JsonUtility.FromJson<Game> (json);
		}
	}
}
