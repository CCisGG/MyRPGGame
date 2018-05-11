using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InventoryController : MonoBehaviour {

    private static InventoryController inventoryController;

    public static InventoryController Controller
    {
        get { return inventoryController; }
    }

    private void Initialize()
    {
        if (inventoryController == null)
        {
            DontDestroyOnLoad(gameObject);
            inventoryController = this;
        }
        else if (inventoryController != this)
        {
            Destroy(gameObject);
        }
    }


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

	void Awake () {
        Initialize();
	}

	public void Sell() {
		Debug.Log ("Triggered");
        GameController.Controller.gold += FromSlot.GetCurrentItem().price;
		FromSlot.ClearSlot ();
		FromSlot = null;
	}

	public void ReturnToPreviousScene(string sceneName) {
		if (sceneName != null) {
			Debug.Log ("sceneName= " + sceneName);
			SceneManager.LoadScene (sceneName);
		}
	}
}
