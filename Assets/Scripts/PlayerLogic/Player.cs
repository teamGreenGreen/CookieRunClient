using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;

    public float jumpVelocity = 20.0f;
    public float groundHeight = 10.0f;

    public bool bGround = false;
    public bool bDoubleJump = false;

    public float speed = 1.0f;

    SpriteRenderer playerRender;

    private Animator anim;

    void Start()
    {
        playerRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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

        UpdateAnimation();
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!bGround)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;

            if (velocity.y < 0)
            {
                Vector2 rayOrigin = new Vector2(pos.x + (playerRender.size.x / 2), pos.y - (playerRender.size.y / 2));
                Vector2 rayDirection = Vector2.down;
                RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, playerRender.size.y / 2);

                if (hit2D.collider != null)
                {
                    Debug.Log("히트다 히트");
                    Ground ground = hit2D.collider.GetComponent<Ground>();
                    if (ground != null)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight + (playerRender.size.y / 2);
                        bGround = true;
                        bDoubleJump = false;
                    }
                    else
                    {
                        Debug.Log("히트했는데, Ground 없다");
                    }
                }

                Debug.DrawRay(rayOrigin, rayDirection * playerRender.size.y / 2, Color.red);
            }
        }
        else
        {
            Debug.Log("떨어진다");
            Vector2 rayOrigin = new Vector2(pos.x - (playerRender.size.x / 2), pos.y - (playerRender.size.y / 2));
            Vector2 rayDirection = Vector2.down;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, 1);
            if (hit2D.collider == null)
            {
                bGround = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * playerRender.size.y / 2, Color.yellow);
        }

        velocity.x += speed * Time.fixedDeltaTime;

        transform.position = pos;
    }

    private void UpdateAnimation()
    {
        if (!bGround && !bDoubleJump)
        {
            anim.SetBool("jumping", true);
        }
        else if (!bGround && bDoubleJump)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("d_jumping", true);
        }
        else
        {
            anim.SetBool("jumping", false);
            anim.SetBool("d_jumping", false);
        }
    }

}
