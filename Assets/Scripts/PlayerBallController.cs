using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBallController : MonoBehaviour {

	// Use this for initialization
	public GameObject explosion;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Hurt player.
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Monster")) {
			GameObject explosions = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
			Destroy (gameObject);
			Destroy (explosions, 0.1f);
			MonsterController monster = other.GetComponent<MonsterController> ();
			if (monster.health > 10) {
				monster.health -= 20;
			} else {
                other.GetComponent<MonsterController>().DropItem();
				Destroy (other.gameObject);
			}
		}
		
	}
}
