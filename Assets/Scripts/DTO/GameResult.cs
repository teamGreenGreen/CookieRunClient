using Assets.Scripts.DTO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour
{
    public class GameResultReq
    {
        // 젤리, 돈, 플레이 시간
        public Dictionary<int/*itemID*/, int/*count*/>? Items { get; set; }
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

    public static async void GameResultPost(Dictionary<int/*itemID*/, int/*count*/>? items, int score, int money, int speed, int currentCookieId)
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

        // 3초 지나면 LobbyScene으로 돌아가기
        LoadSceneManager loadSceneManager =  GameObject.Find("SceneManager").GetComponent<LoadSceneManager>();
        loadSceneManager.SceneChangeByEnumValue(ESceneName.LobbyScene);
    }
}
