using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        base.Move(dir);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
