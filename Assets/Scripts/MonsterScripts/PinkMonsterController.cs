using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinkMonsterController : MonsterController {

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame

	void Update () {
		// Move and attack
		//attack();
		Vector3 player_position = player.transform.position;
		Vector3 position = this.transform.position;
		float x_abs = System.Math.Abs (player_position.x - position.x);
		float y_abs = System.Math.Abs (player_position.y - position.y);
		double distance = x_abs + y_abs;
		if (distance < 10) {

			float x_direction = ((player_position.x - position.x) / x_abs) / 5;
			float y_direction = ((player_position.y - position.y) / y_abs) / 5;
				//this.transform.position = new Vector3 (x_direction, y_direction, 0) + this.transform.position;
			if (x_abs > y_abs) {
				this.transform.position = new Vector3 (-1*x_direction, 0, 0) + this.transform.position;
			} else {
				this.transform.position = new Vector3 (0, -1*y_direction, 0) + this.transform.position;
			}

		}
	



	}

	// Hurt player.
	void OnCollisionEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			GameController.gameController.health += 5;
		}
	}
}
