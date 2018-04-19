using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : MonoBehaviour {

	public InventoryController inventory;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Accept the move signal from keyboard "input", and move the position
		this.transform.position = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0 ) + this.transform.position;
	
		DetectSpace ();
	}

	// Pick up items on Tab Space.
	private bool DetectSpace() {
		if (Input.GetKey(KeyCode.Space)) {
			GameObject item = findClosestObject ("Item");

			if (item != null) 
				inventory.AddItem (item.GetComponent<Item>());
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
