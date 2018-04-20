// Author: Hao Geng

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Should follow alphabetical order, for convenience.
public enum ItemType {
	Animal,
	Craft, Crop, Collection,
	Farage, Fish, Food, Fruit, Furniture,
	Magic, Medicine, Minernal,
	OtherNonStackable, OtherStackable,
	Tool,
	Vegetable,
	Weapon,
}; 


public class Item : MonoBehaviour {

	public ItemType type;

	// The image display in normal condition.
	public Sprite spriteNeutral;

	// The image display in "highlighed" condition.
	public Sprite spriteHighlighted;

	// Whether the item could be stacked together or not.
	private bool isStackable;

	// Max size of the item.
	private int maxSize = 30;

	// Start: initialize "isStackable" in terms of type.
	void Start() {
		switch (type) {
			case ItemType.Weapon:
			case ItemType.Tool:
			case ItemType.Furniture:
			case ItemType.OtherNonStackable:
				isStackable = false;
				break;
			default:
				isStackable = true;
				break;
		}
	}

	// Use: use the item. 
	public void Use() {
		switch (type) {
			case ItemType.Medicine:
				Debug.Log ("health item");
				GameController.gameController.health += 10;
				break;
			case ItemType.Weapon:
				Debug.Log("Weapon item");
				break;
		}
	}

	// GetMaxSize: get max stackable size for the item.
	public int GetMaxSize() {
		if (isStackable) {
			return maxSize;
		} else
			return 1;
	}

	// IsStackable: return if the item is stackable.
	public bool IsStackable() {
		return isStackable;
	}
}
