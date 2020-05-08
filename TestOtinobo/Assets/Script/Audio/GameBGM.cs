using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    public PlayerScript player;//プレイヤースクリプトからの死を取得する為
    public AudioSource audioSource;//オーディオソース
    public bool playerDead;//プレイヤーの死の変数宣言
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerDead = player.GetisDeadFlag;
        if(playerDead==true)
        {
            audioSource = this.GetComponent<AudioSource>();
            audioSource.Stop();
        }
    }
}
