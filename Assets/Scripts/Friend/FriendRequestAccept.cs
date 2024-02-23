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
            Debug.Log("나의 최대 친구 수를 초과합니다.");
            return;
        }
        else if (res.Result == EErrorCode.FriendReqAcceptFailTargetFriendCountExceeded)
        {
            Debug.Log("상대방의 최대 친구 수를 초과합니다.");
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
