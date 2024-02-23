using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UserInfo;

public class LobbyUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static TextMeshProUGUI gemCountText;
    public static TextMeshProUGUI coinCountText;
    public static TextMeshProUGUI levelText;
    public static TextMeshProUGUI expText;
    public static TextMeshProUGUI userNameText;
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
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void UpdateUserInfoUI(UserInfoData.UserInfoRes res)
    {
        levelText.text = res.Level.ToString("N0");
        // 경험치는 최대 경험치 받아서 수정 필요
        expText.text = res.Exp.ToString("N0");
        coinCountText.text = res.Money.ToString("N0");
        gemCountText.text = res.Diamond.ToString("N0");
        acquiredCookieId = res.AcquiredCookieId;
    }
}
