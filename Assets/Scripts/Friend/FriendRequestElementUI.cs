using UnityEngine;
using TMPro;
using Assets.Scripts.DAO.GameServer;

public class FriendRequestElementUI : MonoBehaviour
{
    public TMP_Text toUserNameTxt;
    public TMP_Text fromUserNameTxt;
    public TMP_Text requestId;

    public void SetFriendInfo(FriendRequestElement friend)
    {
        toUserNameTxt.text = friend.ToUserName;
        fromUserNameTxt.text = friend.FromUserName;
        requestId.text = friend.RequestId.ToString();
        // 다른 정보들도 필요하다면 추가로 할당
    }
}
