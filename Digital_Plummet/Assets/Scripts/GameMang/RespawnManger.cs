using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject limitBarrier;

    private GameObject playerGO;

    void Start(){
        if (playerGO == null){
            playerGO = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void Respawn(){
        if (limitBarrier != null){
            limitBarrier.transform.position = new Vector2(respawnPoint.transform.position.x, respawnPoint.transform.position.y + 20);     
        }
        else{
            //nothing to do with the barrier...
        }

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            playerGO.transform.position = respawnPoint.transform.position;
            //playerGO.GetComponent<PlayerController>().ResetCharacter();
            playerGO.GetComponent<PlayerControllerV2>().ResetCharacter();
        }
    }

    public Transform RespawnPoint{
        set { respawnPoint = value; }
    }
}
