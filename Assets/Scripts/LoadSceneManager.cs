using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameResult;
using static UnityEditor.Progress;
using UnityEngine.SocialPlatforms.Impl;

public enum ESceneName
{
    Title,
    LobbyScene,
    SelectStage,
    InGame
}

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
            SceneManager.sceneLoaded += OnSceneLoaded;
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

    public void SceneChangeBySceneNum(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void SceneChangeByEnumValue(ESceneName name)
    {
        SceneManager.LoadScene((int)name);
    }

    public void OnSceneLoaded(Scene scnene, LoadSceneMode mode)
    {
        if (scnene.name == ESceneName.LobbyScene.ToString())
        {
            UserInfoData.RequestUserInfoPostAsync();
            UserInfoData.RequestNowCookieId();
        }
    }

    public void ReturnToLobbyAfterDelay(float delayTime)
    {
        StartCoroutine(DelayedSceneChange(delayTime));
    }

    private IEnumerator DelayedSceneChange(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneChangeByEnumValue(ESceneName.LobbyScene);
    }
}
