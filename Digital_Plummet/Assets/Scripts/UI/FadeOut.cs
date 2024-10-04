using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    [SerializeField] float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0, fadeTime).SetUpdate(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
