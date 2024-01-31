using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float jumpVelocity = 20.0f;
    public float groundHeight = 10.0f;
    public bool bGround = false;

    public bool bDoubleJump = false;
    void Start()
    {
        
    }
    void Update()
    {
        if (bGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bGround = false;
                velocity.y = jumpVelocity;
            }
        }
        else
        {
            if (!bDoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                bDoubleJump = true;
                velocity.y = jumpVelocity * 0.8f;
                Debug.Log("더블점프!");
            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!bGround)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                bGround = true;
                bDoubleJump = false;
            }
        }

        transform.position = pos;
    }
}
