using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : Item {

	public int addHealth;

	// Use this for initialization
	void Start () {
		isStackable = true;
		base.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Use: use the item. 
	public override void Use() {
		Debug.Log ("health item");
		GameController.gameController.health += addHealth;
	}
}
