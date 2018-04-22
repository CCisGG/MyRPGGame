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

interface ItemInterface {
	// How to use the Item, should be defined in every child classes
	void Use ();
}


public abstract class Item : MonoBehaviour, ItemInterface {

	public ItemType type;

	public int price;

	// The image display in normal condition.
	public Sprite spriteNeutral;

	// The image display in "highlighed" condition.
	public Sprite spriteHighlighted;

	// Whether the item could be stacked together or not.
	protected bool isStackable;

	// Whether the item could be sold (or delete).
	private bool isSoldable;

	// Max size of the item.
	private int maxSize = 30;

	// Start: initialize "isStackable" in terms of type.
	void Start() {
		Initialize ();
	}

	public void Initialize() {
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

		isSoldable = isStackable;
		Debug.Log (isStackable);
	}

	// How to use the Item, should be defined in every child classes
	public abstract void Use ();

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
