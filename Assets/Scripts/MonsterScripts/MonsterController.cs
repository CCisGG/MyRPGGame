using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonsterController : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public float nextAttack;
	public float attackRate;
	public float attackSpeed;
	public float health;
    public int touchHurt;
    protected bool startAttack;


    public int TouchHurt
    {
        get { return touchHurt; }
        set { touchHurt = value; }
    }


	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void remoteAttack(GameObject ball) {
		if (Time.time > nextAttack) {
			nextAttack = Time.time + attackRate;
			GameObject balls = Instantiate (ball, transform.position, transform.rotation)as GameObject;
			Vector3 direction = (player.transform.position - this.transform.position);
			balls.GetComponent<Rigidbody2D> ().velocity = direction * attackSpeed;
			if (balls != null) {
				Destroy (balls, 2);
			}
		}
	}
	
	// Update is called once per frame
	private void Update () {
        Move();
	}

    public void Move()
    {
        Vector3 player_position = player.transform.position;
        Vector3 position = this.transform.position;
        float x_abs = System.Math.Abs(player_position.x - position.x);
        float y_abs = System.Math.Abs(player_position.y - position.y);
        double distance = x_abs + y_abs;
        if (distance > 12)
        {
            startAttack = false;
        }
        else if (distance < 8)
        {
            startAttack = true;
            float x_direction = ((player_position.x - position.x) / x_abs) / 20;
            float y_direction = ((player_position.y - position.y) / y_abs) / 20;
            //this.transform.position = new Vector3 (x_direction, y_direction, 0) + this.transform.position;
            if (x_abs > y_abs)
            {
                this.transform.position = new Vector3(x_direction, 0, 0) + this.transform.position;
            }
            else
            {
                this.transform.position = new Vector3(0, y_direction, 0) + this.transform.position;
            }
        }
    }

	// Hurt player.
	public void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
            Debug.Log("touchHurt: " + TouchHurt);
            Debug.Log("attackRate: " + attackRate);
            Debug.Log("attackSpeed: " + attackSpeed);
            if (GameController.Controller.health >= touchHurt) {
                GameController.Controller.health -= touchHurt;
			} else {
				GameObject.Destroy(player);
			}
		}

	}
}
