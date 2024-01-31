using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float groundHeight;
    void Start()
    {
        groundHeight = transform.position.y + (transform.localScale.y / 2);
        Debug.Log(groundHeight);
    }

    void Update()
    {
        
    }
}
