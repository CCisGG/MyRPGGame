using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryController : MonoBehaviour {

	private RectTransform inventoryRect;

	private float inventoryWidth, inventoryHeight;

	public int slots;

	public int rows;

	public float slotPaddingLeft, slotPaddingTop;

	public float slotSize;

	public GameObject slotPrefab;

	private List<GameObject> allSlots;

	private static int emptySlots;

	public static int EmptySlots {
		get { return emptySlots; }
		set { emptySlots = value; }
	}

	// Use this for initialization
	void Start () {
		CreateLayout ();
	}

	// Update is called once per frame
	void Update () {

	}

	// Setup the layout for inventory and slots. 
	private void CreateLayout() {
		emptySlots = slots;

		allSlots = new List<GameObject> ();

		inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
		inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;
		inventoryRect = GetComponent<RectTransform> ();

		inventoryRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, inventoryWidth);
		inventoryRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, inventoryHeight);

		int columns = slots / rows;

		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < columns; x++) {
				GameObject newSlot = (GameObject) Instantiate (slotPrefab);

				newSlot.name = "Slot";
				newSlot.transform.SetParent (this.transform.parent);

				RectTransform slotRect = newSlot.GetComponent<RectTransform> ();

				slotRect.anchoredPosition = inventoryRect.anchoredPosition + new Vector2(slotPaddingLeft * (x + 1) + slotSize * x,
										-slotPaddingTop * (y + 1) - slotSize * y);

//				Debug.Log ("parent postion" + this.transform.parent.position + " " + slotRect.transform.parent.position + " " + inventoryRect.transform.parent.position);
//				Debug.Log (x + " " + y + " " + slotRect.transform.position + " " + slotRect.localPosition);
//				Debug.Log (x + " " + y + " " + inventoryRect.transform.position + " " + inventoryRect.localPosition);
				slotRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, slotSize);
				slotRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, slotSize);

				allSlots.Add (newSlot);
			}
		}

	}

	public bool AddItem(Item item) {
		if (item.maxSize == 1) {
			PlaceEmpty (item);
		} else {

			// Check if any slots has the same item with the current one.
			// If so, stack them together in one slot.
			foreach (GameObject slot in allSlots) {
				Slot tmp = slot.GetComponent<Slot> ();
				if (!tmp.IsEmpty ()) {
					if (tmp.GetCurrentItem ().type == item.type && tmp.IsAvailable ()) {
						tmp.AddItem (item);
						return true;
					}
				}
			}

			// Cannot find any available same-item slots, place item to a new slot.
			PlaceEmpty (item);
		}

		return true;
	}

	// Place an item to an empty slot (Traverse and find empty slots). 
	private bool PlaceEmpty(Item item) {
		if (emptySlots > 0) {
			foreach (GameObject slot in allSlots) {
				Slot tmp = slot.GetComponent <Slot>();
				if (tmp.IsEmpty ()) {

					tmp.AddItem (item);

					emptySlots--;
					return true;
				}
			}
			return false;
		}
		return false;
	} 

}
