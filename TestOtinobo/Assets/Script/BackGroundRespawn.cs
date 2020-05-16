using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRespawn : MonoBehaviour
{
    [SerializeField, Header("BackGroundオブジェクト")] Object BackGround;
    [SerializeField, Header("BackGroundの間隔設定")] int interval = 24;
    [SerializeField, Header("生成位置をどれだけ放すか")] float x = 26;
    [SerializeField, Header("BackGroundのy座標")] float y = 6;
    private GameObject player;
    private int metor = -1;
    private int Px = 0;//プレイヤーのxポジション取得用
    private List<int> poslist = new List<int>();//リスポーン済みリスト

    void Start()
    {
        player = GameObject.Find("Player");
        poslist.Clear();
        transform.position = new Vector3(transform.position.x, y, 0);
        Instantiate(BackGround, transform.position, Quaternion.identity);
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + x, y, 0);
        Px = (int)player.transform.position.x;

        if (Px % interval == 0)
        {
            if (poslist.Contains(Px) == false)
            {
                poslist.Add(Px);
                Instantiate(BackGround, transform.position, Quaternion.identity);
            }
        }
    }
}
