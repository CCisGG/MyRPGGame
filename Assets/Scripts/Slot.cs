using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

	private Stack<Item> items;

	public bool IsEmpty() {
		return items.Count == 0;
	}
}
