using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : MonoBehaviour {

	public InventoryController inventory;

	private Collider2D overlapItem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Accept the move signal from keyboard "input", and move the position
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
