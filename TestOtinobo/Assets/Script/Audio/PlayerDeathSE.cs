using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathSE : MonoBehaviour
{
    public AudioClip PlayerDeathse;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(PlayerDeathse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
