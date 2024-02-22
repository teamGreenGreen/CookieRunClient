using UnityEngine;
using TMPro;
using Assets.Scripts.DAO.GameServer;

public class FriendElementUI : MonoBehaviour
{
    public TMP_Text friendNameTxt;

    public void SetFriendInfo(FriendElement friend)
    {
        friendNameTxt.text = friend.UserName;
        // 다른 정보들도 필요하다면 추가로 할당
    }
}
