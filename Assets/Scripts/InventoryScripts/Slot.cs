﻿// Author: Hao Geng

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/**
 *
 * Slot: Inventory Slot. Take the control of "item number" display, fill in and fetch items.
 **/ 

public class Slot : MonoBehaviour, IPointerClickHandler {

	// Each slot has a stack, for push and pop items.
	private Stack<Item> items;

	// The "item number" text.
	public Text stackText;

	// The sprite(image) of the empty slot
	public Sprite slotEmpty;

	// The sprite(image) of the highlighted slot. A lot would be highlighted when mouse pointer move to the slot.
	public Sprite slotHighlighted;

	void Start() {
		items = new Stack<Item> ();

		// Set up the stackText size and location.
		RectTransform slotRect = GetComponent<RectTransform> ();
		RectTransform txtRect = stackText.GetComponent<RectTransform> ();

		int txtScaleFactor = (int) (slotRect.sizeDelta.x * 0.6);
		stackText.resizeTextMaxSize = txtScaleFactor;
		stackText.resizeTextMinSize = txtScaleFactor;

		txtRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
		txtRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, slotRect.sizeDelta.y);

	}

	void Update() {
		Item item = GetComponent<Item> ();
		Stack<Item> copyItems = new Stack<Item> ();
		for (int i = 0; i < items.Count; i++) {
			copyItems.Push (item);
		}
		items = copyItems;
	}

	// IsEmpty: Return if the slot has no items.
	public bool IsEmpty() {
		return items.Count == 0; 
	}

	// GetItems: return the whole stack for the item.
	public Stack<Item> GetItems() {
		return this.items;
	}

	// IsAvailable: Return if the slots could still filled in same items.
	public bool IsAvailable() {
		return GetCurrentItem().GetMaxSize() > items.Count;
	}

	// GetCurrentItem: return the current item of this slot.
	public Item GetCurrentItem() {
		return items.Peek ();
	}

	// AddItem: add a single item to this slot.
	public void AddItem(Item item) {
		if (this.GetComponent<Item>() == null) {
			CopyComponent (item, gameObject);
		}
		items.Push (item);

		if (items.Count > 1) {
			stackText.text = items.Count.ToString ();
		}

		ChangeSprite (item.spriteNeutral, item.spriteHighlighted);
	}
		
	// AddItems: add a stack of items to this slot.
	public void AddItems(Stack<Item> items) {
		if (items.Count == 0)
			return;
		
		if (GetComponent<Item> () != null) {
			Destroy (GetComponent<Item> ());
		}

		CopyComponent (items.Peek(), gameObject);

		this.items = new Stack<Item>(items);

		stackText.text = items.Count > 1 ? items.Count.ToString () : string.Empty;

		ChangeSprite (GetCurrentItem().spriteNeutral, GetCurrentItem().spriteHighlighted);
	}

	// OnPointerClick: Action taken when right-click the mouse.
	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Right) {
			UseItem ();
		}
	}

	// ClearSlot: empty this slot.
	public void ClearSlot() {
		items.Clear ();
		ChangeSprite (slotEmpty, slotHighlighted);
		stackText.text = string.Empty;
	}

	// ChangeSprite: Change the image of the slot.
	private void ChangeSprite(Sprite spriteNeutral, Sprite spriteHighlighted) {
		GetComponent<Image> ().sprite = spriteNeutral;

		SpriteState st = new SpriteState ();
		st.highlightedSprite = spriteHighlighted;
		st.pressedSprite = spriteNeutral;

		GetComponent<Button> ().spriteState = st;
	}

	// UseItem: use the item
	private void UseItem() {
		// Return if there is no item in this slot.
		if (IsEmpty ())
			return;

		items.Pop ().Use ();
		stackText.text = items.Count > 1 ? items.Count.ToString () : string.Empty;

		if (IsEmpty ()) {
			ChangeSprite (slotEmpty, slotHighlighted);
			Inventory.EmptySlots++;
		}
	}

	public override string ToString() {
		Item item = null;
		if (items.Count != 0) {
			item = GetCurrentItem ();
			return "Slot contains item " + item + " * " + items.Count; 
		}
		return "Slot is empty";
	}

	T CopyComponent<T>(T original, GameObject destination) where T : Component
	{
		System.Type type = original.GetType();
		Component copy = destination.AddComponent(type);
		System.Reflection.FieldInfo[] fields = type.GetFields();
		foreach (System.Reflection.FieldInfo field in fields)
		{
			field.SetValue(copy, field.GetValue(original));
		}
		return copy as T;
	}
}
