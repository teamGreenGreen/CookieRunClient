using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool isPaused = false;
    public int money { get; set; }
    public int score { get; set; }
    private bool[] alphabets = new bool[9];
    private Texture2D[] bonusTimeTextures = new Texture2D[9];
    private Player player;
    private float elapsedTime = 0.0f;
    public float decreaseSpeed = 2.0f; // 매 프레임마다 감소되는 속도
    private float currentDamage = 0f; // 현재 감소하는 양
    public int currentCookieId = 1;
    public Canvas messageCanvas;

    [SerializeField]
    private Image HPBar;

    public enum EAlphabet
    {
        JellyB,
        JellyO,
        JellyN,
        JellyU,
        JellyS,
        JellyT,
        JellyI,
        JellyM,
        JellyE
    }

    public static GameManager Instance
    {
        get
        {
            instance = FindObjectOfType<GameManager>();
            if (instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
            }

            return instance;
        }
    }

    public void Reset()
    {
        money = 0;
        score = 0;

        for(int i = 0; i < alphabets.Length; i++)
        {
            alphabets[i] = false;
        }

        elapsedTime = 0f;
    }

    public void StopBackgroundScrolling()
    {
        GameObject background = GameObject.Find("bg1");
        if(background != null)
        {
            ScrollingObject scrollingObj = background.GetComponent<ScrollingObject>();
            scrollingObj.speed = 0f;
        }

        background = GameObject.Find("bg2");
        if (background != null)
        {
            ScrollingObject scrollingObj = background.GetComponent<ScrollingObject>();
            scrollingObj.speed = 0f;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayer()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    public void OnMessage(string message)
    {
        if (messageCanvas != null)
        {
            TextMeshProUGUI alertMessage = messageCanvas.GetComponentInChildren<TextMeshProUGUI>();
            if (alertMessage != null)
            {
                alertMessage.text = message;
            }

            messageCanvas.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (player != null)
        {
            elapsedTime += Time.deltaTime;

            if (HPBar == null)
            {
                GameObject obj = GameObject.Find("GaugeBar");
                if (obj != null)
                {
                    HPBar = obj.GetComponent<Image>();
                }
            }

            if (elapsedTime > 1.0f && player.hp > 0.0f && HPBar != null)
            {
                elapsedTime = 0.0f;
                currentDamage = 0.7f; // 감소량 설정
            }

            if (currentDamage > 0 && player.hp > 0)
            {
                float decreaseAmount = currentDamage * Time.deltaTime * decreaseSpeed * 10; // 매 프레임마다 감소되는 양 계산
                player.hp -= decreaseAmount;
                HPBar.fillAmount = player.hp / (float)player.maxHp;

                currentDamage -= decreaseAmount; // 감소량 업데이트

                if (currentDamage <= 0)
                {
                    currentDamage = 0f;
                }
            }
        }
    }

    public void PlayerTakeDamage(float value)
    {
        currentDamage = value;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; 
    }

    public void AddAlphabet(string name)
    {
        GameObject imageObject = GameObject.Find("Bonus" + name);

        if (imageObject)
        {
            if (Enum.TryParse(name, out EAlphabet result))
            {
                int value = (int)result;
                if (alphabets[value] == true)
                    return;

                // 모든 알파벳이 채워졌는지 확인
                bool hasAllAlphabets = true;
                foreach (bool hasAlphabet in alphabets)
                {
                    if (!hasAlphabet)
                    {
                        hasAllAlphabets = false;
                        break;
                    }
                }

                // alphabet 다 모으면 이동하는 씬으로 이동
                if (hasAllAlphabets)
                {
                }

                // UI 표시
                alphabets[value] = true;
                Image image = imageObject.GetComponent<Image>();
                if (image)
                {
                    Color imageColor = image.color;
                    imageColor.a = 1.0f;
                    image.color = imageColor;
                }
            }
        }
    }

    public void AddScore(int curScore)
    {
        GameObject gameObject = GameObject.Find("ScoreText");

        if (!gameObject)
            return;

        score += curScore;
        
        TextMeshProUGUI curScoreText = gameObject.GetComponent<TextMeshProUGUI>();

        if (curScoreText)
        {
            curScoreText.text = score.ToString("N0");
        }
    }

    public void AddCoin(int curCoin)
    {
        GameObject gameObject = GameObject.Find("CoinText");

        if (!gameObject)
            return;

        money += curCoin;

        TextMeshProUGUI curMoneyText = gameObject.GetComponent<TextMeshProUGUI>();

        if (curMoneyText)
        {
            curMoneyText.text = money.ToString("N0");
        }
    }

    public async void OpenLoadingCanvas(Dictionary<int, int> acquiredItems, int currentCookieId, int speed)
    {
        OpenCanvas canvas = GameObject.Find("LoadingCanvasController").GetComponent<OpenCanvas>();

        if(canvas != null)
        {
            canvas.OnClick();
        }

        await GameResult.Instance.GameResultPostAsync(acquiredItems, score, money, speed, currentCookieId);
    }
}
