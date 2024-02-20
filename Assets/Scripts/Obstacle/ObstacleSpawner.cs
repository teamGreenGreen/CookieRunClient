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
    private int count = 0;

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
            Vector3 playerPos = player.transform.position;

            if (curObstacleNum > obstaclePrefabs.Count)
                return;

            if(curObstacleNum == 0)
                Instantiate(obstaclePrefabs[curObstacleNum], playerPos + new Vector3(10, 0, 0), Quaternion.identity);
            else
                Instantiate(obstaclePrefabs[curObstacleNum], playerPos + new Vector3(10, 10, 0), Quaternion.identity);
        }
    }
}
