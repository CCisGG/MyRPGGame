using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InventoryController : MonoBehaviour {

	private static InventoryController inventoryController;

	public GameObject slotPrefab;

	public Canvas canvas;

	public Scene previousScene;

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

	private List<Slot> holdingSlots;

	public List<Slot> HoldingSlots {
		get { return holdingSlots; }
		set { holdingSlots = value; }
	}

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

	public void Sell() {
		Debug.Log ("Triggered");
		GameController.gameController.gold += InventoryController.GetInventoryController().FromSlot.GetCurrentItem().price;
	}

	public void ReturnToPreviousScene(string sceneName) {
//		Debug.Log ("Previous Scene name = " + previousScene.name);
//		if (previousScene.name != null || previousScene.name != "") {
//			Debug.Log ("Previous Scene = " + previousScene);
//			SceneManager.LoadScene (previousScene.name);
//		}
		if (sceneName != null) {
			Debug.Log ("sceneName= " + sceneName);
			SceneManager.LoadScene (sceneName);
		}
	}
}
