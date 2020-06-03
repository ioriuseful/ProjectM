using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    [SerializeField, Header("フェードして消えるまでの時間")] float fadeTime = 0.5f;

    private float timelimit;
    private SpriteRenderer sprite;

    void Start()
    {
        timelimit = fadeTime;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timelimit -= Time.deltaTime;
        if (timelimit <= 0f)
        {
            IsDead();
        }

        float alpha = timelimit / fadeTime;
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

    void IsDead()
    {
        Destroy(gameObject);
    }
}
