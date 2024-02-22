using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthworm : Obstacle
{
    bool isGround = false;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(!isGround)
        {
            base.Move(dir);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player != null)
        {
            base.OnTriggerEnter2D(collision);
        }

        Ground ground = collision.gameObject.GetComponent<Ground>();
        if (ground != null && !isGround && animator != null)
        {
            isGround = true;
            animator.SetBool("isGround", true);
            gameObject.AddComponent<Parallax>();
        }
    }
}
