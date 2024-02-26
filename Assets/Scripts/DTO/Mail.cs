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
                if (go != null)
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
        if (res.Result != EErrorCode.None)
        {
            GameManager.Instance.OnMessage("메일 리스트를 불러오는 데\n실패했습니다. \n 잠시 후 다시 시도해주세요");
            return;
        }

        LobbyUIManager.Instance.ClearAllMails();
        LobbyUIManager.Instance.UpdateMailListUI(res);
    }

    public async Task MailOpenPostAsync(int mailboxId)
    {
        MailOpenRes res = await HttpManager.Instance.Post<MailOpenRes>("MailOpen", new
        {
            MailboxId = mailboxId
        });

        if (res.Result != EErrorCode.None)
        {
            GameManager.Instance.OnMessage("메일 열기를 실패했습니다. \n 잠시 후 다시 시도해주세요");
            return;
        }

        MailListPostAsync();
    }

    public async Task MailDeletePostAsync(int mailboxId)
    {
        MailDeleteRes res = await HttpManager.Instance.Post<MailDeleteRes>("MailDelete", new
        {
            MailboxId = mailboxId
        });

        if (res.Result != EErrorCode.None)
        {
            GameManager.Instance.OnMessage("메일 삭제를 실패했습니다. \n 잠시 후 다시 시도해주세요");
            return;
        }

        MailListPostAsync();
    }
}
