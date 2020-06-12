using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    public GameObject first,second,third,forth,fifth;
    public AudioClip NomalSE;
    public AudioClip ChangeSE;
    private AudioSource audioSource;
    public Text HighScoreText; //ハイスコアを表示するText    
    public Text SecondscoreText;
    public Text ThirdscoreText;
    public Text ForthscoreText;
    public Text FifthscoreText;
    public Text NowScoreText;
    public Text OMEDETO;
    public Text Result;
    public PlayerScript saveItem;
    public ScoreLineCount savePoint;
    public ScoreCount savePlayer;
    private int FhighScore, FsecondScore , FthirdScore, FforthScore , FfifthScore ;//スタート時スコア
    private int highScore=1000, secondScore=800, thirdScore=600, forthScore=400, fifthScore=200, nowScore, result; //ハイスコア用変数
    [SerializeField, Header("アイテム拾った時のポイント")]
    public int ItemP=1000;
    [SerializeField, Header("ラインを通った時のポイント")]
    public int LineP=1000;
    [SerializeField, Header("敵を踏んだ時のポイント、300*EnemyPくらいになる")]
    public int EnemyP=10;
    private string key = "HIGH SCORE"; //ハイスコアの保存先キー
    private string key2 = "SECOND SCORE"; //2位
    private string key3 = "THIRD SCORE";//3位
    private string key4 = "FORTH SCORE";//4位
    private string key5 = "FIFTH SCORE";//5位
    private string key6 = "NOW SCORE";
    private string key7 = "Result";
    private int Ipoint, Spoint, Ppoint, Apoint, i = 0;
    private bool PDead, Flash, Change, Set, counter,scoreChange;
    private bool Clear;//表示が全部終わったよ
    private float timer;
    private bool Flag0,Flag1,Flag2,Flag3,Flag4;//同点処理に使用します
    private bool test =false;
    // Start is called before the first frame update
    void Start()
    {
        PDead = false;
        audioSource = gameObject.GetComponent<AudioSource>();
        highScore = PlayerPrefs.GetInt(key, highScore);
        FhighScore = PlayerPrefs.GetInt(key, highScore);
        secondScore = PlayerPrefs.GetInt(key2, secondScore);
        FsecondScore = PlayerPrefs.GetInt(key2, secondScore);
        thirdScore = PlayerPrefs.GetInt(key3, thirdScore);
        FthirdScore = PlayerPrefs.GetInt(key3, thirdScore);
        forthScore = PlayerPrefs.GetInt(key4, forthScore);
        FforthScore = PlayerPrefs.GetInt(key4, forthScore);
        fifthScore = PlayerPrefs.GetInt(key5, fifthScore);
        nowScore = PlayerPrefs.GetInt(key6, 100);
        result = PlayerPrefs.GetInt(key7, result);
        Change = false;
        Set = true;
        first.SetActive(false);
        second.SetActive(false);
        third.SetActive(false);
        forth.SetActive(false);
        fifth.SetActive(false);
        Ipoint = saveItem.Iscore;
        Spoint = savePoint.score;
        Ppoint = savePlayer.scorepoint;
        Apoint = Ipoint + Spoint + Ppoint;
        i = 0;
        Clear = false;
        scoreChange = false;
        Flag0 = false;Flag1 = false;Flag2 = false;Flag3 = false;Flag4 = false;
        test = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        PDead = saveItem.GetisDeadFlag;
        if (i <= 10)
        {
            timer += Time.deltaTime;
        }      
        Ipoint = saveItem.Iscore * ItemP;
        Spoint = savePoint.score * LineP;
        Ppoint = savePlayer.scorepoint * EnemyP;
        Apoint = Ipoint + Spoint + Ppoint;
        nowScore = Apoint;
        PlayerPrefs.SetInt(key6, nowScore);
        NowScoreText.text = "Sore:" + nowScore.ToString();
        #region//同点処理
        if (fifthScore == result)
        {
    
        }
        if (forthScore == nowScore&&FforthScore==nowScore)
        {
            fifthScore = forthScore;
            PlayerPrefs.SetInt(key5, fifthScore);
           
        }
        if (thirdScore == nowScore&&FthirdScore==nowScore)
        {
            fifthScore = FforthScore;
            PlayerPrefs.SetInt(key5, fifthScore);
            forthScore = thirdScore;
            PlayerPrefs.SetInt(key4, forthScore);
            
        }
        if (secondScore ==nowScore&&FsecondScore==nowScore)
        {
            fifthScore = FforthScore;
            PlayerPrefs.SetInt(key5, fifthScore);
            forthScore = FthirdScore;
            PlayerPrefs.SetInt(key4, forthScore);
            thirdScore = secondScore;
            PlayerPrefs.SetInt(key3, thirdScore);
            
        }
        if(highScore==nowScore&&FhighScore==nowScore)
        {
            fifthScore = FforthScore;
            PlayerPrefs.SetInt(key5, fifthScore);
            forthScore = FthirdScore;
            PlayerPrefs.SetInt(key4, forthScore);
            thirdScore = FsecondScore;
            PlayerPrefs.SetInt(key3, thirdScore);
            secondScore = highScore;
            PlayerPrefs.SetInt(key2, secondScore);
        }
        #endregion
        #region //点数更新処理
        if (PDead == true)
        {
            result = Apoint;
            PlayerPrefs.SetInt(key7, result);
            Result.text = result.ToString();
            //Debug.Log(result);
        }
        if (forthScore > result && result > fifthScore)
        {
            fifthScore = result;
            PlayerPrefs.SetInt(key5, fifthScore);
            FifthscoreText.text = fifthScore.ToString();
            if (fifthScore == Apoint)
            {
                Change = true;
            }
        }
        if (thirdScore > result && result > forthScore)
        {
            fifthScore = FforthScore;
            PlayerPrefs.SetInt(key5, fifthScore);
            forthScore = result;
            PlayerPrefs.SetInt(key4, forthScore);
            ForthscoreText.text =  forthScore.ToString();
            if (forthScore == result)
            {
                Change = true;
            }
        }
        if (secondScore >result && result > thirdScore)
        {
            fifthScore = FforthScore;
            PlayerPrefs.SetInt(key5, fifthScore);
            forthScore = FthirdScore;
            PlayerPrefs.SetInt(key4, forthScore);
            thirdScore = result;
            PlayerPrefs.SetInt(key3, thirdScore);
            ThirdscoreText.text =  thirdScore.ToString();
            if (thirdScore == result)
            {
                Change = true;
            }
        }
        if (highScore > result && result > secondScore)
        {
            fifthScore = FforthScore;
            PlayerPrefs.SetInt(key5, fifthScore);
            forthScore = FthirdScore;
            PlayerPrefs.SetInt(key4, forthScore);
            thirdScore = FsecondScore;
            PlayerPrefs.SetInt(key3, thirdScore);
            secondScore = result;
            PlayerPrefs.SetInt(key2, secondScore);
            SecondscoreText.text = secondScore.ToString();
            if (secondScore == result)
            {
                Change = true;
            }
        }
        //ハイスコアより現在スコアが高い時
        if (result > highScore)
        {
            fifthScore = FforthScore;
            PlayerPrefs.SetInt(key5, fifthScore);
            forthScore = FthirdScore;
            PlayerPrefs.SetInt(key4, forthScore);
            thirdScore = FsecondScore;
            PlayerPrefs.SetInt(key3, thirdScore);
            secondScore = FhighScore;
            PlayerPrefs.SetInt(key2, secondScore);
            highScore = result;
            //ハイスコア更新
            PlayerPrefs.SetInt(key, highScore);
            //ハイスコアを保存
            HighScoreText.text =  highScore.ToString();
            //ハイスコアを表示
            if (highScore == result)
            {
                Change = true;
            }
        }
        #endregion

        if (timer >= 0.5f)
        {
            counter = true;
            timer = 0.0f;
        }
        if (counter == true)
        {
            i++;
            counter = false;
            timer = 0.0f;
        }
        switch (i)
        {
            case 1:
                //ハイスコアを表示
                if (Flag0 == true)
                {
                    first.SetActive(true);
                    Flag1 = true;
                    HighScoreText.text =  highScore.ToString();
                    Change = true;
                }
                else if (highScore == result && Flag0 == false)
                {
                    first.SetActive(true);
                    Flag1 = true;
                    Flag2 = true;
                    Flag3 = true;
                    Flag4 = true;
                    HighScoreText.text =  highScore.ToString();//更新
                    Change = true;
                }
                else
                {
                    first.SetActive(true);
                    HighScoreText.text =  highScore.ToString();
                }
                break;
            case 2:
                //SecondscoreText.text = "2位:" + secondScore.ToString();
                if (secondScore == result && Flag1 == false)
                {
                    second.SetActive(true);
                    Flag2 = true;
                    Flag3 = true;
                    Flag4 = true;
                    Change = true;
                    SecondscoreText.text =  secondScore.ToString();//更新
                }
                else
                {
                    second.SetActive(true);
                    SecondscoreText.text =  secondScore.ToString();
                }

                break;
            case 3:
                if (thirdScore == result && Flag2 == false)
                {
                    third.SetActive(true);
                    Flag3 = true;
                    Flag4 = true;
                    Change = true;
                    ThirdscoreText.text =  thirdScore.ToString();//更新
                }
                else
                {
                    third.SetActive(true);
                    ThirdscoreText.text =  thirdScore.ToString();
                }
                break;
            case 4:
                if (forthScore == result && Flag3 == false)
                {
                    forth.SetActive(true);
                    Flag4 = true;
                    ForthscoreText.text =  forthScore.ToString();//更新
                    Change = true;
                }
                else
                {
                    forth.SetActive(true);
                    ForthscoreText.text =  forthScore.ToString();
                }
                break;
            case 5:
                if (fifthScore == result && Flag4 == false)
                {
                    fifth.SetActive(true);
                    FifthscoreText.text =  fifthScore.ToString();//更新
                    Change = true;
                }
                else
                {
                    fifth.SetActive(true);
                    FifthscoreText.text = fifthScore.ToString();
                }
                break;
            case 6:
                Clear = true;
                Result.text =  result.ToString();
                if (Change == true && Set == true && result > 0)
                {
                    audioSource.PlayOneShot(ChangeSE);
                    OMEDETO.text = "Congratulation！";
                    Set = false;
                }
                if (Change == false && Set == false)
                {
                    audioSource.PlayOneShot(NomalSE);
                    OMEDETO.text = "";
                    Set = false;
                }
                break;
        }
        if (Clear == true)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            if (highScore == result)
            {
                HighScoreText.color = new Color(1f, 1f, 1f, level);
            }
            if (secondScore == result&&Flag1==false)
            {
                SecondscoreText.color = new Color(1f, 1f, 1f, level);
            }
            if (thirdScore == result&&Flag2==false)
            {
                ThirdscoreText.color = new Color(1f, 1f, 1f, level);
            }
            if (forthScore == result&&Flag3==false)
            {
                ForthscoreText.color = new Color(1f, 1f, 1f, level);
            }
            if (fifthScore == result&&Flag4==false)
            {
                FifthscoreText.color = new Color(1f, 1f, 1f, level);
            }
        }
    }
}
