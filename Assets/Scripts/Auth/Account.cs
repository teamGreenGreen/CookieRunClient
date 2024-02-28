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
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class Account : MonoBehaviour
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

    public async void OnClick()
    {
        bool isValidEmail = false, isValidPassword = false;

        HttpManager.Instance.ServerURL = HttpManager.AUTH_SERVER_URL;

        isValidEmail = IsValidEmail();
        isValidPassword = IsValidPassword();

        if (isValidEmail && isValidPassword)
        {
            CreateAccountRes res = await HttpManager.Instance.Post<CreateAccountRes>("Account/Create", new {
                Email = inputEmail.text,
                Password = inputPassword.text
            });

            notificationUI.SetActive(true);
            TMP_Text tmp = notificationUI.GetComponentInChildren<TMP_Text>();
            if (res.Result == EErrorCode.None)
            {
                tmp.text = "회원가입이 완료되었습니다";
            }
            else if (res.Result == EErrorCode.CreateAccountFailDuplicate)
            {
                tmp.text = "이미 등록된 이메일 입니다";
            }
               
            return;
        }
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
