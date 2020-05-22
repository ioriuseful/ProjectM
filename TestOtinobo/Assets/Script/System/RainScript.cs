using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainScript : MonoBehaviour
{
    [SerializeField, Header("RainGenerateObjにコンポーネントした時のみチェック")]
    public bool isFallflag;
    [SerializeField, Header("何秒で雨を降らせるか")]
    public float fallRainTime;
    private float time;//保存用タイム
    private bool isfallRainflag;//雨が降るタイミング

    [Header("rainオブジェクトを直接入れる")]
    public GameObject RainObj;

    [SerializeField, Header("水しぶきオブジェクトを入れる")]
    public GameObject Splashes;
    public float y = -0.2f;

    private void Start()
    {
        time = fallRainTime * 60;
    }


    void Update()
    {
        //ジェネレータobjの時のみ
        if (isFallflag)
        {
            float rnd = Random.Range(-8, 5);
            
            //時間がゼロになったらtrue
            time--;
            if (time <= 0)
            {
                isfallRainflag = true;
            }

            //ジェネレータだったら雨を降らす
            if (isfallRainflag)
            {
                Instantiate(RainObj, new Vector3(transform.position.x + rnd, transform.position.y, 100), Quaternion.identity);
                isfallRainflag = false;
                time = fallRainTime * 60;
            }
        }
        if(transform.position.y <= -1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFallflag && (other.gameObject.tag == "Ground" || other.gameObject.tag == "Untagged"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + y, 0);
            Instantiate(Splashes, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
