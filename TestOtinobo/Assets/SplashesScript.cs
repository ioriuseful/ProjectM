using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashesScript : MonoBehaviour
{
    [SerializeField, Header("水しぶきの角度")] float z = 60.0f;
    [SerializeField, Header("水しぶきが消えるまでの時間")] float time = 1.0f;
    [SerializeField, Header("水しぶきの移動速度(Y)")] float Yspeed = 0.01f;
    [SerializeField, Header("水しぶきの移動速度(X)")] float Xspeed = 0.03f;
    private string name;

    void Start()
    {
        name = transform.name;
        
        switch(name)
        {
            case "LSplashes":
                transform.Rotate(0.0f, 0.0f, z);
                break;

            case "RSplashes":
                transform.Rotate(0.0f, 0.0f, -z);
                break;

            default:
                Debug.Log("名前が違います" + transform.name);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform MyTransform = this.transform;

        Vector3 pos = MyTransform.position;

        switch(name)
        {
            case "LSplashes":
                pos.x -= Xspeed;
                pos.y += Yspeed;
                Invoke("IsDead", time);
                break;

            case "RSplashes":
                pos.x += Xspeed;
                pos.y += Yspeed;
                Invoke("IsDead", time);
                break;

            default:
                Debug.Log("名前が違います" + transform.name);
                break;
        }
        MyTransform.position = pos;
    }

    void IsDead()
    {
        Destroy(gameObject);
    }
}
