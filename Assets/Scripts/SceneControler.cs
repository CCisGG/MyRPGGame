using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneControler : MonoBehaviour {

	public string sceneName;

	// Change scene on collision
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			SceneManager.LoadScene (sceneName);
		}
	}
}
