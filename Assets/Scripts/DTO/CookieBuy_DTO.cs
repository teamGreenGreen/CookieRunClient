using Assets.Scripts.DTO;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookieBuy : MonoBehaviour
{
    public class CookieBuyReq
    {
        public int CookieId { get; set; }
    }
    public class CookieBuyRes : ErrorCodeDTO
    {

    }
}
