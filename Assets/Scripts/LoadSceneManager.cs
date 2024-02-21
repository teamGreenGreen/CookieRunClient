using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private LoadSceneManager instance = null;
    private int sceneNumber = 0;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance == null)
        {
            Destroy(gameObject);
        }
    }
    public void SceneChange()
    {
        sceneNumber++;

        SceneManager.LoadScene(sceneNumber);
    }
}
