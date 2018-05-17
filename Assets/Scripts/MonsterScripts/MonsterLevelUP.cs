using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLevelUP : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<PurpleMonsterController>().health = 100 + FindObjectOfType<LevelController>().Level * 20;
        GetComponent<PurpleMonsterController>().attackRate += FindObjectOfType<LevelController>().Level * 0.05f;
        GetComponent<PurpleMonsterController>().attackSpeed += FindObjectOfType<LevelController>().Level * 0.05f;
        GetComponent<PurpleMonsterController>().TouchHurt += FindObjectOfType<LevelController>().Level * 2;
        this.transform.localScale = this.transform.localScale * (1 + FindObjectOfType<LevelController>().Level * 0.2F);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
