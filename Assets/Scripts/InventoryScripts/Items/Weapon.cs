using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

	// Use this for initialization
	void Start () {
		isStackable = false;
		base.Initialize();
	}

	// Update is called once per frame
	void Update () {

	}

	// Use: use the item. 
	public override void Use() {
		Debug.Log("Weapon item");
	}
}
