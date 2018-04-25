using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonsterController : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public float nextAttack;
	public float attackRate;
	public float attackSpeed;
	public float health;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void remoteAttack(GameObject ball) {
		if (Time.time > nextAttack) {
			nextAttack = Time.time + attackRate;
			GameObject balls = Instantiate (ball, transform.position, transform.rotation)as GameObject;
			Vector3 direction = (player.transform.position - this.transform.position);
			balls.GetComponent<Rigidbody2D> ().velocity = direction * attackSpeed;
			if (balls != null) {
				Destroy (balls, 2);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Hurt player.
	public void OnTriggerEnter2D(Collider2D other, int hurt) {
		if (other.CompareTag ("Player")) {
			if (GameController.gameController.health >= hurt) {
				GameController.gameController.health -= hurt;
			} else {
				GameObject.Destroy(player);
			}
		}

	}
}
