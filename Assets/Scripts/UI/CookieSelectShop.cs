using Assets.Scripts.DTO.GameServer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CookieSelectShop;
using static NowCookie;

public class CookieSelectShop : MonoBehaviour
{
    [SerializeField]
    public GameObject[] cookieObjects = new GameObject[4];

    public static int myAcquiredCookieId;
    public int myNowCookieId;

    // Start is called before the first frame update
    void Start()
    {
        myAcquiredCookieId = LobbyUIManager.Instance.acquiredCookieId;
        myNowCookieId = LobbyUIManager.Instance.nowCookieId;
        string binaryCookieID = Convert.ToString(myAcquiredCookieId, 2).PadLeft(4, '0');

        for (int i = 0; i < 4; i++)
        {
            // 활성화 상태의 쿠키 이미지 알파값을 255로 설정, 비활성화 쿠키는 80으로 설정
            if (binaryCookieID[i] == '1') // 4 - i 번째 쿠키를 보유한 경우
            {
                SetAlpha(cookieObjects[4 - i - 1], 255f);
                SetButton(cookieObjects[4 - i - 1], true);
            }
            else if(binaryCookieID[i] == '0') // 보유하지 않은 경우
            {
                SetAlpha(cookieObjects[4 - i - 1], 80f);
                SetButton(cookieObjects[4 - i - 1], false);
            }
        }

        SetColor(cookieObjects[myNowCookieId - 1]);
    }
    // 이미지 알파값을 설정하는 메서드
    private void SetAlpha(GameObject cookieobject, float alphaValue)
    {
        // 선택된 객체의 Alpha 값을 설정
        if (cookieobject != null)
        {
            Image image = cookieobject.GetComponent<Image>();
            if (image != null)
            {
                Color color = image.color;
                color.a = alphaValue / 255f;
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

    // 선택 or 구입 버튼 활성화/비활성화
    private void SetButton(GameObject cookieObject, bool isCookieHave)
    {
        Button selectButton = cookieObject.transform.Find("Select_Btn").GetComponent<Button>();
        Button buyButton = cookieObject.transform.Find("Buy_Btn").GetComponent<Button>();

        if (isCookieHave) // 쿠키 가진 경우 선택 활성화/구매 비활성화
        {
            selectButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
        else // 가지고 있지 않은 경우 선택 비활성화/구매 활성화
        {
            selectButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
        }
    }

    // 현재 선택된 쿠키의 color 조절
    private void SetColor(GameObject cookieObject)
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
}
