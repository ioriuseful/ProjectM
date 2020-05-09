using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    void Start()
    {
        Selectable select = GetComponent<Selectable>();
        select.Select();
    }
}
