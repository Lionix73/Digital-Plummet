using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using JetBrains.Annotations;
using TMPro;
using System;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] float fadeTime = 1.0f;
    [SerializeField] Transform fallingCharacter;
    [SerializeField] Transform handPressing;
    [SerializeField] Transform handSliding;
    [SerializeField] float movementY;
    [SerializeField] float movementX;
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
        DOTween.Sequence()
        .PrependInterval(1)
        .Append(fallingCharacter.DOMoveY(fallingCharacter.position.y - movementY, fadeTime))
        .Append(fallingCharacter.DOMoveY(fallingCharacter.position.y + movementY, 2*fadeTime));

        DOTween.Sequence()
        .PrependInterval(1)
        .Append(handPressing.DOScale(0.5f, fadeTime))
        .Append(handPressing.DOScale(1, fadeTime));

        DOTween.Sequence()
    .   PrependInterval(3*fadeTime)
        .Append(handSliding.DOMoveX(- movementX, fadeTime))
        .Append(handSliding.DOMoveX( movementX, 2 * fadeTime));
    }

}
