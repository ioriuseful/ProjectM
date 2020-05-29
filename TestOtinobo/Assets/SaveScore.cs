using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    public Text HighScoreText; //ハイスコアを表示するText    
    public Text SecondscoreText;
    public Text ThirdscoreText;
    public Text ForthscoreText;
    public Text FifthscoreText;
    public Text NowScoreText;
    public PlayerScript saveItem;
    public ScoreLineCount savePoint;
    public ScoreCount savePlayer;
 
    private int highScore,secondScore,thirdScore,forthScore,fifthScore,nowScore; //ハイスコア用変数
    private string key = "HIGH SCORE"; //ハイスコアの保存先キー
    private string key2 = "SECOND SCORE"; //2位
    private string key3 = "THIRD SCORE";//3位
    private string key4 = "FORTH SCORE";//4位
    private string key5 = "FIFTH SCORE";//5位
    private string key6 = "NOW SCORE";
    private int Ipoint, Spoint, Ppoint, Apoint,before;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt(key, 0);
        secondScore = PlayerPrefs.GetInt(key2, 1);
        thirdScore = PlayerPrefs.GetInt(key3, 2);
        forthScore = PlayerPrefs.GetInt(key4, 3);
        fifthScore = PlayerPrefs.GetInt(key5, 4);
        nowScore = PlayerPrefs.GetInt(key6, 5);
        Ipoint = saveItem.IJumpC;
        Spoint = savePoint.score;
        Ppoint = savePlayer.scorepoint;
        Apoint = Ipoint +Spoint + Ppoint;
        HighScoreText.text = "HighScore: " + highScore.ToString();
        SecondscoreText.text = "SecondScore: " + secondScore.ToString();
        ThirdscoreText.text = "ThirdScore:" + thirdScore.ToString();
        ForthscoreText.text = "ForthScore:" + forthScore.ToString();
        FifthscoreText.text = "FifthScore:" + fifthScore.ToString();
        NowScoreText.text = "NowScore:" + nowScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Ipoint = saveItem.IJumpC*300;
        Spoint = savePoint.score*100;
        Ppoint = savePlayer.scorepoint*10;
        Apoint = Ipoint + Spoint + Ppoint;
        nowScore = Apoint;
        NowScoreText.text = "NowScore:" + nowScore.ToString();
        Debug.Log(nowScore);
        if(forthScore>Apoint&&Apoint>fifthScore)
        {
            fifthScore = Apoint;
            PlayerPrefs.SetInt(key5, fifthScore);
            FifthscoreText.text = "FifthScore:" + fifthScore.ToString();
        }
        if(thirdScore>Apoint&&Apoint >forthScore)
        {
            forthScore = Apoint;
            PlayerPrefs.SetInt(key4, forthScore);
            ForthscoreText.text = "ForthScore:" + forthScore.ToString();
        }
        if (secondScore > Apoint && Apoint > thirdScore)
        {
            thirdScore = Apoint;
            PlayerPrefs.SetInt(key3, thirdScore);
            ThirdscoreText.text = "ThirdScore:" + thirdScore.ToString();
        }
        if(highScore>Apoint&&Apoint>secondScore)
        {
            secondScore = Apoint;
            PlayerPrefs.SetInt(key2, secondScore);
            SecondscoreText.text = "SecondScore: " + secondScore.ToString();
        }
        //ハイスコアより現在スコアが高い時
        if (Apoint > highScore)
        {

            highScore = Apoint;
            //ハイスコア更新

            PlayerPrefs.SetInt(key, highScore);
            //ハイスコアを保存

            HighScoreText.text = "HighScore: " + highScore.ToString();
            //ハイスコアを表示
        }
    }
    // UIボタンクリック時の処理
    public void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
