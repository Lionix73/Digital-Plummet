using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadesAnimation : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
       FadeOut();
    }

    public void FadeOut()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0, fadeTime).SetUpdate(true);
    }
    public void FadeIn()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, fadeTime).SetUpdate(true);

    }
}
