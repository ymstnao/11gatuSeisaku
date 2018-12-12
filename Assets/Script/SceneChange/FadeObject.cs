using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FadeObject : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed = 1f;

    [SerializeField]
    private List<MaskableGraphic> fadeObjects;

    public float FadeSpeed
    {
        get
        {
            return fadeSpeed;
        }

        set
        {
            fadeSpeed = value;
        }
    }

    /// <summary>
    /// フェードイン始める
    /// </summary>
    /// <param name="speed">速度（設定しなければデフォルトの速度）</param>
    public void StartFadeIn(float speed = 0)
    {
        if (speed <= 0)
        {
            speed = fadeSpeed;
        }
        StartCoroutine(FadeIn(speed));
    }

    /// <summary>
    /// フェードアウト始める
    /// </summary>
    /// <param name="speed">速度（設定しなければデフォルトの速度）</param>
    public void StartFadeOut(float speed = 0)
    {
        if (speed <= 0)
        {
            speed = fadeSpeed;
        }
        StartCoroutine(FadeOut(speed));
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    /// <param name="speed">速度</param>
    /// <returns></returns>
    private IEnumerator FadeIn(float speed)
    {
        float a = 0;
        while (true)
        {
            a += speed * Time.deltaTime;

            if (a >= 1)
            {
                a = 1;
                fadeObjects = ChangeAlphaAll(fadeObjects, a);
            }

            fadeObjects = ChangeAlphaAll(fadeObjects, a);
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <param name="speed">速度</param>
    /// <returns></returns>
    private IEnumerator FadeOut(float speed)
    {
        float a = 1;
        while (true)
        {
            a -= speed * Time.deltaTime;

            if (a <= 0)
            {
                a = 0;
                fadeObjects = ChangeAlphaAll(fadeObjects, a);
                yield break;
            }
            fadeObjects = ChangeAlphaAll(fadeObjects, a);
            
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// リスト内のすべてのアルファ値を変更する
    /// </summary>
    /// <param name="target"></param>
    /// <param name="alpha"></param>
    /// <returns></returns>
    private List<MaskableGraphic> ChangeAlphaAll(List<MaskableGraphic> target, float alpha)
    {
        return target.Select(t => {
            Color c = t.color;
            c.a = alpha;
            t.color = c;
            return t;
        }).ToList();
    }
}
