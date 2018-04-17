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
		}
		else if (Instance != this)
		{
			// if there's another copy of the object with this script attached, 
			//the other object will be destroyed and this one saved
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	/// <summary>
	/// Store all game data to carry around
	/// </summary>
	void Start() 
	{
		savedGameData = new Game ();

		// save all addition game data
		savedGameData.addition.correctAnswers = GlobalControl.Instance.savedGameData.addition.correctAnswers;
		savedGameData.addition.increaseRange = GlobalControl.Instance.savedGameData.addition.increaseRange;
		savedGameData.addition.level = GlobalControl.Instance.savedGameData.addition.level;

		// save all subtraction game data
		savedGameData.subtraction.correctAnswers = GlobalControl.Instance.savedGameData.subtraction.correctAnswers;
		savedGameData.subtraction.increaseRange = GlobalControl.Instance.savedGameData.subtraction.increaseRange;
		savedGameData.subtraction.level = GlobalControl.Instance.savedGameData.subtraction.level;

		// save all counting game data
		savedGameData.counting.correctAnswers = GlobalControl.Instance.savedGameData.counting.correctAnswers;
		savedGameData.counting.increaseRange = GlobalControl.Instance.savedGameData.counting.increaseRange;
		savedGameData.counting.level = GlobalControl.Instance.savedGameData.counting.level;
	}

	/// <summary>
	/// update game data before moving between scenes
	/// </summary>
	public void SavePlayer() {
		
		// save all addition game data
		GlobalControl.Instance.savedGameData.addition.correctAnswers = savedGameData.addition.correctAnswers;
		GlobalControl.Instance.savedGameData.addition.increaseRange = savedGameData.addition.increaseRange;
		GlobalControl.Instance.savedGameData.addition.level = savedGameData.addition.level;

		// save all subtraction game data
		GlobalControl.Instance.savedGameData.subtraction.correctAnswers = savedGameData.subtraction.correctAnswers;
		GlobalControl.Instance.savedGameData.subtraction.increaseRange = savedGameData.subtraction.increaseRange;
		GlobalControl.Instance.savedGameData.subtraction.level = savedGameData.subtraction.level;

		// save all counting game data
		GlobalControl.Instance.savedGameData.counting.correctAnswers = savedGameData.counting.correctAnswers;
		GlobalControl.Instance.savedGameData.counting.increaseRange = savedGameData.counting.increaseRange;
		GlobalControl.Instance.savedGameData.counting.level = savedGameData.counting.level;

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

		// create a file with the given filename and save serialized game data
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/" + Instance.filename + ".gd");
		bf.Serialize (file, Instance.savedGameData);
		file.Close ();
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

		// ensures the file was found
		if(File.Exists(Application.persistentDataPath + "/" + Instance.filename + ".gd")) {
			
			// deserialize file and store for future use
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + Instance.filename + ".gd", FileMode.Open);
			Instance.savedGameData = (Game)bf.Deserialize(file);
			file.Close();
		}
	}
}
