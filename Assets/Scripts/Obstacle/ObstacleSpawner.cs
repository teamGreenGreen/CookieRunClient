using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private int curObstacleNum = 0;
    [SerializeField]
    private float addXPos = 0;
    [SerializeField]
    private float yPos = 0;
    [SerializeField]
    public List<GameObject> obstaclePrefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            if (curObstacleNum > obstaclePrefabs.Count)
                return;

            Vector3 curPos = transform.position;
            curPos.x += addXPos;
            curPos.y = yPos;
            curPos.z = -10.0f;

            Instantiate(obstaclePrefabs[curObstacleNum], curPos, Quaternion.identity);
        }
    }
}
