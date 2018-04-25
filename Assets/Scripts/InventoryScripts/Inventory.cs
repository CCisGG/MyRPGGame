﻿// Author: Hao Geng

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	private RectTransform inventoryRect;

	private float inventoryWidth, inventoryHeight;

	public int slotCount;

	public int rows;

	public float slotPaddingLeft, slotPaddingTop;

	public float slotSize;

	private static int emptySlots;

	void Awake () {
//		if (inventory == null) {
//			DontDestroyOnLoad (gameObject);
//			inventory = this;
//		} else if (inventory != this) {
//			Destroy (gameObject);
//		}

	}

	public static int EmptySlots {
		get { return emptySlots; }
		set { emptySlots = value; }
	}
		
	// Start: create the layout.
	void Start () {
		CreateLayout ();
	}

	// Update: is called once per frame
	void Update () {

	}

	public String ToString() {
		if (InventoryController.GetInventoryController ().HoldingSlots == null) {
			return "Null holding slots";
		}
		String res = "";
		for (int i = 0; i < slotCount; i++) {
			res += " |slot " + i + ": ";
			if (i < InventoryController.GetInventoryController ().HoldingSlots.Count) {
				res += InventoryController.GetInventoryController ().HoldingSlots [i].ToString ();
			}
		}
		return res;
	}

	// CreateLayout: Setup the layout for inventory and slots. 
	private void CreateLayout() {
		Debug.Log (ToString());
		emptySlots = slotCount;

		inventoryWidth = (slotCount / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
		inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;
		inventoryRect = GetComponent<RectTransform> ();

		inventoryRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, inventoryWidth);
		inventoryRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, inventoryHeight);

		int columns = slotCount / rows;

		List<Slot> newHoldingSlots = new List<Slot> ();

		int currentCount = 0;
		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < columns; x++) {
				GameObject slotObject =	(GameObject) Instantiate (InventoryController.GetInventoryController().slotPrefab);

				if (InventoryController.GetInventoryController ().HoldingSlots != null) {
					// Inventory and Slots Existed: 
					// The "SLOT object (NOT game object)" should be fetched from slot controller

					Slot newSlot = slotObject.GetComponent<Slot> ();
					Slot oldSlot = InventoryController.GetInventoryController ().HoldingSlots[currentCount];
					newSlot.AddItems (oldSlot.GetItems());
				}


				newHoldingSlots.Add (slotObject.GetComponent<Slot>());


				slotObject.name = "Slot" + currentCount;
				slotObject.transform.SetParent (this.transform.parent);

				RectTransform slotRect = slotObject.GetComponent<RectTransform> ();

				slotRect.anchoredPosition = inventoryRect.anchoredPosition + new Vector2(slotPaddingLeft * (x + 1) + slotSize * x,
										-slotPaddingTop * (y + 1) - slotSize * y);

//				Debug.Log ("parent postion" + this.transform.parent.position + " " + slotRect.transform.parent.position + " " + inventoryRect.transform.parent.position);
//				Debug.Log (x + " " + y + " " + slotRect.transform.position + " " + slotRect.localPosition);
//				Debug.Log (x + " " + y + " " + inventoryRect.transform.position + " " + inventoryRect.localPosition);
				slotRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, slotSize);
				slotRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, slotSize);
				currentCount++;

			}
		}
		InventoryController.GetInventoryController().HoldingSlots = newHoldingSlots;
	}

	// AddItem: add the item to this inventory.
	public bool AddItem(Item item) {
		if (!item.IsStackable()) {
			PlaceEmpty (item);
		} else {

			// Check if any slots has the same item with the current one.
			// If so, stack them together in one slot.
			foreach (Slot slot in InventoryController.GetInventoryController().HoldingSlots) {
				if (!slot.IsEmpty ()) {
					if (slot.GetCurrentItem ().type == item.type && slot.IsAvailable ()) {
						slot.AddItem (item);
						Debug.Log (ToString());
						return true;
					}
				}
			}

			// Cannot find any available same-item slots, place item to a new slot.
			PlaceEmpty (item);
		}
		Debug.Log (ToString());
		return true;
	}

	// MoveItem: Move item by click. 
	// 			Note: In Unity page, should add the this controller and this function to click-list; 
	public void MoveItem(GameObject clicked) {
		if (InventoryController.GetInventoryController ().FromSlot == null) {
			Debug.Log ("fromSlot == null");
			if (!clicked.GetComponent<Slot> ().IsEmpty ()) {
				InventoryController.GetInventoryController ().FromSlot = clicked.GetComponent<Slot> ();
				InventoryController.GetInventoryController ().FromSlot.GetComponent<Image> ().color = Color.gray;

			}
		} else if (InventoryController.GetInventoryController ().ToSlot == null) {
			Debug.Log ("toSlot == null");
			InventoryController.GetInventoryController ().ToSlot = clicked.GetComponent<Slot> ();
		} 

		if (InventoryController.GetInventoryController ().FromSlot != null && InventoryController.GetInventoryController ().ToSlot != null){
			Debug.Log ("both != null");
			Stack<Item> tmpToSlot = new Stack<Item> (InventoryController.GetInventoryController ().ToSlot.GetItems ());
			InventoryController.GetInventoryController ().ToSlot.AddItems (InventoryController.GetInventoryController ().FromSlot.GetItems());

			if (tmpToSlot.Count == 0) {
				InventoryController.GetInventoryController ().FromSlot.ClearSlot ();
			} else {
				InventoryController.GetInventoryController ().FromSlot.AddItems (tmpToSlot);
			}

			InventoryController.GetInventoryController ().FromSlot.GetComponent<Image> ().color = Color.white;
			InventoryController.GetInventoryController ().ToSlot = null;
			InventoryController.GetInventoryController ().FromSlot = null;
		}



	}

	// PlaceEmpty: Place an item to an empty slot (Traverse and find empty slots). 
	private bool PlaceEmpty(Item item) {
		if (emptySlots > 0) {
			foreach (Slot slot in InventoryController.GetInventoryController().HoldingSlots) {
				if (slot.IsEmpty ()) {
					Debug.Log ("Placing empty: " + item);
					slot.AddItem (item);

					emptySlots--;
					return true;
				}
			}
			return false;
		}
		return false;
	} 

}
