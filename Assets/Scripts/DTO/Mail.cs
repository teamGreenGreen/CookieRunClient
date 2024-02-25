using Assets.Scripts.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;
using UnityEngine.SocialPlatforms.Impl;
using System.Dynamic;
using static UserInfoData;
using static GameResult;
using System.Threading.Tasks;


public class MailInfo
{
    public int MailboxId { get; set; }
    public int Uid { get; set; }
    public bool IsRead { get; set; }
    public string Sender { get; set; }
    public string Content { get; set; }
    public string RewardType { get; set; }
    public int Count { get; set; }
    public DateTime ExpiredAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class Mail : MonoBehaviour
{
    public class MailOpenReq
    {
        public int MailboxId { get; set; }
    }

    public class MailListRes : ErrorCodeDTO
    {
        public IEnumerable<MailInfo> MailList { get; set; }
    }

    public class MailOpenRes : ErrorCodeDTO
    {
    }

    public class MailDeleteRes : ErrorCodeDTO
    {

    }

    public static async void MailListPost()
    {
        MailListRes res = await HttpManager.Instance.Post<MailListRes>("MailList", null);
        GameObject gameObject = GameObject.Find("LobbyUIManager");
        if(gameObject != null)
        {
            LobbyUIManager lobbyUIManager = gameObject.GetComponent<LobbyUIManager>();
            lobbyUIManager.ClearAllMails();
            lobbyUIManager.UpdateMailListUI(res);
        }
    }

    public static async Task MailOpenPostAsync(int mailboxId)
    {
        MailOpenRes res = await HttpManager.Instance.Post<MailOpenRes>("MailOpen", new
        {
            MailboxId = mailboxId
        });

        if (res.Result == EErrorCode.None)
        {
            MailListPost();
        }
    }

    public static async Task MailDeletePostAsync(int mailboxId)
    {
        MailDeleteRes res = await HttpManager.Instance.Post<MailDeleteRes>("MailDelete", new
        {
            MailboxId = mailboxId
        });

        if (res.Result == EErrorCode.None)
        {
            MailListPost();
        }
    }
}
