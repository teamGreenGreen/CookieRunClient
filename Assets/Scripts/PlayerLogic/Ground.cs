using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    SpriteRenderer groundRender;
    public float groundHeight;
    void Start()
    {
        groundRender = GetComponent<SpriteRenderer>();
        groundHeight = transform.position.y + (groundRender.size.y / 2);
    }

    void Update()
    {
        
    }
}
