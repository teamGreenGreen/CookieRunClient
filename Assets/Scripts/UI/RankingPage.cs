using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingPage : MonoBehaviour
{
    int page;
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        page = Int32.Parse(transform.Find("PageText").GetComponent<Text>().text);
        parent = transform.parent.parent.parent.parent.gameObject;
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        RankingManager rankScript = parent.transform.GetComponent<RankingManager>() as RankingManager;
        rankScript.SetRanks(page);
    }
}
