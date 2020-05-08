using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;//移動スピード
    public float MinX,MaxX, MaxY,MinY;//移動制限
    public float move;//移動幅
    private bool isReachTargetPosition;
    public bool Xswitch = false;
    public bool Yswitch=false;
    public float randompoint,randompoint2;
    public int xcounter;
    public int ycounter;
    // Start is called before the first frame update
    void Start()
    {
        Transform transform = this.gameObject.GetComponent<Transform>();
        Vector2 pos = transform.position;
        pos.x = transform.position.x;
        pos.y = transform.position.y;
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
        Transform transform = this.gameObject.GetComponent<Transform>();
        Vector2 pos = transform.position;
        if (Xswitch == true)
        {
            pos.x += speed * move;
            //xcounter++;
        }
        if (Xswitch == false)
        {
            pos.x -= speed * move;
            //xcounter--;
        }
        if(xcounter==120)
        {
            Xswitch = false;
        }
        if(xcounter==-120)
        {
            Xswitch = true;
        }
        if (Yswitch == true)
        {
            if (Probability(randompoint))
            {
                pos.y += speed * move;
                Yswitch = false;
                ycounter++;
            }
        }
        if(Yswitch==false)
        {
            if(Probability(randompoint2))
            {
                pos.y -= speed * move;
                ycounter--;
            }
        }
        if(ycounter==120)
        {
            Yswitch = false;
        }
        if(ycounter==-120)
        {
            Yswitch = true;
        }
        transform.position = pos;
    }
}
