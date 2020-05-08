using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField, Header("敵が死んだSE")] public AudioClip EnemyDeadSE;

    [SerializeField, Header("敵を踏んだ時飛ぶ高さ")] public float boundHeight;
    [SerializeField, Header("エネミーが死んだときに出すパーティクルオブジェクト")] GameObject enemyDeathObj;
    //[SerializeField, Header("エネミーが死んだときに出すパーティクルオブジェクト")] GameObject enemyDeathObj2;
    //[SerializeField, Header("エネミーが死んだときに出すパーティクルオブジェクト")] GameObject enemyDeathObj3;

    public enum ColorState
    {
        White, Red, Blue, Green,
    }
    public ColorState CS;//色追加の時はこことプレイヤースクリプトの奴を増やすこと

    //敵を踏んだ時のフラグ
    public bool playerjump;
    private bool isDeadFlag;

    #region//RGBA旧設定
    //[Header("whiteのRGBA")] public byte WhiteR = 255;
    //public byte WhiteG = 255, WhiteB = 255, WhiteA = 255;//ホワイトの時のRGBA
    //[Header("greenのRGBA")] public byte GreenR = 113;
    //public byte GreenG = 250, GreenB = 120, GreenA = 255;//グリーン
    //[Header("redのRGBA")] public byte RedR = 255;
    //public byte RedG = 47, RedB = 20, RedA = 255;//レッド
    //[Header("blueのRGBA")] public byte BlueR = 87;
    //public byte BlueG = 117, BlueB = 255, BlueA = 255;//ブルー　
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    ///// 確率判定
    //public static bool Probability(float fPercent)
    //{
    //    float fProbabilityRate = UnityEngine.Random.value * 100.0f;

    //    if (fPercent == 100.0f && fProbabilityRate == fPercent)
    //    {
    //        return true;
    //    }
    //    else if (fProbabilityRate < fPercent)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        if (playerjump == true)
        {
            audioSource.PlayOneShot(EnemyDeadSE);
            isDeadFlag = true;
        }
        if (isDeadFlag == true)
        {
            Destroy(gameObject);
            Instantiate(enemyDeathObj, transform.position, Quaternion.identity);
        }
        //switch (CS)出力方法が変わったので
        //{
        //    case ColorState.White:
        //      //  GetComponent<Renderer>().material.color = new Color32(WhiteR, WhiteG, WhiteB, WhiteA);
        //        break;
        //    case ColorState.Red:
        //      //  GetComponent<Renderer>().material.color = new Color32(RedR, RedG, RedB, RedA);
        //        break;
        //    case ColorState.Green:
        //      //  GetComponent<Renderer>().material.color = new Color32(GreenR, GreenG, GreenB, GreenA);
        //        break;
        //    case ColorState.Blue:
        //       // GetComponent<Renderer>().material.color = new Color32(BlueR, BlueG, BlueB, BlueA);
        //        break;
        //}
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Rain")
        {
            isDeadFlag = true;
            //Debug.Log("rain");
        }
        if (other.gameObject.tag == "Ground")
        {
            isDeadFlag = true;
            //Debug.Log("a");
        }
    }

    public int GetColor()
    {
        int x;
        x = (int)CS;
        return x;
    }
}
