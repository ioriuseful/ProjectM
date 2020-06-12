using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] Cloud;
    public int Generatetime;//何秒間隔で生成するか
    private bool isGenerateFlag;
    int rnd;
    public PlayerScript player;
    public PlayerScript.PlayerDate state;

    [Header("X座標をplayerからどの範囲までずらすか")]
    public float Xmin = -0.5f, Xmax = 0.5f;
    [Header("Y座標をplayerからどの範囲までずらすか")]
    public float Ymin = -0.5f, Ymax = 0.5f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var parent = transform;//演出雲を子供にするためのおまじない
        state = player.date;
        //プレイヤーが雲状態ならプレイヤーの周りに演出雲生成処理を開始する
        if (state == PlayerScript.PlayerDate.Cloud)
        {
            Generatetime--;
            if (Generatetime <= 0)
            {
                rnd = Random.Range(0, Cloud.Length);
                float xrnd = Random.Range(Xmin, Xmax);
                float yrnd = Random.Range(Ymin, Ymax);
                Instantiate(Cloud[rnd], new Vector2(transform.position.x + xrnd, transform.position.y + yrnd), Quaternion.identity, parent);
                Generatetime = 30;
            }


        }

    }
}
