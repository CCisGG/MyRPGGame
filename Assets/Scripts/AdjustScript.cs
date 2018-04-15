using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScript : MonoBehaviour {

	void OnGUI() {
		if (GUI.Button (new Rect (10, 100, 100, 30), "Health UP")) {
			GameController.gameController.health += 10;
		}
		if (GUI.Button (new Rect (10, 140, 100, 30), "Health Down")) {
			GameController.gameController.health -= 10;
		}
		if (GUI.Button (new Rect (10, 180, 100, 30), "Exp UP")) {
			GameController.gameController.experience += 10;
		}
		if (GUI.Button (new Rect (10, 220, 100, 30), "Exp Down")) {
			GameController.gameController.experience -= 10;
		}

		if (GUI.Button (new Rect (10, 300, 100, 30), "Save")) {
			GameController.gameController.Save();
		}
		if (GUI.Button (new Rect (10, 340, 100, 30), "Load")) {
			GameController.gameController.Load();
		}
	}
}
