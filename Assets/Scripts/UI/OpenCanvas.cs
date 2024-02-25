using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCanvas : MonoBehaviour
{
    [SerializeField]
    private Canvas targetCanvas;
    private Button openButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        targetCanvas.gameObject.SetActive(true);
    }

    public void OnClickedMailBox()
    {
        targetCanvas.gameObject.SetActive(true);
        Mail.MailListPost();
    }
}
