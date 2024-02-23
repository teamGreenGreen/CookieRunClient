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

public class FriendRequestList : MonoBehaviour
{
    public GameObject friendRequestPrefab;
    public Transform contentTransform;
    public async void OnClick()
    {
        FriendRequestListRes res = await HttpManager.Instance.Post<FriendRequestListRes>("friendrequestlist", new
        {

        });

        if (res.Result == 0)
        {
            Debug.Log("요청 성공");
            // FriendList에서 각 FriendElement를 꺼내어 출력
            foreach (FriendRequestElement friend in res.FriendRequestList)
            {
                Debug.Log("요청 보낸 사람의 이름: " + friend.FromUserName);
                GameObject friendRequestObject = Instantiate(friendRequestPrefab, contentTransform);
                FriendRequestElementUI friendRequestElementUI = friendRequestObject.GetComponent<FriendRequestElementUI>();
                friendRequestElementUI.SetFriendInfo(friend);
            }
        }
        else
        {
            Debug.Log("에러");
            return;
        }

        return;
    }
}
