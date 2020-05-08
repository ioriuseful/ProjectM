using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    [Header("最小と最大の横移動速度")]public float minSpeedX;
    public float maxSpeedX;

    [Header("最小と最大の縦移動速度")] public float minSpeedY;
    public float maxSpeedY;

    //最終的に横縦の値を入れるためのVec2
    private Vector2 axis;

    private Rigidbody2D rig2D;
    


    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        axis = new Vector2(Random.Range(minSpeedX, maxSpeedX), Random.Range(minSpeedY, maxSpeedY));
    }



    void Update()
    {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.0f, transform.rotation.w);
        Vector2 velocity = rig2D.velocity;
        velocity = axis;
        rig2D.velocity = velocity;
    }

}
