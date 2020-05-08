using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSE : MonoBehaviour
{
    public AudioClip goalselectse;
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

    public void GoalEvent()
    {
        audioSource.PlayOneShot(goalselectse);
    }
}
