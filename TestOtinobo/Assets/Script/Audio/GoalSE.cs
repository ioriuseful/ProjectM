using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSE : MonoBehaviour
{
    public AudioClip goalselectse;
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
        if (Input.GetKeyDown(KeyCode.DownArrow) && change == true)
        {
            audioSource.PlayOneShot(goalselectse);
            change = false;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && change == false)
        {
            audioSource.PlayOneShot(goalselectse);
            change = true;
        }
    }

    public void GoalEvent()
    {
        audioSource.PlayOneShot(goalselectse);
    }
}
