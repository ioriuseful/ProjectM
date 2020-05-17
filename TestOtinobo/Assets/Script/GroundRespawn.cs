using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRespawn : MonoBehaviour
{
    [SerializeField, Header("groundBoxを入れるリスト")] List<GameObject> groundBoxes;
    [SerializeField, Header("Player")] GameObject player;

    //ランダム値を書くのするための値
    float rndRespawnTime;

    bool isRespawnFlag;

    //Wave処理
    public WaveScript Wscript;
    private int Enemywave;
    int rnd;//保管用

    [SerializeField, Header("地面の間隔設定")] int interval = 30;
    [SerializeField, Header("生成位置をどれだけ放すか")] float x = 20;
    [SerializeField, Header("Lineのy座標")] float y = 0;
    private int Px = 0;//プレイヤーのxポジション取得用
    List<int> poslist = new List<int>();//リスポーン済みリスト

    void Start()
    {
        rnd = Random.Range(0, 1);
        player = GameObject.Find("Player");
        Wscript = GameObject.Find("WaveText").GetComponent<WaveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemywave = Wscript.wave;

        transform.position = new Vector3(player.transform.position.x + x, y, 0);

        switch (Enemywave)
        {
            case 1:
                rnd = Random.Range(0, 1);
                Respawn();
                break;

            case 2:
                rnd = Random.Range(0, 2);
                Respawn();
                break;

            case 3:
                rnd = Random.Range(1, 2);
                Respawn();
                break;

            case 4:
                rnd = Random.Range(0, 2);
                Respawn();
                break;

            case 5:
                rnd = Random.Range(1, 2);
                Respawn();
                break;

            default:
                rnd = Random.Range(0, 2);
                Respawn();
                break;
        }
    }

    void Respawn()
    {
        transform.position = new Vector3(player.transform.position.x + x, y, 0);
        Px = (int)player.transform.position.x;

        if (Px % interval == 0)
        {
            if (poslist.Contains(Px) == false)
            {
                poslist.Add(Px);
                Instantiate(groundBoxes[rnd], transform.position, Quaternion.identity);
            }
        }
    }
}
