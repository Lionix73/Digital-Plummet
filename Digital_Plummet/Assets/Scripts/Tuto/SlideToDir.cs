using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SlideToDir : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            tutorialManager.SlideTuto();
        }
    }
}
