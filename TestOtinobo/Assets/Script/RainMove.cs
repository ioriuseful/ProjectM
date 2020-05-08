using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainMove : MonoBehaviour
{
    [SerializeField, Header("プレイヤーのx軸を取得するため")] GameObject player;
    [SerializeField]
    private float seconds;
    public float speed;//速さ
    [Header("加速量")] public float acceleration;//加速量
    public bool flag1 = false;//距離が遠くなった時のフラグ
    public bool flag2 = true;//距離が近くなった時のフラグ
    public float MaxDistance;//遠い距離
    public float MinDistance;//近い距離
    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // seconds += Time.deltaTime;
        Vector3 a;
        //プレイヤーのtransformを取得
        Transform playerTransform = player.transform;
        //プレイヤーの座標を取得
        Vector3 Ppos = playerTransform.position;
        // このオブジェクトのtransformを取得
        Transform myTransform = this.transform;
        // このオブジェクトの座標を取得
        Vector3 pos = myTransform.position;
        a = pos - Ppos;
        float b = Ppos.x;//プレイヤーのx座標
        float c = pos.x;//雨のx座標
        float d = b - c;//プレイヤーのxと雨のxを引いた値
        if (d > MaxDistance)
        {
            flag1 = true;
            flag2 = false;
        }
        if (d < MinDistance)
        {
            flag1 = false;
            flag2 = true;
        }
        if (flag1 == true)
        {
            pos.x = c + speed * acceleration * 0.1f;
        }
        else if (flag2 == true)
        {
            pos.x = c + speed * 0.1f;
        }
        else
        {
            pos.x = c + speed * 0.1f;
        }

        myTransform.position = pos;
    }
}
