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

	// Return if the slot has no items.
	public bool IsEmpty() {
		return items.Count == 0; 
	}

	// Return if the slots could still filled in same items.
	public bool IsAvailable() {
		return GetCurrentItem().maxSize > items.Count;
	}

	public Item GetCurrentItem() {
		return items.Peek ();
	}

	public void AddItem(Item item) {
		items.Push (item);

		if (items.Count > 1) {
			stackText.text = items.Count.ToString ();
		}

		ChangeSprite (item.spriteNeutral, item.spriteHighlighted);
	}

	// Action taken when mouse click
	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Left) {
			UseItem ();
		}
	}

	// Change the image of the slot.
	private void ChangeSprite(Sprite spriteNeutral, Sprite spriteHighlighted) {
		GetComponent<Image> ().sprite = spriteNeutral;

		SpriteState st = new SpriteState ();
		st.highlightedSprite = spriteHighlighted;
		st.pressedSprite = spriteNeutral;

		GetComponent<Button> ().spriteState = st;
	}
		
	private void UseItem() {
		// Return if there is no item in this slot.
		if (IsEmpty ())
			return;

		items.Pop ().Use ();
		stackText.text = items.Count > 1 ? items.Count.ToString () : string.Empty;

		if (IsEmpty ()) {
			ChangeSprite (slotEmpty, slotHighlighted);
			InventoryController.EmptySlots++;
		}
	}
}
