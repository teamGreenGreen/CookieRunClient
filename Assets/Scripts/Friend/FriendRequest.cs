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
    private TMP_InputField toUserName;
    public async void OnClick()
    {
        FriendRequestRes res = await HttpManager.Instance.Post<FriendRequestRes>("friendrequest", new
        {
            ToUserName = toUserName.text
        });

        if (res.Result == 0)
        {
            GameManager.Instance.OnMessage("요청을 보냈습니다.");
            return;
        }
        else if(res.Result == EErrorCode.FriendReqFailSelfRequest)
        {
            GameManager.Instance.OnMessage("자기 자신에게 요청을 보낼 수 없습니다.");
            return;
        }
        else if (res.Result == EErrorCode.FriendReqFailTargetNotFound)
        {
            GameManager.Instance.OnMessage("존재하지 않는 상대입니다.");
            return;
        }
        else if (res.Result == EErrorCode.FriendReqFailAlreadyFriend)
        {
            GameManager.Instance.OnMessage("이미 친구입니다.");
            return;
        }
        else if (res.Result == EErrorCode.FriendReqFailAlreadyReqExist)
        {
            GameManager.Instance.OnMessage("이미 동일한 신청이 존재합니다.");
            return;
        }
        else if (res.Result == EErrorCode.FriendReqFailMyFriendCountExceeded)
        {
            GameManager.Instance.OnMessage("최대 친구 수를 초과합니다.");
            return;
        }
        else
        {
            Debug.Log("에러");
            return;
        }
    }
}
