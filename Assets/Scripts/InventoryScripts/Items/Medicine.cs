using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : Item
{
    public GameObject player;
    public float moveSpeed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (moveSpeed == 0f)
        {
            moveSpeed = 2.5f; // Default
        }
        isStackable = true;
        base.Initialize();
    }

    public int addHealth;

    // Use this for initialization

    public int addExperience;
    // Use this for initialization

    public void Move()
    {
        Vector3 player_position = player.transform.position;
        Vector3 position = this.transform.position;
        float x_abs = System.Math.Abs(player_position.x - position.x);
        float y_abs = System.Math.Abs(player_position.y - position.y);
        double distance = x_abs + y_abs;
        if (addExperience > 0)
        {
            if (distance < 3)
            {

                float x_direction = ((player_position.x + position.x) / x_abs) / 20;
                float y_direction = ((player_position.y + position.y) / y_abs) / 20;
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
    }



    // Update is called once per frame
    void Update()
    {
        Move();

    }

    // Use: use the item. 
    public override void Use()
    {
        Debug.Log("health item");
        GameController.Controller.health += addHealth;
        GameController.Controller.experience += addExperience;
    }
}

