using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellingController : MonoBehaviour {

	SellingController sellingController;

	void Awake () {
		if (sellingController == null) {
			DontDestroyOnLoad (gameObject);
			sellingController = this;
		} else if (sellingController != this) {
			Destroy (gameObject);
		}

	}

	public void Sell() {
		Slot fromSlot = InventoryController.GetInventoryController ().FromSlot;
		if (fromSlot == null || fromSlot.GetCurrentItem () == null) {
			Debug.Log ("Fromslot is null");
			InventoryController.GetInventoryController ().FromSlot = null;
			return;
		}
		if (!fromSlot.GetCurrentItem ().IsSoldable) {
			Debug.Log ("Item " + fromSlot.GetCurrentItem () + " cannot be sold");
		} else {
			GameController.gameController.gold += fromSlot.GetCurrentItem ().price * fromSlot.GetItems ().Count;
			fromSlot.ClearSlot();
			Destroy (fromSlot.GetComponent<Item> ());
		}
		fromSlot.GetComponent<Image> ().color = Color.white;
		InventoryController.GetInventoryController ().FromSlot = null;
		Inventory.EmptySlots++;
	}
}
