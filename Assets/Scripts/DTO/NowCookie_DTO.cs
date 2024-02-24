using Assets.Scripts.DTO;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NowCookie : MonoBehaviour
{
    public class EditNowCookieReq
    {
        public int CookieId { get; set; }
    }
    public class NowCookieRes : ErrorCodeDTO
    {
        public int NowCookieId { get; set; }
    }
    public class EditNowCookieRes : ErrorCodeDTO
    {

    }
}
