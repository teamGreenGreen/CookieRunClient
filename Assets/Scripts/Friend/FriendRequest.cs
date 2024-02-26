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

public class FriendRequest : MonoBehaviour
{
    [SerializeField]
    public GameObject ErrorPrefab;
    [SerializeField]
    private TMP_InputField toUserName;
    public async void OnClick()
    {
        FriendRequestRes res = await HttpManager.Instance.Post<FriendRequestRes>("friendrequest", new
        {
            ToUserName = toUserName.text
        });

        if (res.Result == 0)
        {
            GameObject warningPrefab = Instantiate(ErrorPrefab);

            Transform alertTxtTransform = warningPrefab.transform.Find("Alert_Txt");
            TextMeshProUGUI alertText = alertTxtTransform.GetComponentInChildren<TextMeshProUGUI>();
            alertText.text = "요청을 보냈습니다.";

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
                Debug.LogError("스크립트가 붙은 객체의 상단 부모에 캔버스가 없습니다.");
            }
            return;
        }
        else if(res.Result == EErrorCode.FriendReqFailSelfRequest)
        {
            GameObject warningPrefab = Instantiate(ErrorPrefab);

            Transform alertTxtTransform = warningPrefab.transform.Find("Alert_Txt");
            TextMeshProUGUI alertText = alertTxtTransform.GetComponentInChildren<TextMeshProUGUI>();
            alertText.text = "자기 자신에게 요청을 보낼 수 없습니다.";

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
                Debug.LogError("스크립트가 붙은 객체의 상단 부모에 캔버스가 없습니다.");
            }
            return;
        }
        else if (res.Result == EErrorCode.FriendReqFailTargetNotFound)
        {
            GameObject warningPrefab = Instantiate(ErrorPrefab);

            Transform alertTxtTransform = warningPrefab.transform.Find("Alert_Txt");
            TextMeshProUGUI alertText = alertTxtTransform.GetComponentInChildren<TextMeshProUGUI>();
            alertText.text = "존재하지 않는 상대입니다.";

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
                Debug.LogError("스크립트가 붙은 객체의 상단 부모에 캔버스가 없습니다.");
            }
            return;
        }
        else if (res.Result == EErrorCode.FriendReqFailAlreadyFriend)
        {
            GameObject warningPrefab = Instantiate(ErrorPrefab);

            Transform alertTxtTransform = warningPrefab.transform.Find("Alert_Txt");
            TextMeshProUGUI alertText = alertTxtTransform.GetComponentInChildren<TextMeshProUGUI>();
            alertText.text = "이미 친구입니다.";

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
                Debug.LogError("스크립트가 붙은 객체의 상단 부모에 캔버스가 없습니다.");
            }
            return;
        }
        else if (res.Result == EErrorCode.FriendReqFailAlreadyReqExist)
        {
            GameObject warningPrefab = Instantiate(ErrorPrefab);

            Transform alertTxtTransform = warningPrefab.transform.Find("Alert_Txt");
            TextMeshProUGUI alertText = alertTxtTransform.GetComponentInChildren<TextMeshProUGUI>();
            alertText.text = "이미 동일한 신청이 존재합니다.";

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
                Debug.LogError("스크립트가 붙은 객체의 상단 부모에 캔버스가 없습니다.");
            }
            return;
        }
        else if (res.Result == EErrorCode.FriendReqFailMyFriendCountExceeded)
        {
            GameObject warningPrefab = Instantiate(ErrorPrefab);

            Transform alertTxtTransform = warningPrefab.transform.Find("Alert_Txt");
            TextMeshProUGUI alertText = alertTxtTransform.GetComponentInChildren<TextMeshProUGUI>();
            alertText.text = "최대 친구 수를 초과합니다.";

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
                Debug.LogError("스크립트가 붙은 객체의 상단 부모에 캔버스가 없습니다.");
            }
            return;
        }
        else
        {
            Debug.Log("에러");
            return;
        }
    }
}
