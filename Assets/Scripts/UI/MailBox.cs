using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MailBox : MonoBehaviour
{
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
        mailNameText.text = sender.ToString();

        gameObject = GameObject.Find("MailContent_Txt");

        if (gameObject == null)
        {
            return;
        }

        mailContentText = gameObject.GetComponent<TextMeshProUGUI>();
        mailContentText.text = content.ToString();
    }
}
