using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeCave : MonoBehaviour {

    public string sceneName;

	void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag ("Player")) {
            LevelController.Controller.Level = 0;
            Destroy(GameObject.Find("LevelController"));
            SceneManager.LoadScene (sceneName);
        }
    }
}
