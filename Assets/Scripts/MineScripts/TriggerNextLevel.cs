using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextLevel : MonoBehaviour {

	// Use this for initialization
	void OnDestroy () {
        Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name.Equals("MineScene")) {
            Debug.Log("Trigger");
            LevelController.ActivateCave();
        }
	}
	
}
