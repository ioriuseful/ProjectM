using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathScript : MonoBehaviour
{
    [SerializeField, Header("フェードして消えるまでの時間")] float fadeTime = 2f;
    [SerializeField, Header("パーティクルの移動速度")] float posspeed = 0.03f;
    [SerializeField, Header("パーティクルの大きさの変化速度")] float sizespeed = 0.08f;
    [SerializeField, Header("パーティクルが大きくなる最大値")] float max = 2;
    [SerializeField, Header("パーティクルが小さくなる最小値")] float min = 1;
    [SerializeField, Header("パーティクルが何回拡大縮小を繰り返すかの値")] int count = 2;
    [Header("プレイヤーデスオブジェならチェック")] public bool isPlayerflag;
    
    private string name;
    private Object parent;
    private bool sizechange = false;
    private float timelimit;
    private SpriteRenderer sprite;

    void Start()
    {
        name = transform.name;
        parent = transform.root.gameObject;
        timelimit = fadeTime;
        sprite = GetComponent<SpriteRenderer>();

        #region 各オブジェクトの回転割り当て
        switch (name)
        {
            case "Up":
                break;

            case "Up2":
                break;

            case "Right":
                transform.Rotate(0, 0, 90);
                break;

            case "Right2":
                transform.Rotate(0, 0, 90);
                break;

            case "Down":
                transform.Rotate(0, 0, 180);
                break;

            case "Down2":
                transform.Rotate(0, 0, 180);
                break;

            case "Left":
                transform.Rotate(0, 0, 270);
                break;

            case "Left2":
                transform.Rotate(0, 0, 270);
                break;

            case "UpRight":
                transform.Rotate(0, 0, 45);
                break;

            case "RightDown":
                transform.Rotate(0, 0, 135);
                break;
                
            case "DownLeft":
                transform.Rotate(0, 0, 225);
                break;

            case "LeftUp":
                transform.Rotate(0, 0, 315);
                break;

            default:
                Debug.Log("名前が違います" + transform.name);
                break;
        }
        #endregion
    }

    void Update()
    {
        Transform MyTransform = transform;

        Vector3 pos = MyTransform.position;

        timelimit -= Time.deltaTime;
        if(timelimit <= 0f && !isPlayerflag)
        {
            Debug.Log("通ってる");
            IsDead();
        }

        float alpha = timelimit / fadeTime;
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
        
        #region 各オブジェクトの挙動
        switch (name)
        {
            case "Up":
                pos.y += posspeed;
                Animation();
                break;

            case "Up2":
                pos.y += posspeed / 3;
                Animation();
                break;

            case "Right":
                pos.x += posspeed;
                Animation();
                break;

            case "Right2":
                pos.x += posspeed / 3;
                Animation();
                break;

            case "Down":
                pos.y -= posspeed;
                Animation();
                break;

            case "Down2":
                pos.y -= posspeed / 3;
                Animation();
                break;

            case "Left":
                pos.x -= posspeed;
                Animation();
                break;

            case "Left2":
                pos.x -= posspeed / 3;
                Animation();
                break;

            case "UpRight":
                pos.x += posspeed / 2;
                pos.y += posspeed / 2;
                Animation();
                break;

            case "RightDown":
                pos.x += posspeed / 2;
                pos.y -= posspeed / 2;
                Animation();
                break;

            case "DownLeft":
                pos.x -= posspeed / 2;
                pos.y -= posspeed / 2;
                Animation();
                break;

            case "LeftUp":
                pos.x -= posspeed / 2;
                pos.y += posspeed / 2;
                Animation();
                break;

            default:
                Debug.Log("名前が違います" + transform.name);
                break;
        }
        #endregion

        MyTransform.position = pos;
    }

    void Animation()
    {
        Transform MyTransform = transform;
        
        Vector3 size = MyTransform.localScale;
        
        if (size.x <= max && count > 0 && sizechange == false)
        {
            size.x += sizespeed;
            size.y += sizespeed;
        }
        else
        {
            if (sizechange == false)
            {
                sizechange = true;
            }
        }

        if (size.x >= min && count > 0 && sizechange == true)
        {
            size.x -= sizespeed;
            size.y -= sizespeed;
        }
        else
        {
            if (sizechange == true)
            {
                sizechange = false;
                count--;
            }
        }
        
        MyTransform.localScale = size;
    }

    void IsDead()
    {
        Destroy(parent);
    }
}
