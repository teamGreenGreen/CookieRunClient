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

public class FriendRequestAccept : MonoBehaviour
{
    [SerializeField]
    public GameObject ErrorPrefab;
    [SerializeField]
    private TextMeshProUGUI requestIdTxt;
    public async void OnClick()
    {
        long requestId = long.Parse(requestIdTxt.text);

        FriendRequestAcceptRes res = await HttpManager.Instance.Post<FriendRequestAcceptRes>("friendrequestaccept", new
        {
            RequestId = requestId
        });

        if (res.Result == 0)
        {
            Debug.Log("수락 성공");
        }
        else if(res.Result == EErrorCode.FriendReqAcceptFailMyFriendCountExceeded)
        {
            GameObject warningPrefab = Instantiate(ErrorPrefab);

            Transform alertTxtTransform = warningPrefab.transform.Find("Alert_Txt");
            TextMeshProUGUI alertText = alertTxtTransform.GetComponentInChildren<TextMeshProUGUI>();
            alertText.text = "나의 최대 친구 수를 초과합니다.";

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
        else if (res.Result == EErrorCode.FriendReqAcceptFailTargetFriendCountExceeded)
        {
            GameObject warningPrefab = Instantiate(ErrorPrefab);

            Transform alertTxtTransform = warningPrefab.transform.Find("Alert_Txt");
            TextMeshProUGUI alertText = alertTxtTransform.GetComponentInChildren<TextMeshProUGUI>();
            alertText.text = "상대방의 최대 친구 수를 초과합니다.";

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
