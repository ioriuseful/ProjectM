using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{
    public AudioClip selectse;
    AudioSource audioSource;
    private float time;
    private bool end = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        FadeManager.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (end == true && time > 0.9f)
        {
            Quit();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    /// ボタンをクリックした時の処理
    public void OnClick()
    {
        audioSource.PlayOneShot(selectse);
        FadeManager.FadeOut(0);
    }


    public void Retry()
    {
        audioSource.PlayOneShot(selectse);
        SceneManager.LoadScene("SampleEnd");
    }
    public void ButtonClickedEnd()
    {
        audioSource.PlayOneShot(selectse);
        time = 0;
        end = true;
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
