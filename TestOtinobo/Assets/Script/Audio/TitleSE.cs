using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSE : MonoBehaviour
{
    public AudioClip selectse;
    AudioSource audioSource;
    private bool change;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        change = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && change == false)
        {
            audioSource.PlayOneShot(selectse);
            change = true;
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && change == true)
        {
            audioSource.PlayOneShot(selectse);
            change = false;
        }
    }

    public void Event()
    {
        audioSource.PlayOneShot(selectse);
    }


}
