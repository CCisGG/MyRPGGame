using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// NextLevelCave which will be used to controller the level when digging mine.
public class NextLevelCave : MonoBehaviour {

    private void Start()
    {
        LevelController.DeactivateCave(this.gameObject);
    }


	// Change scene on collision
	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
            FindObjectOfType<LevelController>().Level++;
            Debug.Log("Level #" + FindObjectOfType<LevelController>().Level);
			SceneManager.LoadScene ("MineScene");
		}
	}
}
