using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial Panels")]

    [Tooltip("Panel that shows the player press to fall faster")]
    [SerializeField] GameObject PressToFallPanel;

    [Tooltip("Panel that shows the player press to lift to slow down")]
    [SerializeField] GameObject LiftToSlowPanel;

    [Tooltip("Panel that shows the player press to slide")]
    [SerializeField] GameObject SlidePanel;

    private PlayerControllerV2 playerController;
    private bool finishedFirstPart;
 
    void Start()
    {
        playerController = FindObjectOfType<PlayerControllerV2>();

        if (playerController != null){
            playerController.TutorialBlock = true; 
        }  

        finishedFirstPart = false; 
    }

    void Update()
    {
        if (!playerController.TutorialBlock && !finishedFirstPart){
            playerController.TutorialBlock = true; 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            playerController.TutorialBlock = false;
            finishedFirstPart = true;
        }
    }

    public void FallingFasterTuto(){
        PressToFallPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ExitFallingFasterTuto(){
        Time.timeScale = 1f;

        PressToFallPanel.SetActive(false);
    }

    public void LiftToSlowTuto(){
        LiftToSlowPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ExitLiftToSlowTuto(){
        Time.timeScale = 1f;
        LiftToSlowPanel.SetActive(false);
    }

    public void SlideTuto(){
        SlidePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ExitSlideTuto(){
        Time.timeScale = 1f;
        SlidePanel.SetActive(false);
    }
}
