using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private RespawnManager respawnManager;
    void Awake(){
        if(respawnManager == null){
            respawnManager = FindObjectOfType<RespawnManager>();
            Debug.Log("Found Respawn Manag");
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            respawnManager.RespawnPoint = transform;
        }
    }
}
