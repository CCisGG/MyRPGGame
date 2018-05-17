using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    
    private static LevelController levelController;

    private static int level;

    private static GameObject nextLevelCave;

    public int Level {
        get { return level; }
        set { level = value; }
    }

    public static LevelController Controller
    {
        get { return levelController; }
    }

    private void Initialize()
    {
        if (levelController == null)
        {
            DontDestroyOnLoad(gameObject);
            levelController = this;
        }
        else if (levelController != this)
        {
            Destroy(gameObject);
        }
    }

	void Awake()
	{
        Initialize();
	}

	void Start()
	{
        level = 0;
	}

    public static void DeactivateCave(GameObject cave) {
        nextLevelCave = cave;
        nextLevelCave.SetActive(false);
    }

    public static void ActivateCave() {
        // First Check if next cave still exists
        if (nextLevelCave != null) { nextLevelCave.SetActive(true); }
    }
}
