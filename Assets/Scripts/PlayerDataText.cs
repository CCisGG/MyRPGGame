using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataText : MonoBehaviour {
	// Use this for initialization
	void Start () {
		showPlayerData();
	}
	
	// Update is called once per frame
	void Update () {
		showPlayerData();
	}

	void showPlayerData() {
        GameController controller = GameController.Controller;
		Text playerDataText = GetComponent<Text> ();
		playerDataText.text = "health: " + controller.health
						+ "   Exp: " + controller.experience
						+ "   Gold:" + controller.gold;
	}
}
