using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    
    private static LevelController levelController;

    private static int level;

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
        level = 1;
	}
}
