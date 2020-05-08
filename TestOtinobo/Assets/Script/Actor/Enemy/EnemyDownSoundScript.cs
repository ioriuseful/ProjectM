using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDownSoundScript : MonoBehaviour
{
    private AudioSource audioSource;
    bool isAudioStartFlag;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        isAudioStartFlag = true;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            audioSource.Play();
        }

        //音が鳴っていなかったら削除
        if(!audioSource.isPlaying && isAudioStartFlag)
        {
            Destroy(gameObject);
        }
    }
}
