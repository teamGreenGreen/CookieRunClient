using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPanel;
    [SerializeField]
    private GameObject closedPanel1;
    [SerializeField]
    private GameObject closedPanel2;

    public void OnClick()
    {
        closedPanel1.gameObject.SetActive(false);
        closedPanel2.gameObject.SetActive(false);
        targetPanel.gameObject.SetActive(true);
    }
}
