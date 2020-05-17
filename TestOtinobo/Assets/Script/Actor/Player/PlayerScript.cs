using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

public class PlayerScript : MonoBehaviour
{
    ////playerの画像を指定するところ
    //[SerializeField, Header("上方向のプレイヤー")] Sprite UpPlayer;
    //[SerializeField, Header("下方向のプレイヤー")] Sprite DownPlayer;

    //メインカメラの当たり判定
    [SerializeField, Header("プレイヤーを追跡するカメラ")] Camera Maincamera;
    [SerializeField, Header("プレイヤーが死んだときに出すパーティクルオブジェクト")] GameObject playerDeathObj;

    #region//インスペクターで設定する
    [Header("ジャンプ速度")] public float jumpSpeed;
    [Header("ジャンプする高さ")] public float jumpHeight;
    [Header("ジャンプする長さ")] public float jumpLimitTime;
    [Header("ジャンプの速さ表現")] public AnimationCurve jumpCurve;
    [Header("踏みつけ判定の高さの割合")] public float stepOnRate;
    [Header("ヒップドロップジャンプの距離")] public float HipJump;
    [Header("パラソルのバランス5.0～9.6の範囲で設定すること")] public float Parasol; 
    [Header("リカバリージャンプの高さ")] public float Recovery;
    [Header("リカバリージャンプの長さ")] public float jumpLimitTime2;
    [Header("ヒップドロップタイム")] public float HipLimitTime;
    public enum ColorState
    {
        White, Red, Blue, Green,
    }
    public ColorState CS;//色を追加する場合エネミージャンプにも同様に色を増やすこと
    public string Color;
    #endregion
    public Text jumpText;

    //[SerializeField, Header("プレイヤーが死んだSE")] public AudioClip PlayerDeadSE;
    [SerializeField, Header("プレイヤーがアイテムをとったSE")] public AudioClip ItemSE;
    [SerializeField, Header("プレイヤーがジャンプしたSE")] public AudioClip JumpSE;
    [SerializeField, Header("敵が死んだSE")] public AudioClip EnemyDeadSE;
    [SerializeField, Header("ヒップドロップ発動時のSE")] public AudioClip HipDropSE;
    AudioSource audioSource;
    private Animator _animator;

    //プレイヤーの画像変更に必要なもの
    SpriteRenderer MainSpriteRenderer;

    [SerializeField, Header("Spriteの格納")] public Sprite White;
    public Sprite Green;
    public Sprite Red;
    public Sprite Blue;
    [SerializeField, Header("地面にぶつかったときのエフェクト")]
    public GameObject GreenEffect;
    public GameObject RedEffect;
    public GameObject BlueEffect;
    //取得した蒸気の数
    public float SteamPoint;

    public int score = 0;//スコアの追加(4/17)
    private int numScore = 0;//ジャンプのご褒美を与えるための500区切りのスコア
    public int AddPoint = 100;//普通のスコア加算
    public int HighPoint = 200;//スコア加算の高いポイント

    public int scoreline = 0;

    private float hinan;
    private Rigidbody2D rig2D;
    private Vector2 gravity;
    private bool Rcv = false;

    private bool damageFlag; //ダメージを受けているか判定
    private bool isDeadFlag; //死亡フラグ
    public bool GetisDeadFlag=false;//死亡フラグを取得する
    private bool isJump = false;
    private float jumpPos = 0.0f;
    private bool isOtherJump=false;
    private float jumpTime;
    private float otherJumpHeight = 0.0f;
    private bool isDown = false;
    private BoxCollider2D capcol = null;
    private bool isGround = false;
    private bool isHead = false;

    private bool IJump = false;
    private float IJumpC = 0;
    private float IJumpH = 20;
    private float xSpeed = 1;

    private bool on_ground = false;
    private bool hiptime = false;
    private bool hip=false;
    private bool stop = false;
    public bool anim1 = false;//animetion
    public bool anim2 = false;//animation
    public bool ColorWallRight = false;
    public bool ColorWallLeft = false;
    public bool ColorWallBottom = false;

