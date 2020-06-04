using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    [SerializeField, Header("軌跡を描くようオブジェクト")]Object Shadowplayer;
    [SerializeField, Header("軌跡を表示させる間隔")]float interval = 3;
    private PlayerScript Pscript;
    private float time = 0;
    private float Addtime = 0.01f;
    private float postime = 1;
    private Vector3 GeneratePos;

    void Start()
    {
        interval = interval / 100;
        Pscript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        if (Pscript.shadowGenerator == true)
        {
            time += Addtime;
            postime += Addtime;
            GeneratePos = new Vector3(transform.position.x, transform.position.y, transform.position.x + Addtime);
            if (interval - time <= 0)
            {
                Instantiate(Shadowplayer, GeneratePos, Quaternion.identity);
                time = 0;
            }
        }
    }
}
