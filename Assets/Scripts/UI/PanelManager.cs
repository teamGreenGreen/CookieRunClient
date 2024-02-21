using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject panelFriendList;
    public GameObject panelFriendReqList;
    public GameObject panelFriendReq;
    // 필요에 따라 추가 패널들을 변수로 선언

    public void ShowPanelFriendList()
    {
        panelFriendList.SetActive(true);
        panelFriendReqList.SetActive(false);
        panelFriendReq.SetActive(false);
        // 다른 패널들도 비활성화 처리
    }

    public void ShowPanelFriendReqList()
    {
        panelFriendList.SetActive(false);
        panelFriendReqList.SetActive(true);
        panelFriendReq.SetActive(false);
        // 다른 패널들도 비활성화 처리
    }

    public void ShowPanelFriendReq()
    {
        panelFriendList.SetActive(false);
        panelFriendReqList.SetActive(false);
        panelFriendReq.SetActive(true);
        // 다른 패널들도 비활성화 처리
    }
}
