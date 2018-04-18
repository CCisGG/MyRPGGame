using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Weapon, Health, Mana}; 


public class Item : MonoBehaviour {

	public ItemType type;

	public Sprite spriteNeutral;

	public Sprite spriteHighlighted;

	public int maxSize;

	public void Use() {
		switch (type) {
			case ItemType.Health:
				Debug.Log ("health item");
				GameController.gameController.health += 10;
				break;
			case ItemType.Weapon:
				Debug.Log("Weapon item");
				break;
		}
	}
}
