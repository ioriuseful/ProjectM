﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLineCount : MonoBehaviour
{
    public PlayerScript player;
    public int score;
    Text text;
    //アニメーション用
    private int changescore;
    private Animator animator;

    AudioSource audioSource;
    [SerializeField, Header("Score変更時のSE")] public AudioClip ScoreSE;

    private string Sstring;

    void Start()
    {
        text = GameObject.Find("ScoreLineText").GetComponent<Text>();//テキストを探す
        player = GameObject.Find("Player").GetComponent<PlayerScript>();//プレイヤースクリプトを探す
        animator = GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    void Update()
    {
        score = player.scoreline;
        if(changescore != score)
        {
            StartAnime();
        }
        changescore = score;
        Sstring = score.ToString();
        text.text = Sstring;
    }
    void StartAnime()
    {
        audioSource.PlayOneShot(ScoreSE);
        animator.SetBool("ChangeScore", true);
        Invoke("StopAnime", 0.1f);
    }

    void StopAnime()
    {
        animator.SetBool("ChangeScore", false);
    }
}
