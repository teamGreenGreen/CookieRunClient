using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CookieSelectShop;
using static NowCookie;
using UnityEngine.UI;

public class SelectCookieBtn : MonoBehaviour
{
    [SerializeField]
    public GameObject[] cookieObjects = new GameObject[4];
    public int cookieNum = -1;
    public void OnClick()
    {
        if (cookieNum == -1)
        {
            return;
        }

        SetNowCookieId();

        GameObject oldCookie = cookieObjects[LobbyUIManager.Instance.nowCookieId - 1];
        GameObject newCookie = cookieObjects[cookieNum - 1];

        SelectColor(newCookie);
        NonSelectColor(oldCookie);
    }

    private async void SetNowCookieId()
    {
        EditNowCookieRes res = await HttpManager.Instance.Post<EditNowCookieRes>("nowcookie/edit", new
        {
            CookieId = cookieNum
        });
        UserInfoData.Instance.RequestNowCookieId();
    }
    // 현재 선택된 쿠키의 color 조절
    private void SelectColor(GameObject cookieObject)
    {
        // 선택된 객체의 Color 값을 설정
        if (cookieObject != null)
        {
            Image image = cookieObject.GetComponent<Image>();
            if (image != null)
            {
                // 선택된 쿠키의 배경 color 초록색으로 바꾸기
                Color color = new Color(167f / 255f, 1f, 196f / 255f);
                image.color = color;
            }
            else
            {
                Debug.LogError("Renderer component not found on the target object.");
            }
        }
        else
        {
            Debug.LogError("Target object is null.");
        }
    }
    // 이전에 선택된 쿠키의 color 조절
    private void NonSelectColor(GameObject cookieObject)
    {
        // 선택된 객체의 Color 값을 설정
        if (cookieObject != null)
        {
            Image image = cookieObject.GetComponent<Image>();
            if (image != null)
            {
                // 선택된 쿠키의 배경 color 초록색으로 바꾸기
                Color color = new Color(255f, 255f, 255f);
                image.color = color;
            }
            else
            {
                Debug.LogError("Renderer component not found on the target object.");
            }
        }
        else
        {
            Debug.LogError("Target object is null.");
        }
    }
}
