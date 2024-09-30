using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
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
        //Debug.Log(playerController.TutorialBlock);

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
}
