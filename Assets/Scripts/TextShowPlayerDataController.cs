﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShowPlayerDataController : MonoBehaviour {
	public Text playerDataText;
	// Use this for initialization
	void Start () {
		showPlayerData();
	}
	
	// Update is called once per frame
	void Update () {
		showPlayerData();
	}

	void showPlayerData() {
		GameController controller = GameController.gameController;
		playerDataText.text = "health: " + controller.health + "   Exp: " + controller.experience;
	}
}