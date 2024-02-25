using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ExpBarControll : MonoBehaviour
{
    [SerializeField]
    private Slider expSlider;
    [SerializeField]
    private TextMeshProUGUI expTxt;

    private int maxExp = 6000;
    private int nowExp = 0;

    // Start is called before the first frame update
    void Start()
    {
        expSlider.value = (float)nowExp / (float)maxExp;
        expTxt.text = nowExp.ToString("N0") + '/' + maxExp.ToString("N0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
