using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : MonoBehaviour {

    private static PlayerController playerController;

    public static PlayerController Controller
    {
        get { return playerController; }
    }


    private void Initialize()
    {
        if (playerController == null)
        {
            DontDestroyOnLoad(gameObject);
            playerController = this;
        }
        else if (playerController != this)
        {
            Destroy(gameObject);
        }
    }

	public Inventory inventory;

    public float moveSpeed;

	private Collider2D overlapItem;

    private Collider2D overlapNPC;


	// Use this for initialization
	public GameObject player_ball;
	private float nextFire;
	private Vector3 direction;
	private float attackSpeed;
    private Animator animator;
    private bool dialogueActive;
    private Vector2 movement;

	private void Awake() {
        Initialize();
	}

	void Start () {
		attackSpeed = 10f;
        animator = transform.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
 
        // 4 - Movement per direction
        movement = new Vector2(
            moveSpeed * horizontal,
            moveSpeed * vertical);
 

        // Accept the move signal from keyboard "input", and move the position
        if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + 0.5f;
			GameObject player_balls = Instantiate (player_ball, transform.position, transform.rotation) as GameObject;
			player_balls.GetComponent<Rigidbody2D> ().velocity = direction * attackSpeed;
			if (player_balls != null) {
				Destroy (player_balls, 2);
			}
		}
       
		if (horizontal > 0) {
            turnRight(true);
            direction = new Vector3(1, 0, 0);
        } else if (horizontal < 0) {
            turnRight(false);
            direction = new Vector3(-1, 0, 0);
        } else if (vertical > 0) {
			direction = new Vector3(0, 1, 0);
        } else if (vertical < 0) {
			direction = new Vector3(0, -1, 0);
        }

        //triger walk animation
        if (Mathf.Abs(horizontal) > 0.1 || Mathf.Abs(vertical) > 0.1) {
            animator.SetTrigger("walk");
        } else
        {
            animator.ResetTrigger("walk");
        }

        if (!dialogueActive) {
            //this.transform.position += new Vector3(horizontal, vertical, 0) * moveSpeed;
        }
	
		// Only allow one single item in specific range
		DetectPickUp ();
        DetectDialogTrigger();
        DetectNextDialogue();
	}

	void OnTriggerEnter2D (Collider2D other) {
        switch (other.tag) {
            case "Item":
                overlapItem = other;
                break;
            case "NPC":
                overlapNPC = other;
                Debug.Log("Enter NPC " + overlapNPC);
                break;
        }
		
	}

	void OnTriggerExit2D (Collider2D other) {
        switch (other.tag) {
            case "Item":
                overlapItem = null;
                break;
            case "NPC":
                overlapNPC = null;
                break;
        }
	}

    public void DetectDialogTrigger() {
        if (overlapNPC != null && !dialogueActive && Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Trigger dialog " + overlapNPC);
            FindObjectOfType<DialogueController>().StartDialogue(overlapNPC.GetComponent<DialogueTrigger>().dialogue);
            dialogueActive = true;
        }
    }

    public void DetectNextDialogue() {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space)) {
            dialogueActive = FindObjectOfType<DialogueController>().DisplayNextSentence();
        }
    }

	public void DetectPickUp() {
		if (overlapItem == null || !overlapItem.CompareTag ("Item"))
			return;
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log ("Picking up " + overlapItem.GetComponent<Item> ());
			inventory.AddItem (overlapItem.GetComponent<Item> ());
			Destroy (overlapItem.gameObject);
		}
	}

    private void turnRight(bool right)
    {
        if (right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }

}
