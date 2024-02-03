using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class Item : InGameObject
{
    [SerializeField]
    private int scorePoint = 0;
    [SerializeField]
    private int moneyPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    public int ScorePoint
    {
        get { return scorePoint; }
        set { scorePoint = value; }
    }
    public int MoneyPoint
    {
        get { return moneyPoint; }
        set { moneyPoint = value; }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
