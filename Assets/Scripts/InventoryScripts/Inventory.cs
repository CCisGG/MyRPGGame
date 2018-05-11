// Author: Hao Geng

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
        if (InventoryController.Controller.HoldingSlots == null) {
			return "Null holding slots";
		}
		String res = "";
		for (int i = 0; i < slotCount; i++) {
			res += " |slot " + i + ": ";
            if (i < InventoryController.Controller.HoldingSlots.Count) {
                res += InventoryController.Controller.HoldingSlots [i].ToString ();
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
                GameObject slotObject =	(GameObject) Instantiate (InventoryController.Controller.slotPrefab);

                if (InventoryController.Controller.HoldingSlots != null) {
					// Inventory and Slots Existed: 
					// The "SLOT object (NOT game object)" should be fetched from slot controller

					Slot newSlot = slotObject.GetComponent<Slot> ();
                    Slot oldSlot = InventoryController.Controller.HoldingSlots[currentCount];
					newSlot.AddItems (oldSlot.GetItems());
				}


				newHoldingSlots.Add (slotObject.GetComponent<Slot>());


				slotObject.name = "Slot" + currentCount;
				slotObject.transform.SetParent (this.transform.parent);

				RectTransform slotRect = slotObject.GetComponent<RectTransform> ();

				slotRect.anchoredPosition = inventoryRect.anchoredPosition + new Vector2(slotPaddingLeft * (x + 1) + slotSize * x,
										-slotPaddingTop * (y + 1) - slotSize * y);

				slotRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, slotSize);
				slotRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, slotSize);
				currentCount++;

			}
		}
        InventoryController.Controller.HoldingSlots = newHoldingSlots;
	}

	// AddItem: add the item to this inventory.
	public bool AddItem(Item item) {
		if (!item.IsStackable()) {
			PlaceEmpty (item);
		} else {

			// Check if any slots has the same item with the current one.
			// If so, stack them together in one slot.
            foreach (Slot slot in InventoryController.Controller.HoldingSlots) {
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
        if (InventoryController.Controller.FromSlot == null) {
			Debug.Log ("fromSlot == null");
			if (!clicked.GetComponent<Slot> ().IsEmpty ()) {
                InventoryController.Controller.FromSlot = clicked.GetComponent<Slot> ();
                InventoryController.Controller.FromSlot.GetComponent<Image> ().color = Color.gray;

			}
        } else if (InventoryController.Controller.ToSlot == null) {
			Debug.Log ("toSlot == null");
            InventoryController.Controller.ToSlot = clicked.GetComponent<Slot> ();
		} 

        if (InventoryController.Controller.FromSlot != null && InventoryController.Controller.ToSlot != null){
			Debug.Log ("both != null");
            Stack<Item> tmpToSlot = new Stack<Item> (InventoryController.Controller.ToSlot.GetItems ());
            InventoryController.Controller.ToSlot.AddItems (InventoryController.Controller.FromSlot.GetItems());

			if (tmpToSlot.Count == 0) {
                InventoryController.Controller.FromSlot.ClearSlot ();
			} else {
                InventoryController.Controller.FromSlot.AddItems (tmpToSlot);
			}

            InventoryController.Controller.FromSlot.GetComponent<Image> ().color = Color.white;
            InventoryController.Controller.ToSlot = null;
            InventoryController.Controller.FromSlot = null;
		}



	}

	// PlaceEmpty: Place an item to an empty slot (Traverse and find empty slots). 
	private bool PlaceEmpty(Item item) {
		if (emptySlots > 0) {
            foreach (Slot slot in InventoryController.Controller.HoldingSlots) {
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
