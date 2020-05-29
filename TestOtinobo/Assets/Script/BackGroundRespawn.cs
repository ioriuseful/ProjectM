using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRespawn : MonoBehaviour
{
    [SerializeField, Header("BackGroundオブジェクト")] Object BackGround;
    [SerializeField, Header("BackGroundの間隔設定")] int interval = 24;
    [SerializeField, Header("生成位置をどれだけ放すか")] float x = 26;
    [SerializeField, Header("BackGroundのy座標")] float y = 6;
    [SerializeField, Header("RedStarオブジェクト")] Object RedStar;
    [SerializeField, Header("GreenStarオブジェクト")] Object GreenStar;
    [SerializeField, Header("BlueStarオブジェクト")] Object BlueStar;
    [SerializeField, Header("WhiteStarオブジェクト")] Object WhiteStar;
    [SerializeField, Header("Starの生成数")] int count = 3;
    private GameObject player;
    private int metor = -1;
    private int Px = 0;//プレイヤーのxポジション取得用
    private List<int> poslist = new List<int>();//リスポーン済みリスト
    private float randomx;
    private float randomy;
    private int randomstar;
    private List<Object> starlist = new List<Object>();

    void Start()
    {
        player = GameObject.Find("Player");
        poslist.Clear();
        transform.position = new Vector3(transform.position.x, y, 0);
        Instantiate(BackGround, transform.position, Quaternion.identity);
        starlist.Add(RedStar);
        starlist.Add(GreenStar);
        starlist.Add(BlueStar);
        starlist.Add(WhiteStar);
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
                for(int i = 0; i < count; i++)
                {
                    randomx = Random.Range(0.0f, 24.0f);
                    randomy = Random.Range(0.0f, 6.0f);
                    randomstar = Random.Range(0, 3);
                    transform.position = new Vector3(player.transform.position.x + x + randomx, y + randomy, 0);
                    Instantiate(starlist[randomstar], transform.position, Quaternion.identity);
                }
            }
        }
    }
}
