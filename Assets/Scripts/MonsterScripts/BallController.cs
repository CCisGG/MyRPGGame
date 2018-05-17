using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    // Use this for initialization
    private int attackHurt;
	public GameObject explosion;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int AttackHurt {
        set { attackHurt = value; }
        get {return  attackHurt; }
    }

	// Hurt player.
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
            Debug.Log("attackHurt " + attackHurt);
            if (GameController.Controller.health >= attackHurt) {
                GameController.Controller.health -= attackHurt;
			} else {
                GameController.Controller.health = 0;
			}
			GameObject explosions = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
			GameObject.Destroy (gameObject);
			GameObject.Destroy (explosions, 0.1f);
		}
		
	}
}
