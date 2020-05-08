using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    //フェードを行うために作っておく関数
    private static Canvas fadeC;
    private static Image fadeI;

    //フェード用の透明度
    private static float alpha = 0.0f;

    //フェードのフラグ
    public static bool isFadeIn = false;
    public static bool isFadeOut = false;

    //フェード時間
    private static float fadeTime = 0.2f;

    //遷移先のシーン番号
    private static int nextScene = 1;

    //フェード用のCanvasとImage生成
    static void Init()
    {
        //フェード用のCanvas生成
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        fadeC = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        fadeC.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<FadeManager>();
        //最前面になるよう適当なソートオーダー設定
        fadeC.sortingOrder = 100;

        //フェード用のImage生成
        fadeI = new GameObject("ImageFade").AddComponent<Image>();
        fadeI.transform.SetParent(fadeC.transform, false);
        fadeI.rectTransform.anchoredPosition = Vector3.zero;
        //Imageサイズは適当に大きく設定してください。
        fadeI.rectTransform.sizeDelta = new Vector2(9999, 9999);
    }

    //フェードイン開始
    public static void FadeIn()
    {
        if (fadeI == null) Init();
        fadeI.color = Color.black;
        isFadeIn = true;
    }

    //フェードアウト開始
    public static void FadeOut(int n)
    {
        if (fadeI == null) Init();
        nextScene = n;
        fadeI.color = Color.clear;
        fadeC.enabled = true;
        isFadeOut = true;
    }

    void Update()
    {
        //フラグ有効なら毎フレームフェードイン/アウト処理
        if (isFadeIn)
        {
            //経過時間から透明度計算
            alpha -= Time.deltaTime / fadeTime;

            //フェードイン終了判定
            if (alpha <= 0.0f)
            {
                isFadeIn = false;
                alpha = 0.0f;
                fadeC.enabled = false;
            }

            //フェード用Color 変更したい場合はここをいじってください。
            fadeI.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if (isFadeOut)
        {
            //経過時間から透明度計算
            alpha += Time.deltaTime / fadeTime;

            //フェードアウト終了判定
            if (alpha >= 1.0f)
            {
                isFadeOut = false;
                alpha = 1.0f;

                //次のシーンへ遷移
                SceneManager.LoadScene(nextScene);
            }

            //フェード用Imageの色・透明度設定
            fadeI.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
}
