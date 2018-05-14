﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurpleMonsterController : MonsterController {

	// Use this for initialization
	public GameObject ball;

	private int touchHurt;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		base.nextAttack = Time.time;
		base.attackRate = 1.5f;
        if (attackSpeed == 0f) {
            attackSpeed = 2.5f; // Default
        }
		touchHurt = 30;
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

	// Hurt player.
	void OnTriggerEnter2D(Collider2D other) {
		base.OnTriggerEnter2D(other,touchHurt);
	}


}
