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

    public async UniTask<TResponse> Post<TRequest, TResponse>(string path, TRequest dto)
    {
        string url = ServerURL + '/' + path;

        string json = JsonConvert.SerializeObject(dto);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json); // JSON 문자열을 바이트 배열로 변환

        UnityWebRequest req = new UnityWebRequest(url, "POST");
        req.uploadHandler = new UploadHandlerRaw(jsonBytes);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        await req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.LogError("HTTP Error: " + req.error);
        }
        else
        {
            string jsonResponse = req.downloadHandler.text;
            TResponse response = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

            return response;
        }

        return default(TResponse);
    }

}
