using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLineCount : MonoBehaviour
{
    public PlayerScript player;
    private int score;
    Text text;

    void Start()
    {
        text = GameObject.Find("ScoreLineText").GetComponent<Text>();//テキストを探す
        player = GameObject.Find("Player").GetComponent<PlayerScript>();//プレイヤースクリプトを探す
    }
    
    void Update()
    {
        score = player.scoreline;
        text.text = score + "本";
    }
}
