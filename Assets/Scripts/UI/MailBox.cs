using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MailBox : MonoBehaviour
{
    public int mailBoxId = -1;
    private TextMeshProUGUI mailNameText;
    private TextMeshProUGUI mailContentText;
    public TextMeshProUGUI sender;
    public TextMeshProUGUI content;

    public void OnClickedDetail()
    {
        GameObject gameObject = GameObject.Find("MailName_Txt");

        if (gameObject == null)
        {
            return;
        }

        mailNameText = gameObject.GetComponent<TextMeshProUGUI>();

        if (mailNameText != null && sender != null)
        {
            mailNameText.text = sender.text.ToString();
        }

        gameObject = GameObject.Find("MailContent_Txt");

        if (gameObject == null)
        {
            return;
        }

        mailContentText = gameObject.GetComponent<TextMeshProUGUI>();

        if (mailContentText != null && content != null)
        {
            mailContentText.text = content.text.ToString();
        }

        Buttons.curMailBoxId = mailBoxId;
    }

    public void ClearDetailInfo()
    {
        GameObject gameObject = GameObject.Find("MailName_Txt");

        if (gameObject == null)
        {
            return;
        }

        mailNameText = gameObject.GetComponent<TextMeshProUGUI>();

        if (mailNameText != null)
        {
            mailNameText.text = "";
        }

        gameObject = GameObject.Find("MailContent_Txt");

        if (gameObject == null)
        {
            return;
        }

        mailContentText = gameObject.GetComponent<TextMeshProUGUI>();

        if (mailContentText != null)
        {
            mailContentText.text = "";
        }

        Buttons.curMailBoxId = -1;
    }
}
