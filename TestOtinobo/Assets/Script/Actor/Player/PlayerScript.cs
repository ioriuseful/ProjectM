using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public enum PlayerState
    {
        Down, Up, Stand,
    }
    [Header("雲状態を解除した時に生成するパーティクルオブジェクト")] public GameObject CloudToNormalObj;
    public enum PlayerDate
    {
        Normal, Cloud,
    }
    public ColorState CS;//色を追加する場合エネミージャンプにも同様に色を増やすこと
    public PlayerState state;
    public PlayerDate date;
    public string Color;
    #endregion
    public Text jumpText;

    //[SerializeField, Header("プレイヤーが死んだSE")] public AudioClip PlayerDeadSE;
    [SerializeField, Header("プレイヤーがアイテムをとったSE")] public AudioClip ItemSE;
    [SerializeField, Header("プレイヤーがジャンプしたSE")] public AudioClip JumpSE;
    [SerializeField, Header("敵が死んだSE")] public AudioClip EnemyDeadSE;
    [SerializeField, Header("ヒップドロップ発動時のSE")] public AudioClip HipDropSE;
    [SerializeField, Header("ヒップドロップ中の軌跡の表示時間")] public float shadowtime = 0.6f;
    [SerializeField, Header("ヒップドロップ中の制限回数")] public int limit = 5;
    public int SpeedDown = 0;
    AudioSource audioSource;
    private Animator _animator;
    public RetryGame retryGame;
    //プレイヤーの画像変更に必要なもの
    SpriteRenderer MainSpriteRenderer;
    #region//画像の格納
    [SerializeField, Header("Spriteの格納")]
    public Sprite White;
    public Sprite Green;
    public Sprite Red;
    public Sprite Blue;
    public Sprite DWhite;
    public Sprite DGreen;
    public Sprite DRed;
    public Sprite DBlue;
    public Sprite UWhite;
    public Sprite UGreen;
    public Sprite URed;
    public Sprite UBlue;
    #endregion
    [SerializeField, Header("地面にぶつかったときのエフェクト")]
    public GameObject GreenEffect;
    public GameObject RedEffect;
    public GameObject BlueEffect;
    public GameObject ItemUp;
    private GameObject ItemEffect;

    //取得した蒸気の数
    public float SteamPoint;

    public int score = 0;//スコアの追加(4/17)
    private int numScore = 0;//ジャンプのご褒美を与えるための500区切りのスコア
    public int AddPoint = 100;//普通のスコア加算
    public int HighPoint = 200;//スコア加算の高いポイント
    public int Iscore;

    public int scoreline = 0;

    private float hinan;
    private Rigidbody2D rig2D;
    private Vector2 gravity;
    private bool Rcv = false;

    private bool damageFlag; //ダメージを受けているか判定
    private bool isDeadFlag; //死亡フラグ
    public bool GetisDeadFlag = false;//死亡フラグを取得する
    private bool isJump = false;
    private float jumpPos = 0.0f;
    private bool isOtherJump = false;
    private float jumpTime;
    private float otherJumpHeight = 0.0f;
    private bool isDown = false;
    private BoxCollider2D capcol = null;
    private bool isGround = false;
    private bool isHead = false;

    private bool IJump = false;
    public int IJumpC = 0;
    private float IJumpH = 20;
    private float xSpeed = 1;

    private bool on_ground = false;
    private bool hiptime = false;
    private bool hip = false;
    private bool stop = false;
    private bool JumpCancel = false;
    public bool anim1 = false;//animetion
    public bool anim2 = false;//animation
    public bool ColorWallRight = false;
    public bool ColorWallLeft = false;
    public bool ColorWallBottom = false;
    public bool Pause = false;
    private bool ColorBStep = false;
    // public GameObject PowerUp;
    public bool PU = false;
    public int EnemyDown;
    [SerializeField, Header("軌跡の表示用")]
    public bool shadowGenerator = false;
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
        jumpText.text = string.Format("× " + IJumpC);
        CS = ColorState.White;
        state = PlayerState.Stand;
        date = PlayerDate.Normal;
        EnemyDown = 0;
        SpeedDown = 0;
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
        Pause = retryGame.pauseGame;
        float ySpeed = GetYSpeed();
        //プレイヤーが動いていたらaxisの値に5かけて動かす
        if (axis > 0 && !hiptime && !hip && ColorWallRight == false && ColorWallLeft == true)
        {
            velocity.x = axis * (5 - SpeedDown);
        }
        if (axis < 0 && !hiptime && !hip && ColorWallRight == true && ColorWallLeft == false)
        {
            velocity.x = axis * (5 - SpeedDown);
        }
        if (axis != 0 && !hiptime && !hip && ColorWallRight == false && ColorWallLeft == true && ColorWallBottom == true)
        {
            velocity.x = axis * (5 - SpeedDown);
        }
        if (axis != 0 && !hiptime && !hip && ColorWallRight == true && ColorWallLeft == false && ColorWallBottom == true)
        {
            velocity.x = axis * (5 - SpeedDown);
        }
        if (axis != 0 && !hiptime && !hip && ColorWallRight == true && ColorWallLeft == true)
        {
            velocity.x = axis * (5 - SpeedDown);
        }
        if (axis != 0 && !hiptime && !hip && ColorWallRight == false && ColorWallLeft == false)
        {
            velocity.x = axis * (5 - SpeedDown);
        }

        else if (axis != 0 && hiptime && !hip)
        {
            velocity.x = axis * (5 - SpeedDown) * HipJump;
            ShadowOn();
            Invoke("ShadowOff", shadowtime);
        }
        if (!stop)
        {
            rig2D.velocity = new Vector2(velocity.x * xSpeed, ySpeed);
            //  Debug.Log("動けるよ");
        }
        else
        {
            rig2D.velocity = new Vector2(0, 0);
            xSpeed = 0;
            //  Debug.Log("動けないよ");
        }
        if (Pause == false)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                anim1 = true; //animのtrue追加
                audioSource.PlayOneShot(HipDropSE);
                hip = true;
                stop = true;
                xSpeed = 1;
                SpeedDown = 0;
                if(date == PlayerDate.Cloud)//雲解除時の演出用objを生成
                {
                    Instantiate(CloudToNormalObj, transform.position, Quaternion.identity);
                }
                date = PlayerDate.Normal;
                Invoke("Hip", HipLimitTime);
            }
        }
        if (on_ground)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            MainSpriteRenderer.color = new Color(1f, 1f, 1f, level);
        }

        if (ColorBStep)
        {
            otherJumpHeight = 0.1f; ;    //踏んづけたものから跳ねる高さを取得する          
            jumpPos = transform.position.y; //ジャンプした位置を記録する 
            isOtherJump = true;
            hip = false;
            isJump = false;
            jumpTime = 0.1f;
            //Parasol = hinan;
            hiptime = false;
            ColorBStep = false;
            state = PlayerState.Up;
        }//押し上げます。

        //ジャンプ(Spaceキー)が押されたらアイテムジャンプを使用する
        if (Pause == false)
        {
            if (IJump && Input.GetButtonDown("Jump") && JumpCancel == false)
            {
                if (IJumpC > 0)
                {
                    IJumpC--;
                    jumpText.text = string.Format("× " + IJumpC);
                    audioSource.PlayOneShot(JumpSE);
                    ShadowOff();
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
                state = PlayerState.Up;
                //Debug.Log("ジャンプしたよ");
            }
        }
        switch (CS)
        {
            case ColorState.White:
                //GetComponent<Renderer>().material.color = new Color32(WhiteR, WhiteG, WhiteB, WhiteA);
                switch (state)
                {
                    case PlayerState.Stand:
                        GetComponent<SpriteRenderer>().sprite = White;
                        Color = "white";
                        break;
                    case PlayerState.Up:
                        GetComponent<SpriteRenderer>().sprite = UWhite;
                        Color = "white";
                        break;
                    case PlayerState.Down:
                        GetComponent<SpriteRenderer>().sprite = DWhite;
                        Color = "white";
                        break;
                }
                break;
            case ColorState.Red:
                //GetComponent<Renderer>().material.color = new Color32(RedR, RedG, RedB, RedA);
                switch (state)
                {
                    case PlayerState.Stand:
                        GetComponent<SpriteRenderer>().sprite = Red;
                        Color = "Red";
                        break;
                    case PlayerState.Up:
                        GetComponent<SpriteRenderer>().sprite = URed;
                        Color = "Red";
                        break;
                    case PlayerState.Down:
                        GetComponent<SpriteRenderer>().sprite = DRed;
                        Color = "Red";
                        break;

                }
                break;
            case ColorState.Green:
                //GetComponent<Renderer>().material.color = new Color32(GreenR, GreenG, GreenB, GreenA);
                switch (state)
                {
                    case PlayerState.Stand:
                        GetComponent<SpriteRenderer>().sprite = Green;
                        Color = "Green";
                        break;
                    case PlayerState.Up:
                        GetComponent<SpriteRenderer>().sprite = UGreen;
                        Color = "Green";
                        break;
                    case PlayerState.Down:
                        GetComponent<SpriteRenderer>().sprite = DGreen;
                        Color = "Green";
                        break;
                }
                break;
            case ColorState.Blue:
                //GetComponent<Renderer>().material.color = new Color32(BlueR, BlueG, BlueB, BlueA);
                switch (state)
                {
                    case PlayerState.Stand:
                        GetComponent<SpriteRenderer>().sprite = Blue;
                        Color = "Blue";
                        break;
                    case PlayerState.Up:
                        GetComponent<SpriteRenderer>().sprite = UBlue;
                        Color = "Blue";
                        break;
                    case PlayerState.Down:
                        GetComponent<SpriteRenderer>().sprite = DBlue;
                        Color = "Blue";
                        break;
                }
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
                ShadowOff();

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
                state = PlayerState.Up;
                ShadowOff();
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
            ShadowOff();
        }
        //if (other.collider.tag == "ColorBlock") 
        //{ 
        //    if (hip) 
        //    {
        //        ColorBStep = true;
        //    }
        //}
        if (!stop && other.collider.tag == "Enemy" || !stop && other.collider.tag == "HighEnemy")
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
                        state = PlayerState.Up;
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
                        ShadowOff();
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
        if (!stop && other.collider.tag == "Kumo")
        {

            EnemyJump o = other.gameObject.GetComponent<EnemyJump>();
            if (o != null)
            {
                anim1 = true;//animのtrue
                audioSource.PlayOneShot(EnemyDeadSE);
                audioSource.PlayOneShot(JumpSE);
                otherJumpHeight = o.boundHeight;    //踏んづけたものから跳ねる高さを取得する
                o.playerjump = true;        //踏んづけたものに対して踏んづけた事を通知する

                date = PlayerDate.Cloud;
                jumpPos = transform.position.y; //ジャンプした位置を記録する 
                isOtherJump = true;
                isJump = false;
                jumpTime = 0.0f;
                state = PlayerState.Up;
                SpeedDown = o.ozyama;
                //Debug.Log("ジャンプしたよ");
                Camera.main.gameObject.GetComponent<CameraScritpt>().Shake();
                ShadowOff();
            }
            else
            {
                Debug.Log("ObjectCollisionが付いてないよ!");
            }
        }

        if (other.gameObject.tag == "ColorBlock")
        {
            ShadowOff();
        }

        //300点取る度にジャンプを一回増やす
        //if (numScore >= 300)
        //{
        //    IJumpC += 1;
        //    numScore = 0;
        //    IJump = true;
        //    jumpText.text = string.Format("× " + IJumpC);
        //}

        //if (other.gameObject.tag == "ColorBlock")
        //{
        //    foreach (ContactPoint2D point in other.contacts)
        //    {
        //        if (point.point.x - transform.position.x > 0.1)
        //        {
        //            ColorWallRight = true;
        //        }
        //        else if (point.point.x - transform.position.x < -0.1)
        //        {
        //            ColorWallLeft = true;
        //        }
        //        else if (point.point.y - transform.position.y < -0.1)
        //        {
        //            ColorWallBottom = true;
        //        }
        //    }
        //    //踏みつけ判定になる高さ
        //    float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));
        //    //踏みつけ判定のワールド座標
        //    float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;
        //    //Debug.Log("接触したよ");
        //    foreach (ContactPoint2D p in other.contacts)
        //    {
        //        if (p.point.y < judgePos)
        //        {
        //            if (stop)
        //            {
        //                PU = true;
        //            }
        //            if (!PU && hip)
        //            {
        //                ColorBStep = true;
        //                hip = false;
        //            }
        //        }
        //    }
        //}
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "ColorBlock")
        {
            ColorWallRight = false;
            ColorWallLeft = false;
            ColorWallBottom = false;
            if (PU)
            {
                PU = false;
            }
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
                else if (point.point.y - transform.position.y < -0.1)
                {
                    ColorWallBottom = true;
                }
            }
            //踏みつけ判定になる高さ
            float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));
            //踏みつけ判定のワールド座標
            float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;
            //Debug.Log("接触したよ");
            foreach (ContactPoint2D p in other.contacts)
            {
                if (p.point.y < judgePos)
                {
                    if (stop)
                    {
                        PU = true;
                    }
                    if (!PU && hip)
                    {
                        ColorBStep = true;
                        hip = false;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
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
                Iscore += 1;
                ItemEffect = (GameObject)Instantiate(ItemUp);
                ItemEffect.transform.SetParent(this.transform, false);
                IJumpH = o.boundHeight;    //踏んづけたものから跳ねる高さを取得する
                if (IJumpC >= limit)
                {
                    IJumpC = limit;
                }
                else
                {
                    IJumpC += o.boundCount;
                    Debug.Log("足してるお");
                }
                IJump = true;
                //GenerateEffect();
                o.playerjump = true;        //踏んづけたものに対して踏んづけた事を通知する
                jumpText.text = string.Format("× " + IJumpC);
                //Debug.Log(IJumpC);
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
            ShadowOff();

            //プレイヤー死亡
            isDeadFlag = true;
        }
        if (other.gameObject.tag == "ColorBlock")
        {
            hip = false;
            stop = false;
        }
        if (other.gameObject.tag == "Rain")
        {
            Camera.main.gameObject.GetComponent<CameraScritpt>().Shake();
            Instantiate(playerDeathObj, transform.position, Quaternion.identity);
            ShadowOff();
            isDeadFlag = true;
        }
        if (other.gameObject.tag == "Ceiling")
        {
            JumpCancel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ceiling")
        {
            JumpCancel = false;
        }
    }

    //private void GenerateEffect()
    //{
    //        GameObject effect = Instantiate(PowerUp) as GameObject;
    //        effect.transform.position = GameObject.Find("Player").transform.position;
    //}

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
            if (jumpPos + otherJumpHeight > transform.position.y && jumpTime < jumpLimitTime && !isHead && !hip && !Rcv)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
                xSpeed = 1;
            }
            else if (jumpPos + otherJumpHeight > transform.position.y && jumpTime < jumpLimitTime && !isHead && hip && !Rcv)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
                //xSpeed = HipJump;
                hiptime = true;
                hip = false;
            }
            else if (jumpPos + otherJumpHeight > transform.position.y && jumpTime < jumpLimitTime2 && !isHead && Rcv)
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
                state = PlayerState.Stand;
            }

        }
        else if (hip)
        {
            isOtherJump = false;
            jumpTime = 0.0f;
            state = PlayerState.Down;
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
        state = PlayerState.Down;
        hiptime = false;
        stop = false;
        ShadowOn();
    }

    public void ShadowOn()
    {
        shadowGenerator = true;
    }

    public void ShadowOff()
    {
        shadowGenerator = false;
    }
}
