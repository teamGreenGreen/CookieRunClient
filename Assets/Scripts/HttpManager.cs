using Assets.Scripts.DTO;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;

public class HttpManager : MonoBehaviour
{
    [SerializeField]
    private GameObject notificationUI;

    private static HttpManager instance;
    public const string GAME_SERVER_URL = "https://localhost:7270";
    public const string AUTH_SERVER_URL = "https://localhost:7034";

    public string ServerURL { get; set; } = "https://localhost:7270";

    public Int64 userId;
    public Int64 uid;
    public string sessionId;

    public static HttpManager Instance
    {
        get
        {
            instance = FindObjectOfType<HttpManager>();
            if(instance == null)
            {
                GameObject go = new GameObject("HttpManager");
                instance = go.AddComponent<HttpManager>();
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

    public async UniTask<TResponse> LoginServer<TResponse>(string path, object dto) where TResponse : ErrorCodeDTO
    {
        string url = ServerURL + '/' + path;

        string json = JsonConvert.SerializeObject(dto); // DTO 객체를 직렬화해서 string에 담는다
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json); // JSON 문자열을 바이트 배열로 변환

        UnityWebRequest req = new UnityWebRequest(url, "POST");
        // HTTP 요청의 본문에 JSON 데이터를 넣기 위해 업로드 핸들러를 설정
        req.uploadHandler = new UploadHandlerRaw(jsonBytes);
        // 응답 데이터를 메모리 버퍼에 저장하는 다운로드 핸들러를 설정
        req.downloadHandler = new DownloadHandlerBuffer();
        // 전송 데이터가 JSON 형식임을 서버에 알려줌

        // 헤더 설정
        req.SetRequestHeader("Content-Type", "application/json");

        await req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError("HTTP Error: " + req.error);
        }
        else
        {
            string jsonResponse = req.downloadHandler.text;
            // JSON 데이터를 역직렬화해서 DTO 객체에 담는다
            TResponse response = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

            if (response.Result == EErrorCode.SessionIdNotFound)
            {
                ActiveNotificationUi();
            }

            return response;
        }

        return default(TResponse);
    }

    // 서버에 post 요청을 보내는 함수
    // 요청, 응답의 타입을 지정해서 사용
    public async UniTask<TResponse> Post<TResponse>(string path, object dto) where TResponse : ErrorCodeDTO
    {
        string url = ServerURL + '/' + path;

        string json = JsonConvert.SerializeObject(dto); // DTO 객체를 직렬화해서 string에 담는다
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json); // JSON 문자열을 바이트 배열로 변환

        UnityWebRequest req = new UnityWebRequest(url, "POST");
        // HTTP 요청의 본문에 JSON 데이터를 넣기 위해 업로드 핸들러를 설정
        req.uploadHandler = new UploadHandlerRaw(jsonBytes);
        // 응답 데이터를 메모리 버퍼에 저장하는 다운로드 핸들러를 설정
        req.downloadHandler = new DownloadHandlerBuffer();
        // 전송 데이터가 JSON 형식임을 서버에 알려줌

        // 헤더 설정
        req.SetRequestHeader("Content-Type", "application/json");
        // 인증 정보 설정
        req.SetRequestHeader("Uid", uid.ToString());
        req.SetRequestHeader("Authorization", sessionId);

        await req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError("HTTP Error: " + req.error);
        }
        else
        {
            string jsonResponse = req.downloadHandler.text;
            // JSON 데이터를 역직렬화해서 DTO 객체에 담는다
            TResponse response = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

            if(response.Result == EErrorCode.SessionIdNotFound)
            {
                ActiveNotificationUi();
            }

            return response;
        }

        return default(TResponse);
    }

    // body를 전송하지 않는 Post 요청
    public async UniTask<TResponse> Post<TResponse>(string path)
    {
        string url = ServerURL + '/' + path;

        UnityWebRequest req = new UnityWebRequest(url, "POST");
        // 응답 데이터를 메모리 버퍼에 저장하는 다운로드 핸들러를 설정
        req.downloadHandler = new DownloadHandlerBuffer();

        // 헤더 설정
        // 인증 정보 설정
        req.SetRequestHeader("Uid", uid.ToString());
        req.SetRequestHeader("Authorization", sessionId);

        await req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError("HTTP Error: " + req.error);
        }
        else
        {
            string jsonResponse = req.downloadHandler.text;
            // JSON 데이터를 역직렬화해서 DTO 객체에 담는다
            TResponse response = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

            return response;
        }

        return default(TResponse);
    }

    public void SetAuthInfo(Int64 uid, string sessionId)
    {
        this.uid = uid;
        this.sessionId = sessionId;
    }

    public void ActiveNotificationUi()
    {
        notificationUI.SetActive(true);
        TMP_Text tmp = notificationUI.GetComponentInChildren<TMP_Text>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            tmp.text = "서버에 연결할 수 없습니다";
        }
        else
        {
            tmp.text = "서버와 연결이 끊어졌습니다";
        }
    }
}
