using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraContoller : MonoBehaviour {

    private static CamaraContoller camaraContoller;

    public static CamaraContoller Controller
    {
        get { return camaraContoller; }
    }

    private void Initialize()
    {
        if (camaraContoller == null)
        {
            DontDestroyOnLoad(gameObject);
            camaraContoller = this;
        }
        else if (camaraContoller != this)
        {
            Destroy(gameObject);
        }
    }

	private GameObject player; // Public 变量在 Unity 里可以通过拖拽来赋值
	private Vector3 offset;

	void Awake()
	{
        Initialize();	
	}

	// Record the initial offset.
	void Start () {
        player = FindObjectOfType<PlayerController>().gameObject;
		offset = this.transform.position - player.transform.position;
	}

	// Keep the offset when player is moving
	void Update () {
		this.transform.position = offset + player.transform.position;
	}

}
