using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
        }
        else
        {
            textPasswordWarning.text = "";

        }
    }

    private bool IsVaildEmail(string email)
    {
        bool valid = Regex.IsMatch(email, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");

        return valid;
    }
}
