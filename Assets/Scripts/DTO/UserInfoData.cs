using Assets.Scripts.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;
using UnityEngine.SocialPlatforms.Impl;

public class UserInfoRes : ErrorCodeDTO
{
    public UserInfo UserInfo { get; set; }
}
public class UserInfoData : MonoBehaviour
{
    public static async void RequestUserInfoPost()
    {
        UserInfoRes res = await HttpManager.Instance.Post<UserInfoRes>("UserInfoLoad", null);
        LobbyUIManager.UpdateUserInfoUI(res);
    }
}
