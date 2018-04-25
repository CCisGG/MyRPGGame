using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurpleMonsterController : MonsterController {

	// Use this for initialization
	public GameObject ball;
	private bool startAttack;
	private int touchHurt;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		base.nextAttack = Time.time;
		base.attackRate = 1.5f;
		base.attackSpeed = 2.5f;
		touchHurt = 30;
	}
	
	// Update is called once per frame
	void attack() {
		if (startAttack) {
			base.remoteAttack (ball);
		}
	}

	void Update () {
		// Move and attack
		attack();
		Vector3 player_position = player.transform.position;
		Vector3 position = this.transform.position;
		float x_abs = System.Math.Abs (player_position.x - position.x);
		float y_abs = System.Math.Abs (player_position.y - position.y);
		double distance = x_abs + y_abs;
		if (distance > 12) {
			startAttack = false;
		}
		else if (distance < 8) {
			startAttack = true;
			float x_direction = ((player_position.x - position.x) / x_abs) / 20;
			float y_direction = ((player_position.y - position.y) / y_abs) / 20;
				//this.transform.position = new Vector3 (x_direction, y_direction, 0) + this.transform.position;
			if (x_abs > y_abs) {
				this.transform.position = new Vector3 (x_direction, 0, 0) + this.transform.position;
			} else {
				this.transform.position = new Vector3 (0, y_direction, 0) + this.transform.position;
			}
		}


	}

	// Hurt player.
	void OnTriggerEnter2D(Collider2D other) {
		base.OnTriggerEnter2D(other,touchHurt);
	}
}
