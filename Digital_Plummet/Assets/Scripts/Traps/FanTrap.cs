using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTrap : MonoBehaviour
{
    [Tooltip("Strength of the pulse")]
    [SerializeField] float pushPower;
    [Tooltip("Just the direction, its normalize inside the code")]
    [SerializeField] Vector2 pushForce;

    private Rigidbody2D playerBody;
    AudioSource sound;

    private void Start() {
        pushForce.Normalize();
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag=="Player"){
            sound.Play();
            playerBody = other.gameObject.GetComponent<Rigidbody2D>();
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (playerBody!=null)
        {
            playerBody.AddForce(pushForce*pushPower,ForceMode2D.Force);
        }
    }
}
