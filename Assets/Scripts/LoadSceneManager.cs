using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        sceneNumber = (int)name;
        SceneManager.LoadScene((int)name);
    }

    public async void OnSceneLoaded(Scene scnene, LoadSceneMode mode)
    {
        if (scnene.name == ESceneName.LobbyScene.ToString())
        {
            sceneNumber = (int)ESceneName.LobbyScene;
            LobbyUIManager.Instance.loadingCanvas.gameObject.SetActive(true);
            await UserInfoData.Instance.RequestUserInfoPostAsync();
            UserInfoData.Instance.RequestNowCookieId();
        }

        else if(scnene.name == ESceneName.InGame.ToString())
        {
            sceneNumber = (int)ESceneName.InGame;
            GameManager.Instance.Reset();
            GameManager.Instance.SetPlayer();
        }
    }
}
