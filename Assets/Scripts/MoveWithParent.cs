using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithParent : MonoBehaviour
{
    SpriteRenderer mySprite;
    SpriteRenderer parentSprite;
    void Start()
    {
        parentSprite = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        mySprite = transform.gameObject.GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(parentSprite.size.x / mySprite.size.x, 1f, 1f);
    }
}