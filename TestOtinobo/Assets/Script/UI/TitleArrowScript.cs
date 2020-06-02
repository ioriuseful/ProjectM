using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleArrowScript : MonoBehaviour
{
    private int arrowposition;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            arrowposition = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            arrowposition = 0;
        }

        if(arrowposition == 0)
        {
            transform.position = new Vector2(-7.4f, -2.3f);
        }
        else if(arrowposition == 1)
        {
            transform.position = new Vector2(2.6f,-2.2f);
        }
    }
}
