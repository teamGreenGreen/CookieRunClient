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
    public bool bSliding = false;

    public float speed = 1.0f;

    SpriteRenderer playerRender;

    private Animator anim;

    public string cookieName = "";

    void Start()
    {
        playerRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (cookieName == "")
        {
            cookieName = "Brave_Cookie_Controller";
        }
        // 동적으로 애니메이션 할당. 씬이 넘어올 때 cookieName에 해당하는 컨트롤러 명을 넘겨줘야 한다. 만약 안넘어오면 용감한 쿠키로 됨
        anim.runtimeAnimatorController = (RuntimeAnimatorController)Instantiate(Resources.Load($"Cookie\\Animation\\{cookieName}", typeof(RuntimeAnimatorController)));
    }
    void Update()
    {
        if (bGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bSliding = false;
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
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            bSliding = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            bSliding = false;
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
                Vector2 rayDirection = Vector2.down;
                Vector2 rayOrigin1 = new Vector2(pos.x + (playerRender.size.x / 2), pos.y);
                RaycastHit2D hit2D1 = Physics2D.Raycast(rayOrigin1, rayDirection, 0.1f);
                Vector2 rayOrigin2 = new Vector2(pos.x, pos.y);
                RaycastHit2D hit2D2 = Physics2D.Raycast(rayOrigin2, rayDirection, 0.1f);
                Vector2 rayOrigin3 = new Vector2(pos.x - (playerRender.size.x / 2), pos.y);
                RaycastHit2D hit2D3 = Physics2D.Raycast(rayOrigin3, rayDirection, 0.1f);


                if (hit2D1.collider != null || hit2D3.collider != null || hit2D3.collider != null)
                {
                    Ground ground
                        = hit2D1.collider != null ? hit2D1.collider.GetComponent<Ground>()
                        : hit2D2.collider != null ? hit2D2.collider.GetComponent<Ground>()
                        : hit2D3.collider.GetComponent<Ground>();

                    if (ground != null)
                    {
                        Debug.Log("ground hit!");
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        bGround = true;
                        bDoubleJump = false;
                    }
                    else
                    {
                        Debug.Log("collider hit, ground NULL!");
                    }
                }

                Debug.DrawRay(rayOrigin1, rayDirection * 0.1f, Color.red);
                Debug.DrawRay(rayOrigin2, rayDirection * 0.1f, Color.red);
                Debug.DrawRay(rayOrigin3, rayDirection * 0.1f, Color.red);
            }
        }
        else
        {
            Vector2 rayDirection = Vector2.down;
            Vector2 rayOrigin1 = new Vector2(pos.x - (playerRender.size.x / 2), pos.y);
            RaycastHit2D hit2D1 = Physics2D.Raycast(rayOrigin1, rayDirection, 0.1f);
            Vector2 rayOrigin2 = new Vector2(pos.x, pos.y);
            RaycastHit2D hit2D2 = Physics2D.Raycast(rayOrigin2, rayDirection, 0.1f);
            Vector2 rayOrigin3 = new Vector2(pos.x + (playerRender.size.x / 2), pos.y);
            RaycastHit2D hit2D3 = Physics2D.Raycast(rayOrigin3, rayDirection, 0.1f);

            if (hit2D1.collider == null && hit2D2.collider == null && hit2D3.collider == null)
            {
                Debug.Log("떨어진다");
                bGround = false;
                bSliding = false;
            }

            Debug.DrawRay(rayOrigin1, rayDirection *0.1f, Color.yellow);
            Debug.DrawRay(rayOrigin2, rayDirection * 0.1f, Color.yellow);
            Debug.DrawRay(rayOrigin3, rayDirection * 0.1f, Color.yellow);
        }

        velocity.x += speed * Time.fixedDeltaTime;

        transform.position = pos;
    }

    private void UpdateAnimation()
    {
        if (!bGround && !bDoubleJump)
        {
            anim.SetBool("jumping", true);
            anim.SetBool("sliding", false);
        }
        else if (!bGround && bDoubleJump)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("d_jumping", true);
        }
        else if (bGround && bSliding)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("d_jumping", false);
            anim.SetBool("sliding", true);
        }
        else
        {
            anim.SetBool("jumping", false);
            anim.SetBool("d_jumping", false);
            anim.SetBool("sliding", false);
        }
    }

}
