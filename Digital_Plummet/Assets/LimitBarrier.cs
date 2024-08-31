using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitBarrier : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform player;
    [SerializeField] private Transform dmgLimit;

    [SerializeField] Vector3 distanceFromPlayer;

    private void Start() {
        if(GameObject.FindWithTag("Player").TryGetComponent<Transform>(out Transform transform)){
            player = transform;
        }
    }

    private void Update() {

        distanceFromPlayer = player.position - dmgLimit.position;
        transform.Translate(distanceFromPlayer*speed*Time.deltaTime);
    }
}
