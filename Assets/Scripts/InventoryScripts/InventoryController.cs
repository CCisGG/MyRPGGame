using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour {

	private static InventoryController inventoryController;

	public GameObject slotPrefab;

	public Canvas canvas;

	private Slot fromSlot, toSlot;

	public Slot FromSlot {
		get { return fromSlot; }
		set { fromSlot = value; }
	}

	public Slot ToSlot {
		get { return toSlot; }
		set { toSlot = value; }
	}

	private GameObject clicked;

	public Text stackText;

	private Slot movingSlot;

	public EventSystem eventSystem;

	public GameObject toopTip;

	public static InventoryController GetInventoryController() {
		if (inventoryController == null) {
			inventoryController = FindObjectOfType<InventoryController> ();
		}
		return inventoryController;
	}


	void Awake () {
		if (inventoryController == null) {
			DontDestroyOnLoad (gameObject);
			inventoryController = this;
		} else if (inventoryController != this) {
			Destroy (gameObject);
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