    #region//各色のRGBA設定
    //[Header("whiteのRGBA")] public byte WhiteR = 255;
    //public byte WhiteG = 255, WhiteB = 255, WhiteA = 255;//ホワイトの時のRGBA
    //[Header("greenのRGBA")] public byte GreenR = 113;
    //public byte GreenG = 250, GreenB = 120, GreenA = 255;//グリーン
    //[Header("redのRGBA")] public byte RedR = 255;
    //public byte RedG = 47, RedB = 20, RedA = 255;//レッド
    //[Header("blueのRGBA")] public byte BlueR = 87;
    //public byte BlueG = 117, BlueB = 255, BlueA = 255;//ブルー　
    #endregion
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        MainSpriteRenderer = GetComponent<SpriteRenderer>();
        rig2D = GetComponent<Rigidbody2D>();
        capcol = GetComponent<BoxCollider2D>();
        FadeManager.FadeIn();
        hinan = Parasol;
        jumpText.text = string.Format("ジャンプ残り "+ IJumpC + " 回");
        CS = ColorState.White;
    }
    private void Awake()//追加
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.0f, transform.rotation.w);
        float axis = Input.GetAxis("Horizontal");
        Vector2 velocity = rig2D.velocity;
        gravity = new Vector2(0.0f, -9.81f + Parasol);

        float ySpeed = GetYSpeed();
        //プレイヤーが動いていたらaxisの値に5かけて動かす
        if (axis > 0 && !hiptime && !hip && ColorWallRight == false && ColorWallLeft == true)
        {
            velocity.x = axis * 5;
        }
        if (axis < 0 && !hiptime && !hip && ColorWallRight == true && ColorWallLeft == false)
        {
            velocity.x = axis * 5;
        }
        if (axis != 0 && !hiptime && !hip && ColorWallRight == false && ColorWallLeft == true && ColorWallBottom == true)
        {
            velocity.x = axis * 5;
        }
        if (axis !=0 && !hiptime && !hip && ColorWallRight == true && ColorWallLeft == false && ColorWallBottom == true)
        {
            velocity.x = axis * 5;
        }
        if (axis != 0 && !hiptime && !hip && ColorWallRight == true && ColorWallLeft == true)
        {
            velocity.x = axis * 5;
        }
        if (axis != 0&&!hiptime&&!hip&&ColorWallRight==false&&ColorWallLeft==false)
        {
            velocity.x = axis * 5;
        }
     
        else if (axis != 0 && hiptime&&!hip)
        {
            velocity.x = axis *5* HipJump;
        }
        if (!stop)
        {
            rig2D.velocity = new Vector2(velocity.x * xSpeed, ySpeed);
        }
        else
        {
            rig2D.velocity = new Vector2(0,0);
            xSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.S))
        {
            anim1 = true; //animのtrue追加
            audioSource.PlayOneShot(HipDropSE);
            hip = true;
            stop = true;
            xSpeed = 1;
            Invoke("Hip", HipLimitTime);
        }
        if (on_ground)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            MainSpriteRenderer.color = new Color(1f, 1f, 1f, level);
        }
        //ジャンプ(Spaceキー)が押されたらアイテムジャンプを使用する
        if (IJump && Input.GetButtonDown("Jump"))
        {
            if (IJumpC > 0)
            {
                IJumpC--;
                jumpText.text = string.Format("ジャンプ残り {0} 回", IJumpC);
                audioSource.PlayOneShot(JumpSE);
            }
            else
            {
                IJump = false;
                return;
            }
            otherJumpHeight = IJumpH;    //踏んづけたものから跳ねる高さを取得する          
            jumpPos = transform.position.y; //ジャンプした位置を記録する 
            isOtherJump = true;
            hip = false;
            isJump = false;
            jumpTime = 0.0f;
            Parasol = hinan;
            hiptime = false;
            //Debug.Log("ジャンプしたよ");       
        }
        switch (CS)
        {
            case ColorState.White:
                //GetComponent<Renderer>().material.color = new Color32(WhiteR, WhiteG, WhiteB, WhiteA);
                GetComponent<SpriteRenderer>().sprite = White;
                Color = "white";
                break;
            case ColorState.Red:
                //GetComponent<Renderer>().material.color = new Color32(RedR, RedG, RedB, RedA);
                GetComponent<SpriteRenderer>().sprite = Red;
                Color = "Red";
                break;
            case ColorState.Green:
                //GetComponent<Renderer>().material.color = new Color32(GreenR, GreenG, GreenB, GreenA);
                GetComponent<SpriteRenderer>().sprite = Green;
                Color = "Green";
                break;
            case ColorState.Blue:
                //GetComponent<Renderer>().material.color = new Color32(BlueR, BlueG, BlueB, BlueA);
                GetComponent<SpriteRenderer>().sprite = Blue;
                Color = "Blue";
                break;
        }
        //アニメーションの条件取得
        if (anim1 == true)
        {
            _animator.SetBool("OnceAnim", true);
            Invoke("StopAnim", 0.5f);
        }
        if (anim1 == false)
        {
            _animator.SetBool("OnceAnim", false);
        }
        if (anim2 == true)
        {
            _animator.SetBool("HipAnim", true);
            Invoke("StopAnim", 1f);
        }
        if (anim2 == false)
        {
            _animator.SetBool("HipAnim", false);
        }
        //if(ColorWall==true)
        //{
        //    velocity.x =  0;           
        //}
        ////ダメージを受けたら一定時間無敵にして点滅させる(ダメージ関連を追加することは無いと思うけど念のため残してます)
        //if (damageFlag)
        //{
        //    float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
        //    MainSpriteRenderer.color = new Color(1f, 1f, 1f, level);
        //}

        //死亡時のフラグ(エフェクトなど追々追加)
        if (isDeadFlag)
        {
            gameObject.SetActive(false);
            rig2D.velocity = new Vector2(0, 0);
            //Destroy(gameObject);
            //SceneManager.LoadScene(0);
        }
        if (isDeadFlag == true)
        {
            GetisDeadFlag = true;
        }
    }
    private void StopAnim()
    {
        anim1 = false;
        anim2 = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (CS == 0)
            {
                Camera.main.gameObject.GetComponent<CameraScritpt>().Shake();
                Instantiate(playerDeathObj, transform.position, Quaternion.identity);

                //プレイヤー死亡
                isDeadFlag = true;
            }
            else
            {
                audioSource.PlayOneShot(JumpSE);
                otherJumpHeight = Recovery;    //踏んづけたものから跳ねる高さを取得する
                jumpPos = transform.position.y; //ジャンプした位置を記録する 
                isOtherJump = true;
                Rcv = true;
                isJump = false;
                hip = false;
                jumpTime = 0.0f;
                on_ground = true;
                StartCoroutine("WaitForit");
                //着地時のエフェクト
                switch (CS)
                {
                    case ColorState.Blue:
                        Instantiate(BlueEffect, transform.position, Quaternion.identity);
                        break;
                    case ColorState.Green:
                        Instantiate(GreenEffect, transform.position, Quaternion.identity);
                        break;
                    case ColorState.Red:
                        Instantiate(RedEffect, transform.position, Quaternion.identity);
                        break;
                }
                CS = ColorState.White;
                return;
            }
        }
        if (other.gameObject.tag == "Rain")
        {
            Camera.main.gameObject.GetComponent<CameraScritpt>().Shake();
            Instantiate(playerDeathObj, transform.position, Quaternion.identity);
            isDeadFlag = true;
        }
        if (other.gameObject.tag == "ColorBlock")
        {
                otherJumpHeight = 0.00001f;    //踏んづけたものから跳ねる高さを取得する          
                jumpPos = transform.position.y; //ジャンプした位置を記録する 
                isOtherJump = true;
                hip = false;
                isJump = false;
                jumpTime = 0.0f;
                Parasol = hinan;
                hiptime = false;
                hip = false;
        }
       

        if (other.collider.tag == "Enemy" || other.collider.tag == "HighEnemy")
        {
            //踏みつけ判定になる高さ
            float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));
            //踏みつけ判定のワールド座標
            float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;
            //Debug.Log("接触したよ");
            foreach (ContactPoint2D p in other.contacts)
            {
                if (p.point.y < judgePos)
                {
                    EnemyJump o = other.gameObject.GetComponent<EnemyJump>();
                    if (o != null)
                    {
                        anim1 = true;//animのtrue
                        audioSource.PlayOneShot(EnemyDeadSE);
                        audioSource.PlayOneShot(JumpSE);
                        otherJumpHeight = o.boundHeight;    //踏んづけたものから跳ねる高さを取得する
                        o.playerjump = true;        //踏んづけたものに対して踏んづけた事を通知する
                        CS = (ColorState)Enum.ToObject(typeof(ColorState), o.GetColor()); 
                        jumpPos = transform.position.y; //ジャンプした位置を記録する 
                        isOtherJump = true;
                        isJump = false;
                        jumpTime = 0.0f;
                        //Debug.Log("ジャンプしたよ");
                        Camera.main.gameObject.GetComponent<CameraScritpt>().Shake();
                        if (other.collider.tag == "Enemy")
                        {
                            score += AddPoint / 2;//スコアを足す(4/17)
                            numScore += AddPoint / 2;
                        }
                        else
                        {
                            score += HighPoint / 2;
                            numScore += HighPoint / 2;
                        }
                    }
                    else
                    {
                        Debug.Log("ObjectCollisionが付いてないよ!");
                    }
                }
                else
                {
                    isDown = true;
                    break;
                }
            }
           
        }
        //300点取る度にジャンプを一回増やす
        if(numScore >= 300)
        {
            IJumpC += 1;
            numScore = 0;
            IJump = true;
            jumpText.text = string.Format("ジャンプ残り {0} 回", IJumpC);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ColorBlock")
        {
            //foreach (ContactPoint2D point in other.contacts)
            //{
            //    if (point.point.x - transform.position.x > 0.1)
            //    {
            //        Debug.Log("right");
            //        ColorWallRight = false;
            //    }
            //    else if (point.point.x - transform.position.x < -0.1)
            //    {
            //        Debug.Log("hitting");
            //        ColorWallLeft = false;
            //    }
            //    else if (point.point.y - transform.position.y < -0.2)
            //    {
            //        ColorWallBottom = false;
            //    }
            //}
            ColorWallRight = false;
            ColorWallLeft = false;
            ColorWallBottom = false;
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "ColorBlock")
        {
            foreach (ContactPoint2D point in other.contacts)
            {
                if (point.point.x - transform.position.x > 0.1)
                {
                    ColorWallRight = true;
                }
                else if (point.point.x - transform.position.x < -0.1)
                {
                    ColorWallLeft = true;
                }
                else if(point.point.y - transform.position.y < -0.1)
                {
                    ColorWallBottom = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //OnDamegeEffect();

        ////ゴールと接触したら
        //if (other.tag == "Goal")
        //{
        //    Invoke("Next", 0f);
        //}

        if (other.gameObject.tag == "ScoreLine")
        {
            scoreline++;
        }

        if (other.tag == "item")
        {
            ItemJump o = other.gameObject.GetComponent<ItemJump>();
            if (o != null)
            {
                audioSource.PlayOneShot(ItemSE);
                IJumpH = o.boundHeight;    //踏んづけたものから跳ねる高さを取得する
                IJumpC += o.boundCount;
                IJump = true;
                o.playerjump = true;        //踏んづけたものに対して踏んづけた事を通知する
                jumpText.text = string.Format("ジャンプ残り {0} 回", IJumpC);
            }
            else
            {
                Debug.Log("ObjectCollisionが付いてないよ!");
            }

        }

        if (other.gameObject.tag == "DeathGround")
        {
            Camera.main.gameObject.GetComponent<CameraScritpt>().Shake();
            Instantiate(playerDeathObj, transform.position, Quaternion.identity);

            //プレイヤー死亡
            isDeadFlag = true;
        }
        if (other.gameObject.tag == "ColorBlock")
        {
            hip = false;
            stop = false;
        }

    }

    #region　ダメージの時に点滅させる処理(使用中)
    /// <summary>
    /// ダメージを受けた時に呼び出す処理
    /// </summary>
    void OnDamegeEffect()
    {
        //damageFlag = true;
        on_ground = true;
        StartCoroutine(WaitForit());//WaitForitで指定した秒分待ってから呼び出す
    }


    IEnumerator WaitForit()
    {
        yield return new WaitForSeconds(1);//1秒間処理を止める

        //ダメージを受けてないと判断し、点滅をやめる
        //damageFlag = false;
        on_ground = false;
        MainSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);

    }
    #endregion

    /// <summary>
    /// シーン変更時にフェードをかける処理
    /// </summary>
    void Next()
    {
        FadeManager.FadeOut(1);
    }
    private float GetYSpeed()
    {

        float ySpeed = gravity.y;
        //何かを踏んだ際のジャンプ
        if (isOtherJump)
        {
            Parasol = hinan;
            if (jumpPos + otherJumpHeight > transform.position.y && jumpTime < jumpLimitTime && !isHead && !hip &&!Rcv)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
                xSpeed = 1;
            }       
            else if(jumpPos + otherJumpHeight > transform.position.y && jumpTime < jumpLimitTime && !isHead && hip && !Rcv)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
                //xSpeed = HipJump;
                hiptime = true;
                hip = false;
            }
            else  if (jumpPos + otherJumpHeight > transform.position.y && jumpTime < jumpLimitTime2 && !isHead && Rcv)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
                xSpeed = 1;
               // Debug.Log("ksk");
            }
            else
            {
                isOtherJump = false;
                hiptime = false;
                Rcv = false;
                jumpTime = 0.0f;
            }

        }
        else if (hip)
        {
            isOtherJump = false;
            jumpTime = 0.0f;
        }
        return ySpeed;
    }

    public int GetColor()
    {
        int x;
        x = (int)CS;
        return x;
    }
    public void Hip()
    {
        Parasol = 0;
        hiptime = false;
        stop = false;
    }
}
