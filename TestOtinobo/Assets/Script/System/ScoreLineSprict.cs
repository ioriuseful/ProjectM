using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLineSprict : MonoBehaviour
{
    private bool ChangeEnd;
    private SpriteRenderer sprite;
    [SerializeField, Header("変更後の赤の濃度(0～1.0f)")] float Red = 1.0f;
    [SerializeField, Header("変更後の緑の濃度(0～1.0f)")] float Green = 1.0f;
    [SerializeField, Header("変更後の青の濃度(0～1.0f)")] float Blue = 1.0f;
    [SerializeField, Header("変更後の透明度(0～1.0f)")] float Alpha = 1.0f;
    [SerializeField, Header("Playerオブジェクト")] GameObject player;
    [SerializeField, Header("消えるまでの距離")] int count = 5;

    public PlayerScript Pscript;//プレイヤースクリプト取得
    private int score;//生成時のスコア
    private int scorenow;//現在のスコア

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        Pscript = player.GetComponent<PlayerScript>();
        score = Pscript.scoreline;
        score += count;
    }
    
    private void Update()
    {
        scorenow = Pscript.scoreline;
        if(score == scorenow)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            sprite.color = new Color(Red, Green, Blue, Alpha);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
