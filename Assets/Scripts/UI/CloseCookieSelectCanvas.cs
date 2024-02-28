using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseCookieSelectCanvas : MonoBehaviour
{
    private Button closeButton;
    [SerializeField]
    private Canvas targetCanvas;

    public void OnClick()
    {
        targetCanvas.gameObject.SetActive(false);
        UserInfoData.Instance.RequestNowCookieId();
    }
}
