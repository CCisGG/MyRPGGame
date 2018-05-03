using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BusinessmanController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	// OnPointerClick: Action taken when right-click the mouse.
	public void OnPointerClick(PointerEventData eventData) {
		if (eventData.button == PointerEventData.InputButton.Left) {
			Debug.Log ("Triggered");
			Sell ();
		}
	}

	public void Sell() {
		GameController.gameController.gold += InventoryController.GetInventoryController().FromSlot.GetCurrentItem().price;
	}

//	void OnMouseDown() {
//		Debug.Log ("Mouse Down");
//		GameController.gameController.gold += InventoryController.GetInventoryController().FromSlot.GetCurrentItem().price;
//	}
}
