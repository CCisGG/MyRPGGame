using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Hurt player.
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			if (GameController.gameController.health >= 10) {
				GameController.gameController.health -= 10;
			} else {
				GameController.gameController.health = 0;
			}
			GameObject.Destroy(gameObject);
		}
		
	}
}
