using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{

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
        Ipoint = saveItem.IJumpC;
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
        Ipoint = saveItem.IJumpC * 300;
        Spoint = savePoint.score * 100;
        Ppoint = savePlayer.scorepoint * 10;
        Apoint = Ipoint + Spoint + Ppoint;
        nowScore = Apoint;
        PlayerPrefs.SetInt(key6, nowScore);
        NowScoreText.text = "今回の成績:" + nowScore.ToString();
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
            Result.text = "result:" + result.ToString();
            Debug.Log(result);
        }
        if (forthScore > result && result > fifthScore)
        {
            fifthScore = result;
            PlayerPrefs.SetInt(key5, fifthScore);
            FifthscoreText.text = "5位:" + fifthScore.ToString();
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
            ForthscoreText.text = "4位:" + forthScore.ToString();
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
            ThirdscoreText.text = "3位:" + secondScore.ToString();
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
            SecondscoreText.text = "2位:" + secondScore.ToString();
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
            HighScoreText.text = "1位:" + highScore.ToString();
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
            case 0:
                //ハイスコアを表示
                if (Flag0 == true)
                {
                    Flag1 = true;
                    HighScoreText.text = "new 1位:" + highScore.ToString();
                    Change = true;
                }
                else if (highScore == result && Flag0 == false)
                {
                    Flag1 = true;
                    Flag2 = true;
                    Flag3 = true;
                    Flag4 = true;
                    HighScoreText.text = "new 1位:" + highScore.ToString();
                    Change = true;
                }
                else
                {
                    HighScoreText.text = "1位:" + highScore.ToString();
                }
                break;
            case 1:
                //SecondscoreText.text = "2位:" + secondScore.ToString();
                if (secondScore == result && Flag1 == false)
                {
                    Flag2 = true;
                    Flag3 = true;
                    Flag4 = true;
                    Change = true;
                    SecondscoreText.text = "new 2位:" + secondScore.ToString();
                }
                else
                {
                    SecondscoreText.text = "2位:" + secondScore.ToString();
                }

                break;
            case 2:
                if (thirdScore == result && Flag2 == false)
                {
                    Flag3 = true;
                    Flag4 = true;
                    Change = true;
                    ThirdscoreText.text = "new 3位:" + thirdScore.ToString();
                }
                else
                {
                    ThirdscoreText.text = "3位:" + thirdScore.ToString();
                }
                break;
            case 3:
                if (forthScore == result && Flag3 == false)
                {
                    Flag4 = true;
                    ForthscoreText.text = "new4位:" + forthScore.ToString();
                    Change = true;
                }
                else
                {
                    ForthscoreText.text = "4位:" + forthScore.ToString();
                }
                break;
            case 4:
                if (fifthScore == result && Flag4 == false)
                {
                    FifthscoreText.text = "new 5位:" + fifthScore.ToString();
                    Change = true;
                }
                else
                {
                    FifthscoreText.text = "5位:" + fifthScore.ToString();
                }
                break;
            case 5:
                Clear = true;
                Result.text = "今回の得点:" + result.ToString();
                if (Change == true && Set == true && result > 0)
                {
                    audioSource.PlayOneShot(ChangeSE);
                    OMEDETO.text = "Congratulation";
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
        OnDamegeEffect();
    }
    void OnDamegeEffect()
    {
        Flash = true;
        //StartCoroutine(WaitForit());//WaitForitで指定した秒分待ってから呼び出す
    }


    //IEnumerator WaitForit()
    //{
    //    yield return new WaitForSeconds(0);//1秒間処理を止める
    //    //Flash = false;
    //    //if (highScore == result)
    //    //{
    //    //    HighScoreText.color = new Color(1f, 1f, 1f, 1f);
    //    //}
    //    //if (secondScore == result)
    //    //{
    //    //    SecondscoreText.color = new Color(1f, 1f, 1f, 1f);
    //    //}
    //    //if (thirdScore == result)
    //    //{
    //    //    ThirdscoreText.color = new Color(1f, 1f, 1f, 1f);
    //    //}
    //    //if (forthScore == result)
    //    //{
    //    //    ForthscoreText.color = new Color(1f, 1f, 1f, 1f);
    //    //}
    //    //if (fifthScore == result)
    //    //{
    //    //    FifthscoreText.color = new Color(1f, 1f, 1f, 1f);
    //    //}
    //}
}
