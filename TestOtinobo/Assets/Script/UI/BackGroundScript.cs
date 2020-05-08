using System.Collections;
using UnityEngine;

public class BackGroundScript : MonoBehaviour
{

    [Header("スクロールの速度")] public float speed = -0.1f;
    [Header("画像のY.position")] public float posY = 4.48f;
    [Header("画像のX.position")] public float posX = 13.8f;

    void Update()
    {
        transform.Translate(speed, 0, 0);
        if(transform.position.x < -posX)
        {
            transform.position = new Vector3(posX, posY, 0);
        }
    }
}
