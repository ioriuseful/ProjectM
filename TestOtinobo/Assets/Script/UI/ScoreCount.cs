using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    Text scoreText;//スライダー

    public GameObject player;//プレイヤーの蒸気の値を取得したい為
    public PlayerScript playerScript;//同上
    int scorepoint;//蒸気の値
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();//テキストを探す
    }

    // Update is called once per frame
    void Update()
    {
        scorepoint = playerScript.score;
        scoreText.text = scorepoint.ToString();//値によって変動させる
    }
}
