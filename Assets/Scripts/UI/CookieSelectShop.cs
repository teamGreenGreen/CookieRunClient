using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookieSelectShop : MonoBehaviour
{
    public enum CookieType
    {
        Brave = 1,
        Lemon,
        BlueberryPie,
        Pancake
    }

    [SerializeField]
    private GameObject braveCookieObj;
    [SerializeField]
    private GameObject lemonCookieObj;
    [SerializeField]
    private GameObject blueberrypieCookieObj;
    [SerializeField]
    private GameObject pancakeCookieObj;
    private int myAcquiredCookieId;

    // Start is called before the first frame update
    void Start()
    {
        myAcquiredCookieId = LobbyUIManager.acquiredCookieId;
        string binaryCookieID = Convert.ToString(myAcquiredCookieId, 2).PadLeft(4,'0');

        for(int i = 0; i < 4; i++)
        {
            // 활성화 상태의 쿠키 이미지 알파값을 255로 설정, 비활성화 쿠키는 80으로 설정
            if (binaryCookieID[i] == 1) // 4 - i 번째 쿠키를 보유한 경우
            {
                SetAlpha((CookieType)4 - i, 255);
                SetButton((CookieType)4 - i, true);
            }
            else // 보유하지 않은 경우
            {
                SetAlpha((CookieType)4 - i, 80);
                SetButton((CookieType)4 - i, false);
            }
        }
    }

    // 이미지 알파값을 설정하는 메서드
    private void SetAlpha(CookieType cookieType, float alphaValue)
    {
        GameObject targetObj = null;

        // Enum 값에 따라 객체 선택
        switch (cookieType)
        {
            case CookieType.Brave:
                targetObj = braveCookieObj;
                break;
            case CookieType.Lemon:
                targetObj = lemonCookieObj;
                break;
            case CookieType.BlueberryPie:
                targetObj = blueberrypieCookieObj;
                break;
            case CookieType.Pancake:
                targetObj = pancakeCookieObj;
                break;
            default:
                Debug.LogError("Invalid CookieType");
                return;
        }

        // 선택된 객체의 Alpha 값을 설정
        if (targetObj != null)
        {
            Image image = targetObj.GetComponent<Image>();
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
    private void SetButton(CookieType cookieType, bool isCookieHave)
    {
        GameObject targetObj = null;

        // Enum 값에 따라 객체 선택
        switch (cookieType)
        {
            case CookieType.Brave:
                targetObj = braveCookieObj;
                break;
            case CookieType.Lemon:
                targetObj = lemonCookieObj;
                break;
            case CookieType.BlueberryPie:
                targetObj = blueberrypieCookieObj;
                break;
            case CookieType.Pancake:
                targetObj = pancakeCookieObj;
                break;
            default:
                Debug.LogError("Invalid CookieType");
                return;
        }

        Button selectButton = targetObj.transform.Find("Select_Btn").GetComponent<Button>();
        Button buyButton = targetObj.transform.Find("Buy_Btn").GetComponent<Button>();

        if(isCookieHave) // 쿠키 가진 경우 선택 활성화/구매 비활성화
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
}
