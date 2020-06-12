using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHitScript : MonoBehaviour
{
    [SerializeField, Header("パーティクルが消えるまでの時間")] float Time = 0.5f;
    [SerializeField, Header("パーティクルの移動速度")] float posspeed = 0.1f;
    [SerializeField, Header("パーティクルの移動速度の変化")] float posspeedchange = 0.08f;
    [SerializeField, Header("パーティクルの大きさの変化速度")] float sizespeed = 0.08f;
    
    private string name;
    private Object parent;

    void Start()
    {
        name = transform.name;
        parent = transform.root.gameObject;

        #region 各オブジェクトの回転割り当て
        switch (name)
        {
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
        
        #region 各オブジェクトの挙動
        switch (name)
        {
            case "UpRight":
                posspeed -= posspeedchange;
                if (posspeed >= 0)
                {
                    pos.x += posspeed;
                    pos.y += posspeed;
                }
                Animation();
                Invoke("IsDead", Time);
                break;

            case "RightDown":
                posspeed -= posspeedchange;
                if (posspeed >= 0)
                {
                    pos.x += posspeed;
                    pos.y -= posspeed;
                }
                Animation();
                Invoke("IsDead", Time);
                break;

            case "DownLeft":
                posspeed -= posspeedchange;
                if (posspeed >= 0)
                {
                    pos.x -= posspeed;
                    pos.y -= posspeed;
                }
                Animation();
                Invoke("IsDead", Time);
                break;

            case "LeftUp":
                posspeed -= posspeedchange;
                if (posspeed >= 0)
                {
                    pos.x -= posspeed;
                    pos.y += posspeed;
                }
                Animation();
                Invoke("IsDead", Time);
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

        size.x += sizespeed;
        size.y += sizespeed;
        MyTransform.localScale = size;
    }

    void IsDead()
    {
        Destroy(parent);
    }
}
