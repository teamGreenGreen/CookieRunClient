using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookieSelectShop : MonoBehaviour
{
    [SerializeField]
    private GameObject braveCookieObj;
    [SerializeField]
    private GameObject lemonCookieObj;
    [SerializeField]
    private GameObject blueberrypieCookieObj;
    [SerializeField]
    private GameObject pancakeCookieObj;

    // Start is called before the first frame update
    void Start()
    {
        // 활성화 상태의 쿠키 이미지 알파값을 255로 설정, 비활성화 쿠키는 80으로 설정
        SetAlpha(braveCookieObj, 255);
        SetAlpha(lemonCookieObj, 80);
        SetAlpha(blueberrypieCookieObj, 80);
        SetAlpha(pancakeCookieObj, 80);
    }

    // 이미지 알파값을 설정하는 메서드
    private void SetAlpha(GameObject obj, int alphaValue)
    {
        if (obj != null)
        {
            Image image = obj.GetComponent<Image>();
            if (image != null)
            {
                Color color = image.color;
                color.a = alphaValue / 255f;
                image.color = color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
