using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerShadow : MonoBehaviour
{
    [SerializeField, Header("軌跡を描くようオブジェクト")] Object Shadowplayer;
    [SerializeField, Header("軌跡を描くようオブジェクトRed")] Object ShadowplayerRed;
    [SerializeField, Header("軌跡を描くようオブジェクトGreen")] Object ShadowplayerGreen;
    [SerializeField, Header("軌跡を描くようオブジェクトBlue")] Object ShadowplayerBlue;
    [SerializeField, Header("軌跡を表示させる間隔")] float interval = 3;
    private TutorialPlayerScript TPscript;
    private string color;
    private float time = 0;
    private float Addtime = 0.01f;
    private float postime = 1;
    private Vector3 GeneratePos;

    void Start()
    {
        interval = interval / 100;
        TPscript = GameObject.Find("TutorilaPlayer").GetComponent<TutorialPlayerScript>();
    }

    void Update()
    {
        if (TPscript.shadowGenerator == true)
        {
            color = TPscript.Color;
            time += Addtime;
            postime += Addtime;
            GeneratePos = new Vector3(transform.position.x, transform.position.y, transform.position.x + Addtime);
            if (interval - time <= 0)
            {
                switch (color)
                {
                    case "white":
                        Instantiate(Shadowplayer, GeneratePos, Quaternion.identity);
                        break;

                    case "Red":
                        Instantiate(ShadowplayerRed, GeneratePos, Quaternion.identity);
                        break;

                    case "Green":
                        Instantiate(ShadowplayerGreen, GeneratePos, Quaternion.identity);
                        break;

                    case "Blue":
                        Instantiate(ShadowplayerBlue, GeneratePos, Quaternion.identity);
                        break;
                }
                time = 0;
            }
        }
    }
}
