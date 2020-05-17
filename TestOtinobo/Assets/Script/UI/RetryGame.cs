using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public PlayerScript player;
    public AudioClip selectse,enterse;
    AudioSource audioSource;
    public GameObject OnPanel, OnUnPanel,Panel,canvasObject;
    public bool pauseGame = false;
    private GameObject prefab, prefab2;
    public bool setGround=false;
    public bool setGround2 = false;
    public void Start()
    {
        OnUnPause();
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        //setGround = player.ColorWallRight;
        //setGround2 = player.ColorWallLeft;
        //if (setGround == true&&setGround2==true)
        //{
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Return))
        {
           audioSource.PlayOneShot(enterse);
           pauseGame = !pauseGame;

           if (pauseGame == true)
           {
             OnPause();
           }
           else
           {
             OnUnPause();
           }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            audioSource.PlayOneShot(selectse);
        }
        //}
    }

    public void OnPause()
    {
        prefab = (GameObject)Instantiate(OnPanel);
        prefab.transform.SetParent(canvasObject.transform, false);
        prefab2 = (GameObject)Instantiate(OnUnPanel);
        prefab2.transform.SetParent(canvasObject.transform, false);
        Panel.SetActive(true);
        Time.timeScale = 0;
        pauseGame = true;
        Cursor.visible = false;    // カーソル表示
        Cursor.lockState = CursorLockMode.Locked;     // 標準モード

    }

    public void OnUnPause()
    {
        Destroy(prefab);
        Destroy(prefab2);
        Time.timeScale = 1;
        pauseGame = false;
        Panel.SetActive(false);
        Cursor.visible = false;     // カーソル非表示
        Cursor.lockState = CursorLockMode.Locked;   // 中央にロック

    }

    public void OnRetry()
    {
        SceneManager.LoadScene("SampleTitle");
    }

    public void OnResume()
    {
        OnUnPause();
    }
    public void Retry()
    {
        pauseGame = !pauseGame;

        if (pauseGame == true)
        {
            OnPause();
        }
        else
        {
            OnUnPause();
        }
    }
}