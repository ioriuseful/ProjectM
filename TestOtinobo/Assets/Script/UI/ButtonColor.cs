using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color32(248,255,0,255);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<Renderer>().material.color = new Color32(248, 255, 0, 255);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            GetComponent<Renderer>().material.color = new Color32(255,255,255,255);
        }
    }
}
