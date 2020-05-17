using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class ColorBlockScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    public enum ColorState
    {
        White, Red, Blue, Green,
    }
    public ColorState CS;

    public PlayerScript player;
    private string Color;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        switch (CS)
        {
            case ColorState.White:
                sprite.color = new Color(1, 1, 1, 1);
                Color = "white";
                break;
            case ColorState.Red:
                sprite.color = new Color(1, 0, 0, 1);
                Color = "Red";
                break;
            case ColorState.Green:
                sprite.color = new Color(0, 1, 0, 1);
                Color = "Green";
                break;
            case ColorState.Blue:
                sprite.color = new Color(0, 0, 1, 1);
                Color = "Blue";
                break;
        }
        if (Color == player.Color)
        {
            if (GetComponent<CapsuleCollider2D>() == null && GetComponent<BoxCollider2D>() == null)
            {
                GetComponent<EdgeCollider2D>().enabled = false;
            }

            if (GetComponent<BoxCollider2D>() == null)
            {
                GetComponent<CapsuleCollider2D>().enabled = false;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }

        }
        else
        {
            if (GetComponent<CapsuleCollider2D>() == null && GetComponent<BoxCollider2D>() == null)
            {
                GetComponent<EdgeCollider2D>().enabled = true;
            }


            if (GetComponent<BoxCollider2D>() == null)
            {
                GetComponent<CapsuleCollider2D>().enabled = true;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
