using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceKeyScript : MonoBehaviour
{
    [Header("押される前")]
    public Sprite DefaultSprite;
    [Header("押された後")]
    public Sprite PushSprite;
    
    SpriteRenderer MainSpriteRenderer;

    void Start()
    {
        MainSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeSprite();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ChangeDeffalut();
        }
    }

    void ChangeSprite()
    {
        MainSpriteRenderer.sprite = PushSprite;
    }
    void ChangeDeffalut()
    {
        MainSpriteRenderer.sprite = DefaultSprite;
    }
}
