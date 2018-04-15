using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GameController: Save data
 *
 */

public class GameController : MonoBehaviour {

	public static GameController gameController;

	public float health;
	public float experience;

	// Use this for initialization
	void Awake () {
		if (gameController == null) {
			DontDestroyOnLoad (gameObject);
			gameController = this;
		} else if (gameController != this) {
			Destroy (gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
