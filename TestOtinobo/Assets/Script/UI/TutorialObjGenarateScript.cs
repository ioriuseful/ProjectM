using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjGenarateScript : MonoBehaviour
{
    [Header("ゲームシステムって書いてあるやつ")]
    public GameObject GameSystemTutorial;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Instantiate(GameSystemTutorial, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
