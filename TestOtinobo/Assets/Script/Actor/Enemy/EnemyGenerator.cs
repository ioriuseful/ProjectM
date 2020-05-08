using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public Transform Player;
    public float offset;
    public GameObject EnemyPrefab;
    public GameObject HighPointEnemy;//高いポイントの敵
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;
    //時間間隔最小値
    public float MinTime = 2f;
    //時間間隔最大値
    public float MaxTime = 5f;
    //X座標の最小値
    public float xMinPosition =-10f;
    //X座標の最大値
    public float xMaxPosition = 10f;
    //Y座標の最小値
    public float yMinPosition = -10f;
    //Y座標の最大値
    public float yMaxPosition = 10f;
    //通常の敵が出る確率
    public float generator = 100;

    // Start is called before the first frame update
    void Start()
    {
        //時間間隔を決定する
        interval = GetRandomTime();
    }
    /// 確率判定
    public static bool Probability(float fPercent)
    {
        float fProbabilityRate = UnityEngine.Random.value * 100.0f;

        if (fPercent == 100.0f && fProbabilityRate == fPercent)
        {
            return true;
        }
        else if (fProbabilityRate < fPercent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //時間計測
        time += Time.deltaTime;

        //経過時間が生成時間になった時(生成時間より大きくなった時)
        if(time > interval)
        {
            if (Probability(50))
            {
                //enemyのインスタンス化
                GameObject enemy = Instantiate(EnemyPrefab);
                //生成した敵の座標を決定する
                enemy.transform.position = GetRandomPosition();
                //経過時間を初期化して再度時間計測を始める
                time = 0f;
                //次に発生する時間間隔を決定する
                interval = GetRandomTime();
            }
            else
            {
                GameObject enemyhigh = Instantiate(HighPointEnemy);
                enemyhigh.transform.position = GetRandomPosition();
                //経過時間を初期化して再度時間計測を始める
                time = 0f;
                //次に発生する時間間隔を決定する
                interval = GetRandomTime();
            }
        }
    }
    //ランダムな時間を生成する関数
    private float GetRandomTime()
    {
        return Random.Range(MinTime, MaxTime);
    }
    //ランダムな位置を生成する関数
    private Vector2 GetRandomPosition()
    {
        Vector2 pos = transform.position;
        pos.y = Player.position.y;
        pos.x = Player.position.x;
        float x = Random.Range(pos.x+xMinPosition, pos.x+xMaxPosition);
        float y = Random.Range(pos.y+yMinPosition, pos.y+yMaxPosition);
        transform.position = pos;
        //vec2型のpositionを返す
        return new Vector2(x, y);
    }
}
