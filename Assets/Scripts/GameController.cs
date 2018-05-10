using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


/*
 * GameController: Save data
 *
 */

public class GameController : MonoBehaviour {

	public static GameController gameController;

	public float health;
	public float experience;
	public float gold;

	void Awake () {
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			gameController = this;
		} else if (gameController != this) {
			Destroy (gameObject);
		}

	}
	
	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();
		data.health = health;
		data.experience = experience;
        data.gold = gold;

		bf.Serialize (file, data);
		file.Close();
	}

	public void Load() {
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData) bf.Deserialize (file);
			file.Close();

			health = data.health;
			experience = data.experience;
            gold = data.gold;
		}


	}
}


[Serializable]
class PlayerData {
	public float health;
	public float experience;
    public float gold;
}