using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using System.IO;
using static CreateMap;
using System.Text.RegularExpressions;
using System;
using Unity.VisualScripting;

public enum EItemId
{
    SilverCoin = 1,
    GoldCoin,
    JellyBean,
    JellyBearYellow,
    JellyBearPink,
    JellyBearBig,
    JellyB,
    JellyO,
    JellyN,
    JellyU,
    JellyS,
    JellyT,
    JellyI,
    JellyM,
    JellyE,
}

public class Player : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;

    public float jumpVelocity = 20.0f;
    public float groundHeight = 10.0f;

    public bool bGround = false;
    public bool bDoubleJump = false;
    public bool bSliding = false;

    public float speed = 1.0f;

    public float hp { get; set; }
    public float maxHp { get; set; }

    SpriteRenderer playerRender;

    private Animator anim;

    private bool bDeath = false;

    public string cookieName = "";

    public List<List<int>> cookieDatas = new List<List<int>>
    {
        new List<int> {0, 0},
        new List<int> {0, 0},
        new List<int> {5, 0},
        new List<int> {5, 5},
        new List<int> {10, 10},
    };

    Dictionary<int/*itemID*/, int/*count*/> acquiredItems = new Dictionary<int, int>
    {
        { 1, 0 },
        { 2, 0 },
        { 3, 0 },
        { 4, 0 },
        { 5, 0 },
        { 6, 0 },
        { 7, 0 },
        { 8, 0 },
        { 9, 0 },
        { 10, 0 },
        { 11, 0 },
        { 12, 0 },
        { 13, 0 },
        { 14, 0 },
        { 15, 0 },
    };

    public enum ECookieName
    {
        Brave = 1,
        Lemon,
        Blueberry_Pie,
        Pancake,
        End
    }

    void Start()
    {
        playerRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        ECookieName cookieNameEnum = (ECookieName)GameManager.Instance.currentCookieId;
        cookieName = cookieNameEnum.ToString();

        if(cookieName == "0")
        {
            cookieName = "Brave";
        }

        if (cookieNameEnum > ECookieName.End)
        {
            return;
        }

        // 동적으로 애니메이션 할당. 씬이 넘어올 때 cookieName에 해당하는 컨트롤러 명을 넘겨줘야 한다. 만약 안넘어오면 용감한 쿠키로 됨
        anim.runtimeAnimatorController = (RuntimeAnimatorController)Instantiate(Resources.Load($"Cookie\\Animation\\{cookieName}_Cookie_Controller", typeof(RuntimeAnimatorController)));

        hp = 10.0f;
        maxHp = 10.0f;
    }
    void Update()
    {
        if (hp <= 0.0f && !bDeath)
        {
            bDeath = true;
            // TODO.김초원 : 쿠키 ID 받을 수 있게 되면 주석 풀기
            GameManager.Instance.OpenLoadingCanvas(acquiredItems, /*currentCookieId*/ 1, (int)speed);
            GameManager.Instance.StopBackgroundScrolling();
            speed = 0f;
        }

        if (bGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bSliding = false;
                bGround = false;
                velocity.y = jumpVelocity;
            }
        }
        else
        {
            if (!bDoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                bDoubleJump = true;
                velocity.y = jumpVelocity * 0.8f;
                Debug.Log("더블점프!");
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            bSliding = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            bSliding = false;
        }

        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!bGround)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;

            if (velocity.y < 0)
            {
                Vector2 rayDirection = Vector2.down;
                Vector2 rayOrigin = new Vector2(pos.x, pos.y);
                int layerMask = 1 << LayerMask.NameToLayer("Ground");
                RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, 0.1f, layerMask);

                if (hit2D.collider != null)
                {
                    Ground ground = hit2D.collider.GetComponent<Ground>();

                    if (ground != null)
                    {
                        Debug.Log("ground hit!");
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        bGround = true;
                        bDoubleJump = false;
                    }
                    else
                    {
                        Debug.Log("collider hit, ground NULL!");
                    }
                }

                Debug.DrawRay(rayOrigin, rayDirection * 0.1f, Color.red);
            }
        }
        else
        {
            Vector2 rayDirection = Vector2.down;
            Vector2 rayOrigin = new Vector2(pos.x, pos.y);
            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, 0.1f, layerMask);

            if (hit2D.collider == null)
            {
                Debug.Log("떨어진다");
                bGround = false;
                bSliding = false;
            }

            Debug.DrawRay(rayOrigin, rayDirection * 0.1f, Color.yellow);
        }

        velocity.x += speed * Time.fixedDeltaTime;
        transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        if (item != null)
        {
            ++acquiredItems[item.ID];
            // BONUSTIME이면
            if (item.Alphabet)
                GameManager.Instance.AddAlphabet(item.name);

            int curScore = item.ScorePoint;
            curScore += curScore / 100 * cookieDatas[GameManager.Instance.currentCookieId][0];
            GameManager.Instance.AddScore(curScore);

            int money = item.MoneyPoint;
            money += money / 100 * cookieDatas[GameManager.Instance.currentCookieId][1];
            GameManager.Instance.AddCoin(money);

            Destroy(collision.gameObject);
        }
    }

    private void UpdateAnimation()
    {
        if (!bGround && !bDoubleJump)
        {
            anim.SetBool("jumping", true);
            anim.SetBool("sliding", false);
        }
        else if (!bGround && bDoubleJump)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("d_jumping", true);
        }
        else if (bGround && bSliding)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("d_jumping", false);
            anim.SetBool("sliding", true);
        }
        else
        {
            anim.SetBool("jumping", false);
            anim.SetBool("d_jumping", false);
            anim.SetBool("sliding", false);
        }
    }
    private void OnBecameInvisible()
    {
        hp = 0;
    }
}