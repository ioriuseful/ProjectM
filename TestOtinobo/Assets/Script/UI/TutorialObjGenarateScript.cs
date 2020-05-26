using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjGenarateScript : MonoBehaviour
{
    [Header("ゲームシステムって書いてあるやつ")]
    public GameObject GameSystemTutorial;
    [Header("操作確認って書いてあるやつ")]
    public GameObject MoveTutorial;

    bool mflag = false;
    bool sflag = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && !sflag)
        {
            Instantiate(GameSystemTutorial, transform.position, Quaternion.identity);
            sflag = true;
            mflag = false;
            Destroy(MoveTutorial);
        }
        if (Input.GetKeyDown(KeyCode.Return) && !mflag)
        {
            Instantiate(MoveTutorial, transform.position, Quaternion.identity);
            mflag = true;
            sflag = false;
            Destroy(GameSystemTutorial);
        }
    }
}
