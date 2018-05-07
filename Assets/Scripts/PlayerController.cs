using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : MonoBehaviour {

	public Inventory inventory;

	private Collider2D overlapItem;

	// Use this for initialization
	public GameObject player_ball;
	private float nextFire;
	private Vector3 direction;
	private float attackSpeed;
    private Animator animator;
    void Start () {
		attackSpeed = 10f;
        animator = transform.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		// Accept the move signal from keyboard "input", and move the position
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + 0.5f;
			GameObject player_balls = Instantiate (player_ball, transform.position, transform.rotation) as GameObject;
			player_balls.GetComponent<Rigidbody2D> ().velocity = direction * attackSpeed;
			if (player_balls != null) {
				Destroy (player_balls, 2);
			}
		}
		if (Input.GetAxis ("Horizontal") > 0) {
			direction = new Vector3(1, 0, 0);
            animator.SetTrigger("walk");
        } else if (Input.GetAxis ("Horizontal") < 0) {
			direction = new Vector3(-1, 0, 0);
		} else if (Input.GetAxis ("Vertical") > 0) {
			direction = new Vector3(0, 1, 0);
		} else if (Input.GetAxis ("Vertical") < 0) {
			direction = new Vector3(0, -1, 0);
		}

        this.transform.position = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0 ) + this.transform.position;
	
		// Only allow one single item in specific range
		DetectPickUp ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		overlapItem = other;
	}

	void OnTriggerExit2D (Collider2D other) {
		overlapItem = null;
	}

	public void DetectPickUp() {
		if (overlapItem == null || !overlapItem.CompareTag ("Item"))
			return;
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log ("Picking up " + overlapItem.GetComponent<Item> ());
			inventory.AddItem (overlapItem.GetComponent<Item> ());
			//			GameObject.Destroy (item);
		}
	}

}
