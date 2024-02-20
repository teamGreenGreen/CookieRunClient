using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class CreateAccount : MonoBehaviour
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

    public void CheckInput()
    {
        if (string.IsNullOrEmpty(inputEmail.text))
        {
            textEmailWarning.text = "이메일을 입력해주세요.";
        }
        else if(!IsVaildEmail(inputEmail.text))
        {
            textEmailWarning.text = "올바른 이메일 형식이 아닙니다.";
        }
        else
        {
            textEmailWarning.text = "";
        }

        if (string.IsNullOrEmpty(inputPassword.text))
        {
            textPasswordWarning.text = "비밀번호를 입력해주세요.";
            return;
        }
        else
        {
            textPasswordWarning.text = "";
        }

        StartCoroutine(Login());
    }

    private bool IsVaildEmail(string email)
    {
        bool valid = Regex.IsMatch(email, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");

        return valid;
    }

    public enum EErrorCode
    {
        None
    }

    public class ErrorCodeDTO
    {
        public EErrorCode Result { get; set; } = EErrorCode.None;
    }

    [Serializable]
    public class CreateAccountReq
    {
        public string Email;
        public string Password;
    }


    public class CreateAccountRes : ErrorCodeDTO
    {
    }

    IEnumerator Login()
    {
        string url = "https://localhost:7034/Account/Create";

        CreateAccountReq request = new CreateAccountReq
        {
            Email = inputEmail.text,
            Password = inputPassword.text
        };

        string jsonRequest = JsonUtility.ToJson(request);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonRequest); // JSON 문자열을 바이트 배열로 변환

        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(jsonBytes);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("HTTP Error: " + www.error);
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            CreateAccountRes response = JsonUtility.FromJson<CreateAccountRes>(jsonResponse);

            Debug.Log("Login Response: " + response);
        }
    }
}
