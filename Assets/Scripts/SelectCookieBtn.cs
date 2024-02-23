using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCookieBtn : MonoBehaviour
{
    public int cookieNum = -1;
    public void OnClick()
    {
        if (cookieNum == -1)
        {
            return;
        }
    }
}
