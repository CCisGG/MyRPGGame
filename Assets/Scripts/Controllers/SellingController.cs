﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellingController : MonoBehaviour {
    
    private static SellingController sellingController;

    public static SellingController Controller
    {
        get { return sellingController; }
    }

    private void Initialize()
    {
        if (sellingController == null)
        {
            DontDestroyOnLoad(gameObject);
            sellingController = this;
        }
        else if (sellingController != this)
        {
            Destroy(gameObject);
        }
    }

	public void Sell() {
        Slot fromSlot = InventoryController.Controller.FromSlot;
		if (fromSlot == null || fromSlot.GetCurrentItem () == null) {
			Debug.Log ("Fromslot is null");
            InventoryController.Controller.FromSlot = null;
			return;
		}
		if (!fromSlot.GetCurrentItem ().IsSoldable) {
			Debug.Log ("Item " + fromSlot.GetCurrentItem () + " cannot be sold");
		} else {
            GameController.Controller.gold += fromSlot.GetCurrentItem ().price * fromSlot.GetItems ().Count;
			fromSlot.ClearSlot();
			Destroy (fromSlot.GetComponent<Item> ());
		}
		fromSlot.GetComponent<Image> ().color = Color.white;
        InventoryController.Controller.FromSlot = null;
		Inventory.EmptySlots++;
	}
}
