using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPEffect : MonoBehaviour
{
    [Tooltip("How long the EMP affect the player")]
    [SerializeField] float effectDuration;
    private GameObject playerPos;
    private Vector3 pushForce;
    [SerializeField] private float pushPower;

    void Update(){
        playerPos = GameObject.FindWithTag("Player");
        pushForce=playerPos.transform.position-transform.position;
        pushForce.Normalize();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            PlayerControllerV2 player = other.gameObject.GetComponent<PlayerControllerV2>();
            //Activacion de cosa que hace... falta hacer

            player.EMPHit(pushForce,pushPower);
        }
    }
}
