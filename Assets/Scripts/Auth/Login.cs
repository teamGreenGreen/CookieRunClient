using Assets.Scripts.DTO;
using Assets.Scripts.DTO.AuthServer;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class Login : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputEmail;
    [SerializeField]
    private TMP_Text textEmailWarning;
    [SerializeField]
    private TMP_InputField inputPassword;
    [SerializeField]
    private TMP_Text textPasswordWarning;
    [SerializeField]
    private GameObject notificationUI;
    [SerializeField]
    private LoadSceneManager loadSceneManager;

    public async void LoginAuthServer()
    {
        bool isValidEmail = false, isValidPassword = false;

        HttpManager.Instance.ServerURL = HttpManager.AUTH_SERVER_URL;

        isValidEmail = IsValidEmail();
        isValidPassword = IsValidPassword();

        if (isValidEmail && isValidPassword)
        {
            AuthServerLoginRes res = await HttpManager.Instance.LoginAuthServer<AuthServerLoginRes>("Account/Login", new {
                Email = inputEmail.text,
                Password = inputPassword.text
            });

            if (res.Result == EErrorCode.None)
            {
                // 연결될 서버의 URL을 게임서버의 URL로 교체
                HttpManager.Instance.ServerURL = HttpManager.GAME_SERVER_URL; 
                // 인증 완료 후 씬 변경
                loadSceneManager.SceneChange();
            }
            else
            {
                notificationUI.SetActive(true);
                TMP_Text tmp = notificationUI.GetComponentInChildren<TMP_Text>();
                tmp.text = "로그인에 실패했습니다";
            }

            return;
        }
    }

    public async void LoginGameServer(Int64 userId, string authToken)
    {
        // TODO : 닉네임 설정 기능 추가
        GameServerLoginRes res = await HttpManager.Instance.Post<GameServerLoginRes>("Login", new
        {
            UserId = userId,
            AuthToken = authToken,
            UserName = "test"
        });

        HttpManager.Instance.SetAuthInfo(res.Uid, res.SessionId);
    }

    private bool IsValidPassword()
    {
        if (string.IsNullOrEmpty(inputPassword.text))
        {
            textPasswordWarning.text = "비밀번호를 입력해주세요";
            return false;
        }
        else
        {
            textPasswordWarning.text = "";
            return true;
        }

    }

    private bool IsValidEmail()
    {
        if (string.IsNullOrEmpty(inputEmail.text))
        {
            textEmailWarning.text = "이메일을 입력해주세요";
            return false;
        }
        else if (!IsVaildEmailFormat(inputEmail.text))
        {
            textEmailWarning.text = "올바른 이메일 형식이 아닙니다";
            return false;
        }
        else
        {
            textEmailWarning.text = "";
            return true;
        }
    }

    private bool IsVaildEmailFormat(string email)
    {
        bool valid = Regex.IsMatch(email, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");

        return valid;
    }
}
