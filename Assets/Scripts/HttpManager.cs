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

public class HttpManager : MonoBehaviour
{
    private static HttpManager instance;
    public string ServerURL { get; set; } = "https://localhost:7034";

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

    // 서버에 post 요청을 보내는 함수
    // 요청, 응답의 타입을 지정해서 사용
    public async UniTask<TResponse> Post<TRequest, TResponse>(string path, TRequest dto)
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

            return response;
        }

        return default(TResponse);
    }

}
