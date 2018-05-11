using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScript : MonoBehaviour {

	void OnGUI() {
		if (GUI.Button (new Rect (10, 100, 100, 30), "Health UP")) {
            GameController.Controller.health += 10;
		}
		if (GUI.Button (new Rect (10, 140, 100, 30), "Health Down")) {
            GameController.Controller.health -= 10;
		}
		if (GUI.Button (new Rect (10, 180, 100, 30), "Exp UP")) {
            GameController.Controller.experience += 10;
		}
		if (GUI.Button (new Rect (10, 220, 100, 30), "Exp Down")) {
            GameController.Controller.experience -= 10;
		}

		if (GUI.Button (new Rect (10, 300, 100, 30), "Save")) {
            GameController.Controller.Save();
		}
		if (GUI.Button (new Rect (10, 340, 100, 30), "Load")) {
            GameController.Controller.Load();
		}
	}
}
