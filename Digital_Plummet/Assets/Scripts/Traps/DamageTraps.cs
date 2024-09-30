using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTraps : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            //PlayerController player = other.gameObject.GetComponent<PlayerController>();
            PlayerControllerV2 playerV2 = other.gameObject.GetComponent<PlayerControllerV2>();
            //player.TakeDmg();
            playerV2.TakeDmg();
        }
    }
}
