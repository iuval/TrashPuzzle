﻿using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    public const int CELL_TYPE_FREE = -1;
    public const int CELL_TYPE_1 = 0;
    public const int CELL_TYPE_2 = 1;
    public const int CELL_TYPE_3 = 2;
    public const int CELL_TYPE_4 = 3;
    public const int CELL_TYPE_5 = 4;

    private int type;

    public float speed = 0.01f;
    private Vector2 movement;
    public int cell_x;
    public int cell_y;

    private bool moving = false;
    private float target_x;
    private float target_y;

    private Animator anim;

    public void setCellType(int type)
    {
        this.type = type;

        SpriteRenderer ren = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case CELL_TYPE_FREE:
                {
                    ren.color = Color.green;
                    anim.SetBool("Clean", false);
                } break;
            case CELL_TYPE_1:
                {
                    ren.color = Color.blue;
                } break;
            case CELL_TYPE_2:
                {
                    ren.color = Color.yellow;
                } break;
            case CELL_TYPE_3:
                {
                    ren.color = Color.red;
                } break;
            case CELL_TYPE_4:
                {
                    ren.color = Color.white;
                } break;
            case CELL_TYPE_5:
                {
                    ren.color = Color.magenta;
                } break;
        }
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Clean", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Vector2 pos = transform.position;
            pos += movement * Time.deltaTime;

            if (Vector2.Distance(pos, new Vector2(target_x, target_y)) < 0.1)
            {
                pos.x = target_x;
                pos.y = target_y;
                moving = false;
            }
            transform.position = pos;
        }
    }

    public void Move(int new_x, int new_y, float t_x, float t_y)
    {
        cell_x = new_x;
        cell_y = new_y;

        target_x = t_x;
        target_y = t_y;

        movement = Vector2.zero;

        if (target_x > transform.position.x)
        {
            movement.x = speed;
        }
        else if (target_x < transform.position.x)
        {
            movement.x = -speed;
        }

        if (target_y > transform.position.y)
        {
            movement.y = speed;
        }
        else if (target_y < transform.position.y)
        {
            movement.y = -speed;
        }

        moving = true;
    }

    public bool isMoving()
    {
        return moving;
    }

    public int getCellType()
    {
        return type;
    }
}
