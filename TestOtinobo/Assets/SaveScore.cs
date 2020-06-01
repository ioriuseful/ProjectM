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
 
    private int highScore,secondScore,thirdScore,forthScore,fifthScore,nowScore,result; //ハイスコア用変数
    private string key = "HIGH SCORE"; //ハイスコアの保存先キー
    private string key2 = "SECOND SCORE"; //2位
    private string key3 = "THIRD SCORE";//3位
    private string key4 = "FORTH SCORE";//4位
    private string key5 = "FIFTH SCORE";//5位
    private string key6 = "NOW SCORE";
    private string key7 = "Result";
    private int Ipoint, Spoint, Ppoint, Apoint;
    private bool PDead,Flash,Change,Set;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        PDead = false;
        highScore = PlayerPrefs.GetInt(key, 5);
        secondScore = PlayerPrefs.GetInt(key2, 4);
        thirdScore = PlayerPrefs.GetInt(key3, 3);
        forthScore = PlayerPrefs.GetInt(key4, 2);
        fifthScore = PlayerPrefs.GetInt(key5, 1);
        nowScore = PlayerPrefs.GetInt(key6, 100);
        result = PlayerPrefs.GetInt(key7, 0);
        Change = false;
        Set = true;
        Ipoint = saveItem.IJumpC;
        Spoint = savePoint.score;
        Ppoint = savePlayer.scorepoint;
        Apoint = Ipoint +Spoint + Ppoint;
        HighScoreText.text = "1位:" + highScore.ToString();
        SecondscoreText.text = "2位:" + secondScore.ToString();
        ThirdscoreText.text = "3位:" + thirdScore.ToString();
        ForthscoreText.text = "4位:" + forthScore.ToString();
        FifthscoreText.text = "5位:" + fifthScore.ToString();
        NowScoreText.text = "今回のスコア:" + nowScore.ToString();
        Result.text = "結果:" + result.ToString();
        OMEDETO.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        PDead = saveItem.GetisDeadFlag;
        Ipoint = saveItem.IJumpC * 300;
        Spoint = savePoint.score * 100;
        Ppoint = savePlayer.scorepoint * 10;
        Apoint = Ipoint + Spoint + Ppoint;
        nowScore = Apoint;
        PlayerPrefs.SetInt(key6, nowScore);
        NowScoreText.text = "今回の成績:" + nowScore.ToString();
        OnClick();
        if (PDead == true)
        {
            result = Apoint;
            PlayerPrefs.SetInt(key7, result);
            Result.text = "result:" + result.ToString();
            Debug.Log(result);
        }
        if (forthScore > Apoint && Apoint > fifthScore)
        {
            fifthScore = Apoint;
            PlayerPrefs.SetInt(key5, fifthScore);
            FifthscoreText.text = "5位:" + fifthScore.ToString();
            if (fifthScore == Apoint)
            {
                StartCoroutine("WaitForit");
                Change = true;
            }
        }
        if (thirdScore > Apoint && Apoint > forthScore)
        {
            forthScore = Apoint;
            PlayerPrefs.SetInt(key4, forthScore);
            ForthscoreText.text = "4位:" + forthScore.ToString();
            if (forthScore == Apoint)
            {
                StartCoroutine("WaitForit");
                Change = true;
            }
        }
        if (secondScore > Apoint && Apoint > thirdScore)
        {
            thirdScore = Apoint;
            PlayerPrefs.SetInt(key3, thirdScore);
            ThirdscoreText.text = "3位:" + thirdScore.ToString();
            if (thirdScore == Apoint)
            {
                StartCoroutine("WaitForit");
                Change = true;
            }
        }
        if (highScore > Apoint && Apoint > secondScore)
        {
            secondScore = Apoint;
            PlayerPrefs.SetInt(key2, secondScore);
            SecondscoreText.text = "2位:" + secondScore.ToString();
            if (secondScore == Apoint)
            {
                StartCoroutine("WaitForit");
                Change = true;
            }
        }
        //ハイスコアより現在スコアが高い時
        if (Apoint > highScore)
        {

            highScore = Apoint;
            //ハイスコア更新

            PlayerPrefs.SetInt(key, highScore);
            //ハイスコアを保存

            HighScoreText.text = "1位:" + highScore.ToString();
            //ハイスコアを表示
            if (highScore == Apoint)
            {
                StartCoroutine("WaitForit");
                Change = true;
            }
        }
        if (Flash)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            if (highScore == result)
            {
                HighScoreText.color = new Color(1f, 1f, 1f, level);
                Change = true;
            }
            if (secondScore == result)
            {
                SecondscoreText.color = new Color(1f, 1f, 1f, level);
                Change = true;
            }
            if (thirdScore == result)
            {
                ThirdscoreText.color = new Color(1f, 1f, 1f, level);
                Change = true;
            }
            if (forthScore == result)
            {
                ForthscoreText.color = new Color(1f, 1f, 1f, level);
                Change = true;
            }
            if (fifthScore == result)
            {
                FifthscoreText.color = new Color(1f, 1f, 1f, level);
                Change = true;
            }
        }
        if (Change==true&&Set==true)
        {
             audioSource.PlayOneShot(ChangeSE);
             OMEDETO.text = "おめでとう！";
             Set = false;
        } 
        if(Change==false&&Set==false)
        {
            audioSource.PlayOneShot(NomalSE);
            OMEDETO.text = "";
            Set = false;
        }
        OnDamegeEffect();
        WaitForit();
    }
    // UIボタンクリック時の処理
    public void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    void OnDamegeEffect()
    {
        Flash = true;
        //StartCoroutine(WaitForit());//WaitForitで指定した秒分待ってから呼び出す
    }


    IEnumerator WaitForit()
    {
        yield return new WaitForSeconds(1);//1秒間処理を止める
        //Flash = false;
        if (highScore == result)
        {
            HighScoreText.color = new Color(1f, 1f, 1f, 1f);
            Change = true;
        }
        if (secondScore == result)
        {
            SecondscoreText.color = new Color(1f, 1f, 1f, 1f);
            Change = true;
        }
        if (thirdScore == result)
        {
            ThirdscoreText.color = new Color(1f, 1f, 1f, 1f);
            Change = true;
        }
        if (forthScore == result)
        {
            ForthscoreText.color = new Color(1f, 1f, 1f, 1f);
            Change = true;
        }
        if (fifthScore == result)
        {
            FifthscoreText.color = new Color(1f, 1f, 1f, 1f);
            Change = true;
        }
    }
}
