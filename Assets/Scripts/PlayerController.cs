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
	}

	void OnTriggerStay2D (Collider2D other) {
		if (!other.CompareTag ("Item"))
			return;
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log ("Picking up " + other.GetComponent<Item> ());
			inventory.AddItem (other.GetComponent<Item> ());
			//			GameObject.Destroy (item);
		}
	}
}
