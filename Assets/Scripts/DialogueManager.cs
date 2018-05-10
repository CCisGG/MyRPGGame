using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public GameObject dialogueBox;
	public Text nameText;
	public Text dialogueText;
	public Queue<string> sentences;
	public Animator animator;
	public bool dialogueActive;
	// Use this for initialization
	void Start () {
		sentences = new Queue<string> ();
	}

	void Update() {
		if (dialogueActive && Input.GetKey(KeyCode.Space)) {
			dialogueBox.SetActive(false);
			dialogueActive = false;
		}
	}

	public void ShowBox(string dialogue) {
		dialogueActive = true;
		dialogueBox.SetActive (true);
		dialogueText.text = dialogue;
	}

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

	IEnumerator TypeSentence(string sentence) {
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray() ) {
			dialogueText.text += letter;
			yield return null;
		}
	}


	void EndDialogue() {
		Debug.Log ("End of conversation");
		animator.SetBool ("isOpen", false);

	}
}
