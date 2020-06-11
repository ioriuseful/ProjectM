using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainMove : MonoBehaviour
{
    [SerializeField, Header("プレイヤーのx軸を取得するため")] GameObject player;
    [SerializeField]
    private float seconds;
    public float speed = 0.3f;//速さ
    public RetryGame retry;
    [Header("加速量")] public float acceleration = 10;//加速量
    public bool flag1 = false;//距離が遠くなった時のフラグ
    public bool flag2 = true;//距離が近くなった時のフラグ
    public WaveScript nowWave;
    //public float MaxDistance;//遠い距離
    //public float MinDistance;//近い距離
    public bool setpause;
    private float addspeed;
    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        addspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        setpause = retry.pauseGame;
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
        //if (d > MaxDistance)
        //{
        //    flag1 = true;
        //    flag2 = false;
        //}
        //if (d < MinDistance)
        //{
        //    flag1 = false;
        //    flag2 = true;
        //}
        if (flag1 == true)
        {
            pos.x = c + speed * acceleration * 0.5f;
        }
        else if (flag2 == true)
        {
            pos.x = c + speed * nowWave.wave / 16;
        }
        else
        {
            pos.x = c + speed * nowWave.wave;
        }
        if (setpause == true)
        {
            speed = 0;
        }
        else
        {
            speed = addspeed;
        }
        myTransform.position = pos;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MainCamera")
        {
            flag1 = false;
            flag2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "MainCamera")
        {
            flag1 = true;
            flag2 = false;
        }
    }
}
