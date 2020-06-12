using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour
{
    [Header("押されてないときの画像")]
    public Sprite defaultArrow;
    [Header("押されているときの画像")]
    public Sprite ShadowArrow;

    SpriteRenderer MainSpriteRenderer;

    [Header("右の画像？")]
    public bool isrightArrowflag;
    [Header("左の画像？")]
    public bool isleftArrowflag;

    void Start()
    {
        MainSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isrightArrowflag)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                ChangeRightArrowSprite();
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                ChangeDefaultArrowSprite();
            }
        }
        else if (isleftArrowflag)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                ChangeRightArrowSprite();
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                ChangeDefaultArrowSprite();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                ChangeRightArrowSprite();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                ChangeDefaultArrowSprite();
            }
        }
        
    }
    void ChangeDefaultArrowSprite()
    {
        MainSpriteRenderer.sprite = defaultArrow;
    }
    void ChangeRightArrowSprite()
    {
        MainSpriteRenderer.sprite = ShadowArrow;
    }


}