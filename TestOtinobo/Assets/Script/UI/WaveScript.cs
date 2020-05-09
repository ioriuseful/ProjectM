using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WaveScript : MonoBehaviour
{
    public PlayerScript player;
    public int number;
    [SerializeField, Header("移動する高さ限界")] public float hight;
    [SerializeField, Header("移動速度")] public float speed;
    private Vector3 pos;
    private int score;
    public int wave;
    public int changewave;
    public int max;//Waveの最大値
    Text text;
    private Animator animator;
    private bool maxwave;

    void Start()
    {
        text = GameObject.Find("WaveText").GetComponent<Text>();//テキストを探す
        pos = transform.position;
        maxwave = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        score = player.scoreline;
        changewave = wave;
        if (wave >= max)
        {
            text.text = "Wave Max!";
            if(wave == max && maxwave == false)
            {
                StartAnime();
                maxwave = true;
            }
        }
        else
        {
            if (score % number == 0)
            {
                wave = score / number + 1;
                if (changewave != wave && wave != 1)
                {
                    //値が変化した時の処理
                    StartAnime();
                }
            }
            text.text = "Wave " + wave;
        }
    }

    void StartAnime()
    {
        animator.SetBool("ChangeWave", true);
        Invoke("StopAnime", 1.05f);
    }

    void StopAnime()
    {
        animator.SetBool("ChangeWave", false);
    }
}
