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
    private static int money = 0;
    private static int score = 0;
    private static bool[] alphabets = new bool[9];
    private static Texture2D[] bonusTimeTextures = new Texture2D[9];


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

    public static void AddAlphabet(string name)
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

    public static void AddScore(int curScore)
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

    public static void AddCoin(int curCoin)
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
}
