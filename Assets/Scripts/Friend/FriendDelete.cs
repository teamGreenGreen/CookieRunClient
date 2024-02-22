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
            Debug.Log("삭제 성공");
        }
        else
        {
            Debug.Log("에러");
            return;
        }

        return;
    }
}
