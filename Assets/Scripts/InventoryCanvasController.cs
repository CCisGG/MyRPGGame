using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvasController : MonoBehaviour {

	private CanvasScaler scaler;

	public static InventoryCanvasController canvasController;

	void Awake () {
		if (canvasController == null) {
			DontDestroyOnLoad (gameObject);
			canvasController = this;
		} else if (canvasController != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		scaler = GetComponent<CanvasScaler> ();
		scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
	}
}
