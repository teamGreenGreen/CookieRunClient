using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarControll : MonoBehaviour
{
    [SerializeField]
    private Slider expSlider;
    [SerializeField]
    private TextMeshProUGUI expTxt;

    private int maxExp = 6170;
    private int nowExp = 6000;

    // Start is called before the first frame update
    void Start()
    {
        expSlider.value = (float)nowExp / (float)maxExp;
        expTxt.text = string.Format("{0:#,###}/{1:#,###}", nowExp, maxExp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
