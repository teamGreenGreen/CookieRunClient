using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    int page;
    string myRank;
    string[] ranks;
    int rankSize;
    GameObject RankContent;
    GameObject BtnContent;

    // Start is called before the first frame update
    private void OnEnable()
    {
        // TODO : 서버에 연결하여 내 랭킹 요청
        myRank = "32:999999999";
        Transform myScore = transform.Find("My_Score");
        myScore.GetComponent<Text>().text = $"{myRank.Split(":")[1]} 점";
        Transform myRankText = transform.Find("My_Rank_Text");
        myRankText.GetComponent<Text>().text = $"{myRank.Split(":")[0]} 등";
        SetRanks();
        SetButton();
    }
    public void SetRanks(int page = 1)
    {
        // TODO : api 서버에 연동하여 아래의 값 가져오기
        ranks = new string[] { "남훈:1000", "정아:900", "준철:150", "초원:1", "세상아:1", "남훈:1", "정아:1", "준철:1", "초원:1", "세상아:1", "남훈:1", "정아:1", "준철:1", "초원:1", "세상아:1" };

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
    public void SetButton()
    {
        // TODO : api 서버에서 rank 크기 가져오기
        rankSize = 3;

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
