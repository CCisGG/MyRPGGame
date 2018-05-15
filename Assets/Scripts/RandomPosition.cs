using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour {

    public float x;
    public float y;

	// Use this for initialization
	void Start () {
        this.gameObject.transform.position = new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
