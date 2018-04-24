using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : MonoBehaviour {

	public InventoryController inventory;

	// Use this for initialization
	public GameObject player_ball;
	private float nextFire;
	private Vector3 direction;
	private float attackSpeed;
	void Start () {
		attackSpeed = 10f;
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
		} else if (Input.GetAxis ("Horizontal") < 0) {
			direction = new Vector3(-1, 0, 0);
		} else if (Input.GetAxis ("Vertical") > 0) {
			direction = new Vector3(0, 1, 0);
		} else if (Input.GetAxis ("Vertical") < 0) {
			direction = new Vector3(0, -1, 0);
		}
		this.transform.position = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0 ) + this.transform.position;
	
		DetectPickUpAction ();
	}

	// Pick up items on tab Space.
	private bool DetectPickUpAction() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			GameObject item = findClosestObject ("Item");
			if (item != null)
				Debug.Log (item.GetComponent<Item>());
				inventory.AddItem (item.GetComponent<Item>());
//			GameObject.Destroy (item);
			return true;
		}
		return false;
	}

	public GameObject findClosestObject(string tag) {
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag (tag);
		GameObject closestObject = null;
		float minDist = 5;

		foreach (GameObject obj in gameObjects) {
			float dist = Vector3.Distance (obj.transform.position, gameObject.transform.position);

			if (dist < minDist ) {
				minDist = dist;
				closestObject = obj;
			}
		}


		return closestObject;
	}
}
