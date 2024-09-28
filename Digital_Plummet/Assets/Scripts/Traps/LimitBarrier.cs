using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitBarrier : MonoBehaviour
{
    [Tooltip("How fast the barrier moves")]
    [SerializeField] private float speed;
    private Transform player;

    [Tooltip("Distance where the player will die, manually located")]
    [SerializeField] private Transform dmgLimit;

    Vector3 distanceFromPlayer;

    private void Start() {
        if(GameObject.FindWithTag("Player").TryGetComponent<Transform>(out Transform transform)){
            player = transform;
        }
    }

    private void Update() {
        if(player != null){
            distanceFromPlayer = player.position - dmgLimit.position;
            if(distanceFromPlayer.magnitude>10){
            transform.Translate(distanceFromPlayer*speed*Time.deltaTime);
            }
            else {
                transform.Translate(Vector3.down*speed*Time.deltaTime);
            }
        }
    }
}
