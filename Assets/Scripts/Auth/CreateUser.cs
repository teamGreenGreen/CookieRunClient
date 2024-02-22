using Assets.Scripts.DTO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class CreateUser : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputUserName;
    [SerializeField]
    private TMP_Text textUserNameWarning;
    [SerializeField]
    private LoadSceneManager loadSceneManager;

    public async void GameServerCreateUser()
    {
        if(IsValidUserName())
        {
            // 게임 서버로 유저 생성 요청
            GameServerCreateRes loginRes = await HttpManager.Instance.LoginServer<GameServerCreateRes>("CreateUser", new
            {
                UserId = HttpManager.Instance.uid,
                AuthToken = HttpManager.Instance.sessionId
            });
        }
        // TODO : 이미 등록된 닉네임 처리
        else
        {
            //notificationUI.SetActive(true);
            //TMP_Text tmp = notificationUI.GetComponentInChildren<TMP_Text>();
            //tmp.text = "이미 등록된 닉네임 입니다";
            
        }

        loadSceneManager.SceneChange();
    }

    private bool IsValidUserName()
    {
        if (string.IsNullOrEmpty(inputUserName.text))
        {
            textUserNameWarning.text = "닉네임을 입력해주세요";
            return false;
        }
        else
        {
            textUserNameWarning.text = "";
            return true;
        }
    }
}
