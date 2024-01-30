using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;

    private void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        width = collider.size.x;
    }

    private void Update()
    {
        if (transform.position.x <= -width)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }    
}
