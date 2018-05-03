using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
		if (!fromSlot.GetCurrentItem ().IsSoldable) {
			Debug.Log ("Item " + fromSlot.GetCurrentItem() + " cannot be sold");
			return;
		}
		GameController.gameController.gold += fromSlot.GetCurrentItem().price;
		fromSlot.ClearSlot();
		InventoryController.GetInventoryController ().FromSlot = null;
	}
}
