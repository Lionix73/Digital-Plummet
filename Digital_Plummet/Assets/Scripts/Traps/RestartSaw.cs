using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartSaw : MonoBehaviour
{
    [SerializeField] DetectionSaw sawWillReset;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            sawWillReset.ResetPosition();
        }
    }
}
