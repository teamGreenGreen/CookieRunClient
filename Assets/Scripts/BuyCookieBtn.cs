using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CookieSelectShop;
using UnityEngine.UI;
using static CookieBuy;
using System.Threading.Tasks;
using TMPro;

public class BuyCookieBtn : MonoBehaviour
{
    [SerializeField]
    public GameObject ErrorPerfab;
    public GameObject myGameObject;
    public int cookieNum = -1;
    private bool isBuySuccess;
    public async void OnClick()
    {
        if (cookieNum == -1)
        {
            return;
        }

        isBuySuccess = await BuyCookie();
        if (isBuySuccess)
        {
            NewColor(myGameObject);
            SetButton(myGameObject);
        }
    }
    private async Task<bool> BuyCookie()
    {
        CookieBuyRes res = await HttpManager.Instance.Post<CookieBuyRes>("cookiebuy", new
        {
            CookieId = cookieNum
        });
        if (res.Result == EErrorCode.None)
        {
            UserInfoData.Instance.RequestUserInfoPostAsync();
            return true;
        }
        else if (res.Result == EErrorCode.NotEnoughDiamond)
        {
            GameManager.Instance.OnMessage("다이아몬드가 부족합니다.");
            return false;
        }
        else
        {
            return false;
        }
    }
    // 현재 선택된 쿠키의 color 조절
    private void NewColor(GameObject cookieObject)
    {
        // 선택된 객체의 Color 값을 설정
        if (cookieObject != null)
        {
            Image image = cookieObject.GetComponent<Image>();
            if (image != null)
            {
                Color color = new Color(255f, 255f, 255f, 255f);
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
    private void SetButton(GameObject cookieObject)
    {
        Button selectButton = cookieObject.transform.Find("Select_Btn").GetComponent<Button>();
        Button buyButton = cookieObject.transform.Find("Buy_Btn").GetComponent<Button>();

        selectButton.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(false);

    }
}
