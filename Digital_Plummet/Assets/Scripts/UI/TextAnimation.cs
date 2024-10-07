using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using JetBrains.Annotations;
using TMPro;
using System;
using System.Runtime.CompilerServices;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] float fadeTime = 1.0f;
    [SerializeField] RectTransform fallingCharacter;
    [SerializeField] RectTransform hand;
    [SerializeField] float movementY;
    [SerializeField] float movementX;
    [SerializeField] bool tapScreen;
    [SerializeField] bool liftScreen;
    [SerializeField] bool slideScreen;
    public Canvas myCanvas;
    // Start is called before the first frame update
    /* void Start()
     {
         DOTween.Sequence()
         .PrependInterval(1)
         .Append(fallingCharacter.DOMoveY(fallingCharacter.position.y - movementY, fadeTime))
         .Append(fallingCharacter.DOMoveY(fallingCharacter.position.y + movementY, 2*fadeTime));

         DOTween.Sequence()
         .PrependInterval(1)
         .Append(handPressing.DOScale(0.5f, fadeTime))
         .Append(handPressing.DOScale(1, fadeTime));

         DOTween.Sequence()
         .PrependInterval(3*fadeTime)
         .Append(handSliding.DOMoveX(- movementX, fadeTime))
         .Append(handSliding.DOMoveX( movementX, 2 * fadeTime));




     } */

    private void OnEnable()
    {
        if (tapScreen == true)
        {
            PressScreen();
            DOTween.Sequence().SetUpdate(true)
            .PrependInterval(1).SetUpdate(true)
            .Append(fallingCharacter.DOAnchorPosY(fallingCharacter.anchoredPosition.y + movementY , 2 * fadeTime, true)).SetUpdate(true);
            
        }
        else if (liftScreen == true)
        {
            DOTween.Sequence().SetUpdate(true)
            .PrependInterval(1).SetUpdate(true)
            .Append(fallingCharacter.DOAnchorPosY(fallingCharacter.anchoredPosition.y + (movementY), 2*fadeTime, true)).SetUpdate(true)
            .Append(fallingCharacter.DOAnchorPosY(fallingCharacter.anchoredPosition.y + (1.25f*movementY), 2 * fadeTime, true)).SetUpdate(true);
            PressScreen();
        }
        else if(slideScreen == true)
        {
            SlideScreen();
        }
       /* DOTween.Sequence()
        .PrependInterval(1)
        .Append(fallingCharacter.DOMoveY(fallingCharacter.position.y - movementY, fadeTime))
        .Append(fallingCharacter.DOMoveY(fallingCharacter.position.y + movementY, 2*fadeTime)); */



    }
    private void PressScreen()
    {

        DOTween.Sequence().SetUpdate(true)
        .PrependInterval(1).SetUpdate(true)
        .Append(hand.DOScale(hand.localScale / 2, fadeTime)).SetUpdate(true)
        .Append(hand.DOScale(hand.localScale, fadeTime)).SetUpdate(true);
    }

    private void SlideScreen()
    {
        RectTransform canvasRect = myCanvas.GetComponent<RectTransform>();
        float canvasWidth = canvasRect.rect.width;
        DOTween.Sequence().SetUpdate(true)
        .PrependInterval(1).SetUpdate(true)
        .Append(hand.DOAnchorPosX(hand.anchoredPosition.x - movementX, fadeTime,true)).SetUpdate(true)
        .Append(hand.DOAnchorPosX(hand.anchoredPosition.x + movementX, 2 * fadeTime, true)).SetUpdate(true);
    }
}
