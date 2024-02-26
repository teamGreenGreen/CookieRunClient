using Assets.Scripts.DTO;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

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

        if(res.Result == EErrorCode.GameResultService_GetRedisUserInfoFail ||
           res.Result == EErrorCode.GameResultService_RewardCalcFail ||
           res.Result == EErrorCode.GameResultService_DBUserInfoUpdateFail ||
           res.Result == EErrorCode.GameResultService_AddLevelUpRewardFail ||
           res.Result == EErrorCode.GameResultService_RedisUpdateError)
        {
            GameManager.Instance.OnMessage("서버 연결이 끊어졌습니다. 다시 로그인 해주세요.");
            return;
        }
        else if(res.Result == EErrorCode.GameResultService_PlayerSpeedChangedDetected)
        {
            GameManager.Instance.OnMessage("속도 변경이 감지되었습니다. 다시 로그인 해주세요.");
            return;
        }
        else if (res.Result == EErrorCode.GameResultService_MoneyOrExpChangedDetected)
        {
            GameManager.Instance.OnMessage("점수나 코인 변경이 감지되었습니다. 다시 로그인 해주세요.");
            return;
        }

        // 결과창 켜기 -> 1초 대기
        StartCoroutine(WaitAndOpenCanvas(score, money));
    }

    private IEnumerator WaitAndOpenCanvas(int score,int money)
    {
        yield return new WaitForSeconds(1f);

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
