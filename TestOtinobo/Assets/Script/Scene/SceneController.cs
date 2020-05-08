using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public AudioClip selectse;
    AudioSource audioSource;
    private bool end=false;
    private float time;
    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (end == true && time > 0.9f)
        {
            Quit();
        }
    }
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }

    public void ButtonClickedTitle()
    {
        audioSource.PlayOneShot(selectse);
        SceneManager.LoadScene("SampleTitle");
    }

    public void ButtonClickedGameScene()
    {
        audioSource.PlayOneShot(selectse);
        SceneManager.LoadScene("SampleScene");
    }
    
    public void ButtonClickedEnd()
    {
        audioSource.PlayOneShot(selectse);
        time = 0;
        end = true;
    }
}
