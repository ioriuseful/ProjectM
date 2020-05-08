using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSE : MonoBehaviour
{
    public AudioClip gameoverselectse;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOverEvent()
    {
        audioSource.PlayOneShot(gameoverselectse);
    }
}
