using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialJumpCount0script : MonoBehaviour
{
    private TutorialPlayerScript Pscript;
    private Text text;
    private bool animationoff;
    private bool alphachange = false;
    private float alpha;
    private int addcount;
    private float addtime;
    [SerializeField, Header("点滅回数")] public int count = 3;
    [SerializeField, Header("alpha値最大の時間")] public float stoptime = 1.0f;
    [SerializeField, Header("alpha値の上昇速度")] public float alphaspeed = 0.1f;

    void Start()
    {
        Pscript = GameObject.Find("TutorilaPlayer").GetComponent<TutorialPlayerScript>();
        text = GameObject.Find("TutorialJumpCount0").GetComponent<Text>();
        alpha = 0.0f;
        addcount = count;
        addtime = stoptime;
        animationoff = true;
    }

    void Update()
    {
        if (Pscript.IJumpC == 0 && Input.GetButtonDown("Jump") && animationoff == true)
        {
            animationoff = false;
        }
        if (animationoff == false)
        {
            Animation();
        }
    }

    void Animation()
    {
        if (alpha <= 1.0f && count > 0 && alphachange == false)
        {
            alpha += alphaspeed;
        }
        else
        {
            if (alphachange == false)
            {
                if (stoptime >= 0)
                {
                    stoptime -= 0.1f;
                }
                else
                {
                    stoptime = addtime;
                    alphachange = true;
                }
            }
        }

        if (alpha >= 0.0f && count > 0 && alphachange == true)
        {
            alpha -= alphaspeed;
        }
        else
        {
            if (alphachange == true)
            {
                alphachange = false;
                count--;
            }

        }

        if (count == 0)
        {
            animationoff = true;
            count = addcount;
        }
        text.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
}
