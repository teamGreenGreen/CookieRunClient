using Assets.Scripts.DTO;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultReq
{
    public Dictionary<int/*itemID*/, int/*count*/> Items { get; set; }
    public int Score { get; set; }
    public int Money { get; set; }
    public int Speed { get; set; }
    public int CurrentCookieId { get; set; }
}

public class GameResultRes : ErrorCodeDTO
{
    public int Money { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    public int MaxExp { get; set; }
}

public class GameResult : MonoBehaviour
{
    private static GameResult instance;
    public static GameResult Instance
    {
        get
        {
            instance = FindObjectOfType<GameResult>();
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameResult>();
            }

            return instance;
        }
    }

    public async Task GameResultPostAsync(Dictionary<int/*itemID*/, int/*count*/> items, int score, int money, int speed, int currentCookieId)
    {
        GameResultRes res = await HttpManager.Instance.Post<GameResultRes>("GameResult", new
        {
            Items = items,
            Score = score,
            Money = money,
            Speed = speed,
            CurrentCookieId = currentCookieId
        });

        // 결과창 켜기
        OpenCanvas canvas = GameObject.Find("ResultCanvasController").GetComponent<OpenCanvas>();

        if (canvas != null)
        {
            canvas.OnClick();

            // 캔버스 내용 수정
            TextMeshProUGUI coinText = GameObject.Find("ResultCoinText").GetComponent<TextMeshProUGUI>();
            if (coinText)
            {
                coinText.text = money.ToString("N0");
            }

            TextMeshProUGUI scoreText = GameObject.Find("ResultScoreText").GetComponent<TextMeshProUGUI>();
            if (scoreText)
            {
                scoreText.text = score.ToString("N0");
            }
        }
    }
}
