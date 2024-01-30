using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSceneManager : MonoBehaviour
{
    GameObject sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager");
    }

    public void OnClick()
    {
        sceneManager.GetComponent<LoadSceneManager>().SceneChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
