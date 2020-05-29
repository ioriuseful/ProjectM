using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    [SerializeField, Header("星の輝くループ間隔")] float time = 0.3f;
    [SerializeField, Header("星の輝く大きさの変化速度(変化が速すぎるとマイナスになりタイミングがずれる)")] float sizespeed = 0.1f;
    [SerializeField, Header("それぞれの星の光るタイミングずらす用")]
    public float maxtime = 10.0f;
    public float mintime = 0.0f;
    [SerializeField, Header("また光出すまで用タイマー")]
    public float retime = 5.0f;
    
    private float random;
    private float timelimit;
    private float timelimit2;
    private SpriteRenderer sprite;
    private bool change = true;
    private float timer = 0.01f;

    [SerializeField, Header("消えるまでの距離")] int count = 20;

    private PlayerScript Pscript;//プレイヤースクリプト取得
    private GameObject player;
    private int score;//生成時のスコア
    private int scorenow;//現在のスコア

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        random = Random.Range(mintime, maxtime);
        timelimit2 = retime;

        player = GameObject.Find("Player");
        Pscript = player.GetComponent<PlayerScript>();
        score = Pscript.scoreline;
        score += count;
    }

    void Update()
    {
        Invoke("Animation", random);
        scorenow = Pscript.scoreline;
        if (score == scorenow)
        {
            Destroy(gameObject);
        }
    }

    void Animation()
    {
        Transform MyTransform = transform;

        Vector3 size = MyTransform.localScale;

        if (timelimit < time && change == true)
        {
            size.x += sizespeed;
            size.y += sizespeed;
            timelimit += timer;
        }
        else
        {
            if (change == true)
            {
                timelimit2 = retime;
                change = false;
            }
        }
        if (timelimit > 0f && change == false)
        {
            size.x -= sizespeed;
            size.y -= sizespeed;
            timelimit -= timer;
        }
        else
        {
            if (change == false)
            {
                timelimit2 -= timer;
                if (timelimit2 < 0)
                {
                    change = true;
                }
            }
        }
        float alpha = timelimit / time;
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
        Debug.Log(timelimit);
        MyTransform.localScale = size;
    }
}
