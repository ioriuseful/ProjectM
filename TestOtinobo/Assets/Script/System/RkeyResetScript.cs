using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RkeyResetScript : MonoBehaviour
{
    /// <summary>
    /// Rキーでリセットするためだけのスクリプト
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
