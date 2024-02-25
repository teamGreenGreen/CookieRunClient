using Assets.Scripts.DTO;
using UnityEngine;
using static NowCookie;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class UserInfoRes : ErrorCodeDTO
{
    public UserInfo UserInfo { get; set; }
}

public class UserInfoData : MonoBehaviour
{
    public static UserInfoData Instance
    {
        get
        {
            instance = FindObjectOfType<UserInfoData>();
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<UserInfoData>();
            }

            return instance;
        }
    }
    private static UserInfoData instance;

    public async Task RequestUserInfoPostAsync()
    {
        UserInfoRes res = await HttpManager.Instance.Post<UserInfoRes>("UserInfoLoad", null);
        LobbyUIManager.Instance.UpdateUserInfoUI(res);
    }
    public static async void RequestNowCookieId()
    {
        NowCookieRes res = await HttpManager.Instance.Post<NowCookieRes>("nowcookie", null);
        UpdateNowCookieId(res);
    }
    private static void UpdateNowCookieId(NowCookieRes res)
    {
        // UserInfoData에 nowCookieId를 저장하거나 필요한 처리 수행

        // LobbyUIManager의 인스턴스 찾기
        LobbyUIManager lobbyUIManager = FindObjectOfType<LobbyUIManager>();
        if (lobbyUIManager != null)
        {
            // LobbyUIManager의 메서드 호출
            lobbyUIManager.LoadAndSetCookieImage(res.NowCookieId);
        }
        else
        {
            Debug.LogError("LobbyUIManager not found.");
        }
    }
}
