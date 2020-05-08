using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    Slider slider;//スライダー

    public GameObject player;//プレイヤーの蒸気の値を取得したい為
    public PlayerScript script;//同上
    float steampoint;//蒸気の値
    // Start is called before the first frame update
    void Start()
    { 
        slider = GameObject.Find("SteamSlider").GetComponent<Slider>();//スライダーを探す
    }

    // Update is called once per frame
    void Update()
    {
        steampoint = script.SteamPoint;
        slider.value = steampoint;//値によって変動させる
    }
}
