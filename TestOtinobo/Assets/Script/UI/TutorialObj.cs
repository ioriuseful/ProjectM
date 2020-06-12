using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObj : MonoBehaviour
{
    //操作確認に飛ばすためのスクリプト
    [Header("操作確認って書いてあるやつ")]
    public GameObject MoveTutorial;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(MoveTutorial, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
