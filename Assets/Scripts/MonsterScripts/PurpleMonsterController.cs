using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurpleMonsterController : MonsterController {

	// Use this for initialization
	public GameObject ball;

	//private int touchHurt;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		base.nextAttack = Time.time;
		base.attackRate = 1.5f;
        if (attackSpeed == 0f) {
            attackSpeed = 1.5f; // Default
        }
		base.touchHurt = 20;
	}
	
	// Update is called once per frame
	void Attack() {
		if (startAttack) {
			base.remoteAttack (ball);
		}
	}

	void Update () {
        // Move and attack
        base.Move();
		Attack();
	}
}
