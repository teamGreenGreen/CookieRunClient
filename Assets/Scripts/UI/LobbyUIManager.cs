using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static Mail;
using Unity.VisualScripting;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField]
    public Image mainCookieImage;
    // Start is called before the first frame update
    public TextMeshProUGUI gemCountText;
    public TextMeshProUGUI coinCountText;
    public TextMeshProUGUI levelText;
    public Slider slider;
    public TextMeshProUGUI userNameText;
    public ScrollRect scrollRect;
    public GameObject mailPrefab;
    public int acquiredCookieId;
    public int nowCookieId;
    public Canvas loadingCanvas;

    private static LobbyUIManager instance;
    public static LobbyUIManager Instance
    {
        get
        {
            instance = FindObjectOfType<LobbyUIManager>();
            if (instance == null)
            {
                GameObject go = new GameObject("LobbyUIManager");
                instance = go.AddComponent<LobbyUIManager>();
            }

            return instance;
        }
    }

    void Start()
    {
        GameObject gameObject = GameObject.Find("GemGount_Txt");
        if (gameObject != null)
        {
            gemCountText = gameObject.GetComponent<TextMeshProUGUI>();
        }

        gameObject = GameObject.Find("CoinCount_Txt");
        if(gameObject != null)
        {
            coinCountText = gameObject.GetComponent<TextMeshProUGUI>();
        }

        gameObject = GameObject.Find("Level_Txt");
        if(gameObject != null)
        {
            levelText = gameObject.GetComponent<TextMeshProUGUI>();
        }

        gameObject = GameObject.Find("Slider");
        if(gameObject != null)
        {
            slider = gameObject.GetComponent<Slider>();
        }

        gameObject = GameObject.Find("UserName_Txt");
        if(gameObject != null)
        {
            userNameText = gameObject.GetComponent<TextMeshProUGUI>();
        }

        gameObject = GameObject.Find("LoadingCanvas");
        if (gameObject != null)
        {
            loadingCanvas = gameObject.GetComponent<Canvas>();
        }

        StartCoroutine(WaitAndOpenCanvas(1.0f));
    }

    private IEnumerator WaitAndOpenCanvas(float time)
    {
        yield return new WaitForSeconds(time);

        if(loadingCanvas != null)
        {
            loadingCanvas.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void ClearAllMails()
    {
        if (scrollRect != null)
        {
            foreach (Transform child in scrollRect.content.transform)
            {
                MailBox mailBox = child.GetComponent<MailBox>();
                mailBox.ClearDetailInfo();
                Destroy(child.gameObject);
            }
        }
    }

    public void UpdateUserInfoUI(UserInfoRes res)
    {
        if (res == null)
        {
            return;
        }

        if(levelText == null ||
            slider == null ||
            coinCountText == null ||
            gemCountText == null ||
            userNameText == null)
        {
            return;
        }

        levelText.text = res.UserInfo.Level.ToString("N0");
        ExpBarControll expBarController = slider.GetComponent<ExpBarControll>();
        expBarController.nowExp = res.UserInfo.Exp / 1000;
        
        int maxExp = 0;
      
        if (res.UserInfo.Level == 1) maxExp = 6000;
        else if (res.UserInfo.Level == 2) maxExp = 10000;
        else if (res.UserInfo.Level == 3) maxExp = 30000;
        else maxExp = 50000;

        expBarController.maxExp = maxExp;
        coinCountText.text = res.UserInfo.Money.ToString("N0");
        gemCountText.text = res.UserInfo.Diamond.ToString("N0");
        acquiredCookieId = res.UserInfo.AcquiredCookieId;
        userNameText.text = res.UserInfo.UserName.ToString();
    }

    public void UpdateMailListUI(MailListRes res)
    {
        if (scrollRect == null)
        {
            return;
        }

        RectTransform content = scrollRect.content;

        foreach (MailInfo mail in res.MailList)
        {
            GameObject mailObject = Instantiate(mailPrefab, content);
            MailBox mailBox = mailObject.GetComponent<MailBox>();
            mailBox.mailBoxId = mail.MailboxId;

            Transform textTransform = mailObject.transform.Find("preview");
            if(textTransform != null)
            {
                TextMeshProUGUI mailInfoText = textTransform.GetComponent<TextMeshProUGUI>();
                
                if(mailInfoText != null)
                {
                    mailInfoText.text = mail.Sender.ToString();
                }
            }

            Transform senderTransform = mailObject.transform.Find("sender");
            if (senderTransform != null)
            {
                TextMeshProUGUI senderText = senderTransform.GetComponent<TextMeshProUGUI>();

                if (senderText != null)
                {
                    senderText.text = mail.Sender.ToString();
                    mailBox.sender.text = mail.Sender.ToString();
                }
            }

            Transform contentTransform = mailObject.transform.Find("content");
            if (contentTransform != null)
            {
                TextMeshProUGUI contentText = contentTransform.GetComponent<TextMeshProUGUI>();

                if (contentText != null)
                {
                    contentText.text = mail.Content.ToString();
                    mailBox.content.text = mail.Content.ToString();
                }
            }
            Transform countTransform = mailObject.transform.Find("count");
            if (countTransform != null)
            {
                TextMeshProUGUI countText = countTransform.GetComponent<TextMeshProUGUI>();
                if (countText != null)
                {
                    countText.text = mail.Count.ToString();
                }
            }

            if (mail.RewardType == "diamond")
            {
                Transform diamondTransform = mailObject.transform.Find("diamond");

                if(diamondTransform != null && countTransform != null)
                {
                    diamondTransform.gameObject.SetActive(true);
                    countTransform.gameObject.SetActive(true);
                }
            }
            else if(mail.RewardType == "money")
            {
                Transform moneyTransform = mailObject.transform.Find("coin");

                if (moneyTransform != null)
                {
                    moneyTransform.gameObject.SetActive(true);
                    countTransform.gameObject.SetActive(true);
                }
            }
        }
    }
    public void LoadAndSetCookieImage(int cookieId)
    {
        nowCookieId = cookieId;
        GameManager.Instance.currentCookieId = cookieId;

        // 이미지 파일명을 생성
        string imageName = "Cookie" + cookieId;

        // Resources 폴더 내의 CookieImages 폴더에서 이미지 로드
        Sprite cookieSprite = Resources.Load<Sprite>("Cookie/StandCookie/" + imageName);

        // 이미지가 로드되었는지 확인 후 교체
        if (cookieSprite != null)
        {
            mainCookieImage.sprite = cookieSprite;
        }
        else
        {
            Debug.LogError("Image not found for cookieId: " + cookieId);
        }
    }
}
