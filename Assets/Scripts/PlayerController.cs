using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public InventoryController inventory;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Accept the move signal from keyboard "input", and move the position
		this.transform.position = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0 ) + this.transform.position;
	}

	// Pick up items on collision.
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Item")) {
			inventory.AddItem (other.GetComponent<Item>());
		}
	}
}
