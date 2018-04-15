using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraContoller : MonoBehaviour {

	public GameObject player; // Public 变量在 Unity 里可以通过拖拽来赋值
	private Vector3 offset;

	// Record the initial offset.
	void Start () {
		offset = this.transform.position - player.transform.position;
	}

	// Keep the offset when player is moving
	void Update () {
		this.transform.position = offset + player.transform.position;
	}

}
