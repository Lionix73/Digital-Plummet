using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    private Rigidbody2D projectileRb;
    
    private void Awake() {
        projectileRb = GetComponent<Rigidbody2D>();
    }

    public void ShootLaser(Vector3 direction){
        projectileRb.velocity = direction*speed;
        Destroy(gameObject,lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.TakeDmg();
        }
        if(other.gameObject.tag != "Projectile"){
            Destroy(gameObject);
        }
    }
}
