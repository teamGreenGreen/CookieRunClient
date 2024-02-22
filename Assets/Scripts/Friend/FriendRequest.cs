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
            Debug.Log("요청 성공");
        }
        else
        {
            Debug.Log("에러");
            return;
        }

        return;
    }
}
