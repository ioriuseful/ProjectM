using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectButton : MonoBehaviour
{
    Selectable select;
    void Start()
    {
        Selectable select = GetComponent<Selectable>();
        select.Select();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            select.Select();
        }
    }
}
