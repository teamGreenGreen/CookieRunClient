using Assets.Scripts.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    int page;
    string myRank;
    string[] ranks;
    double rankSize;
    const int PlayerNum = 15;
    GameObject RankContent;
    GameObject BtnContent;

    // Start is called before the first frame update
    private async void OnEnable()
    {
        // TODO : 서버에 연결하여 내 랭킹 요청
        RankGetRes res = await HttpManager.Instance.Post<RankGetRes>("rank/user", null);

        myRank = res.Rank;
        Transform myScore = transform.Find("My_Score");
        Transform myRankText = transform.Find("My_Rank_Text");
        Transform myName = transform.Find("My_Name");
        if (res.Rank != null)
        {
            myScore.GetComponent<Text>().text = $"{myRank.Split(":")[1]} 점";
            myRankText.GetComponent<Text>().text = $"{myRank.Split(":")[0]} 등";
        }
        else
        {
            myScore.GetComponent<Text>().text = $"게임을 먼저 진행하세요";
            myRankText.GetComponent<Text>().text = $"-";
        }
        GameObject go = GameObject.Find("UserName_Txt");
        string userName = go.GetComponent<TextMeshProUGUI>().text;
        myName.GetComponent<Text>().text = userName;

        SetRanks();
        SetButton();
    }
    public async void SetRanks(int page = 1)
    {
        RanksLoadReq req = new RanksLoadReq();
        req.PlayerNum = PlayerNum;
        req.Page = page;
        // TODO : api 서버에 연동하여 아래의 값 가져오기
        RanksLoadRes res = await HttpManager.Instance.Post<RanksLoadRes>("rank/total-rank", req);
        ranks = res.Ranks;

        RankContent = transform.Find("ScrollRanks").Find("Viewport").Find("Content").gameObject;
        RankContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);

        foreach (Transform child in RankContent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < ranks.Length; i++)
        {
            GameObject prefab = Resources.Load("Prefabs/Ranking_Item") as GameObject;
            GameObject item = MonoBehaviour.Instantiate(prefab) as GameObject;
            item.transform.localScale = new Vector3(1f, 1f, 1f);
            item.transform.SetParent(RankContent.transform, false);

            Transform rankText = item.transform.Find("RankText");
            Transform userName = item.transform.Find("UserName");
            Transform score = item.transform.Find("Score");

            rankText.GetComponent<Text>().text = $"{15 * (page - 1) + i + 1}등";
            userName.GetComponent<Text>().text = $"{ranks[i].Split(":")[0]}";
            score.GetComponent<Text>().text = $"{ranks[i].Split(":")[1]} 점";
        }
    }
    public async void SetButton()
    {
        // TODO : api 서버에서 rank 크기 가져오기
        RankSizeRes res = await HttpManager.Instance.Post<RankSizeRes>("rank/size-rank",null);
        rankSize = Math.Ceiling((double)res.Size/(float)PlayerNum);

        BtnContent = transform.Find("ScrollBtn").Find("Viewport").Find("Content").gameObject;

        foreach (Transform child in BtnContent.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < rankSize; i++)
        {
            GameObject prefab = Resources.Load("Prefabs/PageBtn") as GameObject;
            GameObject item = MonoBehaviour.Instantiate(prefab) as GameObject;
            item.transform.localScale = new Vector3(1f, 1f, 1f);
            item.transform.SetParent(BtnContent.transform, false);

            item.transform.Find("PageText").GetComponent<Text>().text = $"{i+1}";
        }
    }
}
