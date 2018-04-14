using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0 ) + this.transform.position;
	}
}
