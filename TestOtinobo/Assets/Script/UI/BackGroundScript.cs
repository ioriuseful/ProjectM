using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScript : MonoBehaviour
{
    [SerializeField, Header("消えるまでの距離")] int count = 20;

    private PlayerScript Pscript;//プレイヤースクリプト取得
    private GameObject player;
    private int score;//生成時のスコア
    private int scorenow;//現在のスコア

    private void Start()
    {
        player = GameObject.Find("Player");
        Pscript = player.GetComponent<PlayerScript>();
        score = Pscript.scoreline;
        score += count;
    }

    private void Update()
    {
        scorenow = Pscript.scoreline;
        if (score == scorenow)
        {
            Destroy(gameObject);
        }
    }
}
