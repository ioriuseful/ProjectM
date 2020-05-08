using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLineRespawn : MonoBehaviour
{
    [SerializeField, Header("ScoreLineオブジェクト")] Object Line;
    [SerializeField, Header("スコアラインの間隔設定")] int interval = 10;
    [SerializeField, Header("Playerオブジェクト")] GameObject player;
    [SerializeField, Header("生成位置をどれだけ放すか")] float x = 20;
    [SerializeField, Header("Lineのy座標")] float y = 6;
    private int metor = -1;
    private int Px = 0;//プレイヤーのxポジション取得用
    List<int> poslist = new List<int>();//リスポーン済みリスト
    
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + x, y, 0);
        Px = (int)player.transform.position.x;
        
        if(Px % interval == 0)
        {
            if (poslist.Contains(Px) == false)
            {
                poslist.Add(Px);
                Instantiate(Line, transform.position, Quaternion.identity);
            }
        }
    }
}
