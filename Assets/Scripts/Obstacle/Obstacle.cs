using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    protected string dir;
    [SerializeField]
    protected float depth = 1.0f;
    protected Player player;
    protected float damage = 0.0f;
    [SerializeField]
    protected float speed = 0.0f; 

    protected void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
   
    void FixedUpdate()
    {
        Move(dir);
    }

    protected void Move(string dir)
    {
        if (dir == null) return;

        float realVelocity = player.speed / depth;
        Vector2 pos = transform.position;

        if (dir == "left")
        {
            pos.x -= realVelocity * Time.fixedDeltaTime * speed;
        }
        else if(dir == "down")
        {
            pos.y -= realVelocity * Time.fixedDeltaTime * speed;
        }

        transform.position = pos;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            // PlayerHP 감소
        }
    }
}
