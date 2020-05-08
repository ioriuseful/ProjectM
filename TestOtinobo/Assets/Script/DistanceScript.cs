using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceScript : MonoBehaviour
{
    //プレイヤーがどれだけ移動したのかを表示する処理

    [SerializeField, Header("メートルテキスト")] Text metorText;
    int metor = 0;//メートル表示

    
    // Start is called before the first frame update
    void Start()
    {
        metorText.text = string.Format("移動距離 {0}m", metor);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            metor++;
        }
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            metor--;
        }

        metorText.text = string.Format("移動距離 {0}m", metor);
    }
}
