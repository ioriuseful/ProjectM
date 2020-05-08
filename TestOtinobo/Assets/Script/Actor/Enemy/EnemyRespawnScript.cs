using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnScript : MonoBehaviour
{

    [SerializeField, Header("EnemyBoxを入れるリスト")] List<GameObject> enemyBoxes;
    [SerializeField, Header("Player")] GameObject player;//Ｙ軸を参照するため

    [Header("再配置するまでの最小時間と最大時間")] public float minRespawnTime;
    public float maxRespawnTime;


    float NowxPosition,BeforexPosition; //生成した時点でのX座標と以前生成した時のX座標の取得
    [SerializeField,Header("前回生成した位置とどれだけ離れたら生成するか")]
    public float InstantiateDistance;

    //ランダム値を書くのするための値
    float rndRespawnTime;
    //int rnd;

    bool isRespawnFlag;

    //Wave処理
    public WaveScript Wscript;
    private int Enemywave;
    int rnd;//保管用
    [Header("Wave毎のリスポーン頻度の上昇、減少の設定")]
    public float MaxTimeUp = 0.5f;
    public float MaxTimeDown = 0;
    public float MinTimeUp = 0.5f;
    public float MinTimeDown = 0;
    private float MaxTime;
    private float MinTime;


    void Start()
    {
        rnd = Random.Range(0, 1);
        rndRespawnTime = Random.Range(minRespawnTime * 60, maxRespawnTime * 60);
        Instantiate(enemyBoxes[rnd], transform.position, Quaternion.identity);
        player = GameObject.Find("Player");
        Wscript = GameObject.Find("WaveText").GetComponent<WaveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemywave = Wscript.wave;

        transform.position = new Vector3(player.transform.position.x + 13f, transform.position.y, transform.position.z);
        NowxPosition = transform.position.x;
        //リスポーンして一定時間が経つと新しいEnemyBoxを生成するようにする
        //if (!isRespawnFlag)
        //{
        //    rndRespawnTime--;
        //    if (rndRespawnTime <= 0)
        //    {
        //        isRespawnFlag = true;
        //    }
        //}
        //else
        //{
        //    //時間が経過したらまた時間を再設定し、Boxをリスポーンさせる
        //    rnd = Random.Range(0, 3);
        //    rndRespawnTime = Random.Range(minRespawnTime * 60, maxRespawnTime * 60);
        //    Instantiate(enemyBoxes[rnd], transform.position, Quaternion.identity);
        //    isRespawnFlag = false;
        //}

        switch(Enemywave)
        {
            case 1:
                MaxTime = maxRespawnTime + MaxTimeUp - MaxTimeDown;
                MinTime = minRespawnTime + MinTimeUp - MinTimeDown;
                rnd = Random.Range(0, 2);
                Respawn();
                break;

            case 2:
                MaxTime = maxRespawnTime + MaxTimeUp * Enemywave - MaxTimeDown * Enemywave;
                MinTime = minRespawnTime + MinTimeUp * Enemywave - MinTimeDown * Enemywave;
                rnd = Random.Range(2, 4);
                Respawn();
                break;

            case 3:
                MaxTime = maxRespawnTime + MaxTimeUp * Enemywave - MaxTimeDown * Enemywave;
                MinTime = minRespawnTime + MinTimeUp * Enemywave - MinTimeDown * Enemywave;
                rnd = Random.Range(0, 4);
                Respawn();
                break;
            
            case 4:
                MaxTime = maxRespawnTime + MaxTimeUp * Enemywave - MaxTimeDown * Enemywave;
                MinTime = minRespawnTime + MinTimeUp * Enemywave - MinTimeDown * Enemywave;
                rnd = Random.Range(0, 2);
                Respawn();
                break;
            
            case 5:
                MaxTime = maxRespawnTime + MaxTimeUp * Enemywave - MaxTimeDown * Enemywave;
                MinTime = minRespawnTime + MinTimeUp * Enemywave - MinTimeDown * Enemywave;
                rnd = Random.Range(0, 3);
                Respawn();
                break;
                
            default:
                MaxTime = maxRespawnTime + MaxTimeUp * 6 - MaxTimeDown * 6;
                MinTime = minRespawnTime + MinTimeUp * 6  - MinTimeDown * 6;
                rnd = Random.Range(1, 3);
                Respawn();
                break;
        }
    }

    void Respawn()
    {
        //リスポーンして一定距離動くと新しいEnemyBoxを生成するようにする
        if (!isRespawnFlag)
        {
            //今の距離と一つ前に生成した距離を計算して指定以上離れていたらTrue
            if (NowxPosition - BeforexPosition >= InstantiateDistance)
            {
                isRespawnFlag = true;
            }
        }
        else
        {
            //時間が経過したらまた時間を再設定し、Boxをリスポーンさせる
            BeforexPosition = NowxPosition;
            Instantiate(enemyBoxes[rnd], transform.position, Quaternion.identity);
            isRespawnFlag = false;
        }
    }
}
