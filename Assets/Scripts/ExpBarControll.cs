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

    private int _maxExp = 6000;
    public int maxExp {
        get => _maxExp;
        set
        {
            _maxExp = value;
            UpdateScore(_nowExp, _maxExp);
        }
    }

    private int _nowExp;
    public int nowExp
    {
        get => _nowExp;
        set
        {
            _nowExp = value;
            UpdateScore(_nowExp, _maxExp);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(nowExp, maxExp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateScore(int nowExp, int maxExp)
    {
        expSlider.value = (float)nowExp / (float)maxExp;
        expTxt.text = nowExp.ToString("N0") + " / " + maxExp.ToString("N0");
    }
}
