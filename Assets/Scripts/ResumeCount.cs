using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ResumeCount : MonoBehaviour
{
    private float duration = 3f;
    private float countdownInterval = 1f;
    private float lastUpdateTime;
    public GameObject closeUI;
    private TextMeshProUGUI countDownText;

    private void OnEnable()
    {
        duration = 3f;

        lastUpdateTime = Time.realtimeSinceStartup;

        countDownText = GetComponent<TextMeshProUGUI>();
        countDownText.text = duration.ToString();
    }

    private void Update()
    {
        while (Time.realtimeSinceStartup - lastUpdateTime >= countdownInterval)
        {
            lastUpdateTime = Time.realtimeSinceStartup;
            duration--;

            countDownText.text = duration.ToString();

            if (duration <= 0)
            {
                GameManager.Instance.TogglePause();
                closeUI.SetActive(false);
            }
        }
    }
}
