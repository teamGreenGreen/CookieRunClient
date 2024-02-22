using Assets.Scripts.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;
using UnityEngine.SocialPlatforms.Impl;

public class UserInfo : MonoBehaviour
{    public class UserInfoRes : ErrorCodeDTO
    {
        public Int64 Uid { get; set; }
        public Int64 UserId { get; set; }
        public string UserName { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int Money { get; set; }
        public int MaxScore { get; set; }
        public byte[] AcquiredCookieId { get; set; }
        public int Diamond { get; set; }
    }

    public static async void RequestUserInfoPost()
    {
        UserInfoRes res = await HttpManager.Instance.Post<UserInfoRes>("UserInfoLoad", null);
        LobbyUIManager.UpdateUserInfoUI(res);
    }
}
