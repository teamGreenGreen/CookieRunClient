using Assets.Scripts.DTO;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;


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
    public static Mail Instance
    {
        get
        {
            instance = FindObjectOfType<Mail>();
            if (instance == null)
            {
                GameObject go = new GameObject("LobbyUIManager");
                if(go !=  null)
                {
                    instance = go.AddComponent<Mail>();
                }
            }

            return instance;
        }
    }
    private static Mail instance;

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

    public async void MailListPostAsync()
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

    public async Task MailOpenPostAsync(int mailboxId)
    {
        MailOpenRes res = await HttpManager.Instance.Post<MailOpenRes>("MailOpen", new
        {
            MailboxId = mailboxId
        });

        if (res.Result == EErrorCode.None)
        {
            MailListPostAsync();
        }
    }

    public async Task MailDeletePostAsync(int mailboxId)
    {
        MailDeleteRes res = await HttpManager.Instance.Post<MailDeleteRes>("MailDelete", new
        {
            MailboxId = mailboxId
        });

        if (res.Result == EErrorCode.None)
        {
            MailListPostAsync();
        }
    }
}
