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
    // 젤리, 돈, 플레이 시간
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
    public static async Task GameResultPost(Dictionary<int/*itemID*/, int/*count*/> items, int score, int money, int speed, int currentCookieId)
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
        }

        // 캔버스 내용 수정
        TextMeshProUGUI coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
        if (coinText)
        {
            coinText.text = money.ToString("N0");
        }

        TextMeshProUGUI scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        if (scoreText)
        {
            scoreText.text = score.ToString("N0");
        }


        // 3초 지나면 LobbyScene으로 돌아가기

        LoadSceneManager loadSceneManager =  GameObject.Find("SceneManager").GetComponent<LoadSceneManager>();
        loadSceneManager.ReturnToLobbyAfterDelay(3.0f);
    }
}
