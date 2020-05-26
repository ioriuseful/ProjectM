using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBreaker : MonoBehaviour
{
    [Header("エフェクト削除時間")] public float BreakTime;
    // Start is called before the first frame update
    void Start()
    {
        //エフェクトが生成されてBreakTime後にオブジェクトを削除する
        Invoke("BreakEffect", BreakTime);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //エフェクト(自分自身)を削除する
    void BreakEffect()
    {
        Destroy(gameObject);
    }
}
