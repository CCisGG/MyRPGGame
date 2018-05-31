using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Dialogue Controller for controlling the Diaglogue between player and NPC.
 **/
public class DialogueController : MonoBehaviour {
	public GameObject dialogueBox;
	public Text nameText;
	public Text dialogueText;
	public Queue<string> sentences;
	public Animator animator;
	public bool dialogueActive;
	// Use this for initialization

    private static DialogueController dialogueController;

    public static DialogueController Controller
    {
        get { return dialogueController; }
    }

    private void Initialize()
    {
        if (dialogueController == null)
        {
            DontDestroyOnLoad(gameObject);
            dialogueController = this;
        }
        else if (dialogueController != this)
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        Initialize();
    }

	void Start () {
		sentences = new Queue<string> ();
	}

    // Update the status for dialogue and dialogue box.
	void Update() {
		if (dialogueActive && Input.GetKey(KeyCode.Space)) {
			dialogueBox.SetActive(false);
			dialogueActive = false;
		}
	}

    // Show to dialogue box.
	public void ShowBox(string dialogue) {
		dialogueActive = true;
		dialogueBox.SetActive (true);
		dialogueText.text = dialogue;
	}

    // Start Dialogue
	public void StartDialogue(Dialogue dialogue) {
		animator.SetBool ("isOpen", true);
		Debug.Log ("Start Dialogue with " + dialogue.name);
		nameText.text = dialogue.name;
		sentences.Clear ();

		foreach(string sentence in dialogue.sentences) {
			sentences.Enqueue (sentence);
		}

		//DisplayNextSentence ();
	}

    // Diaplay next sentence.
	public bool DisplayNextSentence() {
		if (sentences.Count == 0) {
			EndDialogue ();
			return false;
		}
		string sentence = sentences.Dequeue ();

		StopAllCoroutines ();
		StartCoroutine(TypeSentence (sentence));
		Debug.Log (dialogueText.text);
        return true;

	}

    // show the sentence char by char in the dialogue box.
	IEnumerator TypeSentence(string sentence) {
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray() ) {
			dialogueText.text += letter;
			yield return null;
		}
	}

    // End Dialogue
	void EndDialogue() {
		Debug.Log ("End of conversation");
		animator.SetBool ("isOpen", false);

	}
}
