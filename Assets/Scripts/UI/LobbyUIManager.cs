using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static Mail;
using static UserInfo;

public class LobbyUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static TextMeshProUGUI gemCountText;
    public static TextMeshProUGUI coinCountText;
    public static TextMeshProUGUI levelText;
    public static TextMeshProUGUI expText;
    public static TextMeshProUGUI userNameText;
    public ScrollRect scrollRect;
    public GameObject mailPrefab;
    public static int acquiredCookieId;

    void Start()
    {
        GameObject gameObject = GameObject.Find("GemGount_Txt");
        gemCountText = gameObject.GetComponent<TextMeshProUGUI>();

        gameObject = GameObject.Find("CoinCount_Txt");
        coinCountText = gameObject.GetComponent<TextMeshProUGUI>();

        gameObject = GameObject.Find("Level_Txt");
        levelText = gameObject.GetComponent<TextMeshProUGUI>();

        gameObject = GameObject.Find("Exp_Txt");
        expText = gameObject.GetComponent<TextMeshProUGUI>();

        gameObject = GameObject.Find("UserName_Txt");
        userNameText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public float elapseedTime = 0;
    public bool onceCheck = false;

    // Update is called once per frame
    void Update()
    {
    }

    public void ClearAllMails()
    {
        if (scrollRect != null)
        {
            foreach (Transform child in scrollRect.content.transform)
            {
                MailBox mailBox = child.GetComponent<MailBox>();
                mailBox.ClearDetailInfo();
                Destroy(child.gameObject);
            }
        }
    }

    public static void UpdateUserInfoUI(UserInfoRes res)
    {
        if (res == null)
        {
            return;
        }

        if(levelText == null ||
            expText == null ||
            coinCountText == null ||
            gemCountText == null ||
            userNameText == null)
        {
            return;
        }

        levelText.text = res.UserInfo.Level.ToString("N0");
        // TODO.김초원 : 경험치는 최대 경험치 받아서 수정 필요
        expText.text = res.UserInfo.Exp.ToString("N0");
        coinCountText.text = res.UserInfo.Money.ToString("N0");
        gemCountText.text = res.UserInfo.Diamond.ToString("N0");
        acquiredCookieId = res.UserInfo.AcquiredCookieId;
        userNameText.text = res.UserInfo.UserName.ToString();
    }

    public void UpdateMailListUI(MailListRes res)
    {
        if (scrollRect == null)
        {
            return;
        }

        RectTransform content = scrollRect.content;

        foreach (MailInfo mail in res.MailList)
        {
            GameObject mailObject = Instantiate(mailPrefab, content);
            MailBox mailBox = mailObject.GetComponent<MailBox>();
            mailBox.mailBoxId = mail.MailboxId;

            Transform textTransform = mailObject.transform.Find("preview");
            if(textTransform != null)
            {
                TextMeshProUGUI mailInfoText = textTransform.GetComponent<TextMeshProUGUI>();
                
                if(mailInfoText != null)
                {
                    mailInfoText.text = mail.Sender.ToString();
                }
            }

            Transform senderTransform = mailObject.transform.Find("sender");
            if (senderTransform != null)
            {
                TextMeshProUGUI senderText = senderTransform.GetComponent<TextMeshProUGUI>();

                if (senderText != null)
                {
                    senderText.text = mail.Sender.ToString();
                    mailBox.sender.text = mail.Sender.ToString();
                }
            }

            Transform contentTransform = mailObject.transform.Find("content");
            if (contentTransform != null)
            {
                TextMeshProUGUI contentText = contentTransform.GetComponent<TextMeshProUGUI>();

                if (contentText != null)
                {
                    contentText.text = mail.Content.ToString();
                    mailBox.content.text = mail.Content.ToString();
                }
            }

            if (mail.RewardType == "diamond")
            {
                Transform diamondTransform = mailObject.transform.Find("diamond");

                if(diamondTransform != null)
                {
                    diamondTransform.gameObject.SetActive(true);
                }
            }
            else if(mail.RewardType == "money")
            {
                Transform moneyTransform = mailObject.transform.Find("coin");

                if (moneyTransform != null)
                {
                    moneyTransform.gameObject.SetActive(true);
                }
            }
        }
    }
}
