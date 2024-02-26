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

public class FriendList : MonoBehaviour
{
    public GameObject friendPrefab;
    public Transform contentTransform;
    public async void OnClick()
    {
        FriendListRes res = await HttpManager.Instance.Post<FriendListRes>("friendlist", new
        {

        });

        if(res.Result == 0)
        {
            ClearContentTransform();

            // FriendList에서 각 FriendElement를 꺼내어 출력
            foreach (FriendElement friend in res.FriendList)
            {
                GameObject friendObject = Instantiate(friendPrefab, contentTransform);
                FriendElementUI friendElementUI = friendObject.GetComponent<FriendElementUI>();
                friendElementUI.SetFriendInfo(friend);
            }
        }
        else
        {
            Debug.Log("에러");
            return;
        }

        return;
    }
    private void ClearContentTransform()
    {
        // contentTransform의 모든 자식 객체 제거
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
