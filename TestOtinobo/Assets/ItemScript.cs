using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public PlayerScript player;
    private float item;
    //アニメーション用
    private float changeitem;
    private Animator animator;

    AudioSource audioSource;
    [SerializeField, Header("item取得時のSE")] public AudioClip itemSE;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();//プレイヤースクリプトを探す
        animator = GetComponent<Animator>();
        changeitem = 0;
    }

    // Update is called once per frame
    void Update()
    {
        item = player.IJumpC;
        if (changeitem != item && changeitem < item)
        {
            StartAnime();
        }
        changeitem = item;
    }
    void StartAnime()
    {
        //audioSource.PlayOneShot(itemSE);
        animator.SetBool("ChangeItem", true);
        Invoke("StopAnime", 0.1f);
    }

    void StopAnime()
    {
        animator.SetBool("ChangeItem", false);
    }
}
