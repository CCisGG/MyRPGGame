using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public GameObject ball;
	private float nextAttack;
	private float attackRate;
	private float attackSpeed;
	private bool startAttack;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		nextAttack = Time.time;
		attackRate = 1.5f;
		attackSpeed = 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
		// Accept the move signal from keyboard "input", and move the position
		//GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (startAttack && Time.time > nextAttack) {
			nextAttack = Time.time + attackRate;
			GameObject balls = Instantiate (ball, transform.position, transform.rotation)as GameObject;
			Vector3 direction = (player.transform.position - this.transform.position);
			balls.GetComponent<Rigidbody2D> ().velocity = direction * attackSpeed;
			Destroy (balls, 2);
		}
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
		if (other.CompareTag ("Player")) {
			if (GameController.gameController.health >= 30) {
				GameController.gameController.health -= 30;
			} else {
				GameObject.Destroy(player);
			}
		}
	}
}
