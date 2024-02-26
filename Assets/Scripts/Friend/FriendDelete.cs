using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DTO;
using Assets.Scripts.DAO;
using System;
using System.Text.RegularExpressions;
using TMPro;
using Assets.Scripts.DTO.GameServer;
using Assets.Scripts.DAO.GameServer;
using UnityEngine.UI;

public class FriendDelete : MonoBehaviour
{
    [SerializeField]
    public GameObject ErrorPrefab;
    [SerializeField]
    private TextMeshProUGUI friendNameTxt;
    public async void OnClick()
    {
        string friendName = friendNameTxt.text;

        FriendDeleteRes res = await HttpManager.Instance.Post<FriendDeleteRes>("frienddelete", new
        {
            FriendName = friendName
        });

        if (res.Result == 0)
        {
            GameObject warningPrefab = Instantiate(ErrorPrefab);

            Transform alertTxtTransform = warningPrefab.transform.Find("Alert_Txt");
            TextMeshProUGUI alertText = alertTxtTransform.GetComponentInChildren<TextMeshProUGUI>();
            alertText.text = "친구를 삭제했습니다.";

            Canvas canvas = GetComponentInParent<Canvas>();

            if (canvas != null)
            {
                RectTransform prefabRectTransform = warningPrefab.GetComponent<RectTransform>();
                prefabRectTransform.SetParent(canvas.transform, false);
                prefabRectTransform.localPosition = Vector3.zero;
                warningPrefab.transform.SetParent(canvas.transform, false);
            }
            else
            {
                Debug.LogError("CookieSelect_Canvas 스크립트가 붙은 객체의 상단 부모에 캔버스가 없습니다.");
            }
            return;
        }
        else
        {
            Debug.Log("에러");
            return;
        }

        return;
    }
}
