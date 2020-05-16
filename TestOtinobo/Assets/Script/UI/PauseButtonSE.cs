using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonSE : MonoBehaviour
{
    public AudioClip selectse,enterse;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonSE()
    {
        audioSource.PlayOneShot(selectse);
    }
    public void ButtonEnter()
    {
        audioSource.PlayOneShot(enterse);
    }
}
