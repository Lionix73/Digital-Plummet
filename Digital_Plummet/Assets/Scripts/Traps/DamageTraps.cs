using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTraps : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.TakeDmg();
        }
    }
}
