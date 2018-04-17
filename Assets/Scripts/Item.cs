using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Sword, Health}; 


public class Item : MonoBehaviour {

	public ItemType type;

	public Sprite sprite;

	public int maxSize;

	void Use() {
		switch (type) {
			case ItemType.Health:
				Debug.Log ("health item");
				break;
			case ItemType.Sword:
				Debug.Log ("Sword item");
				break;
		}
	}
}
