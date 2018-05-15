using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MineSceneManager : MonoBehaviour {

	// Change scene on collision
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
            FindObjectOfType<LevelController>().Level++;
            Debug.Log("Level #" + FindObjectOfType<LevelController>().Level);
			SceneManager.LoadScene ("MineScene");
		}
	}
}
