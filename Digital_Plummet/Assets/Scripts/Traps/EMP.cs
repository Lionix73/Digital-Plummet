using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour
{
    [SerializeField] private Collider2D collider;
    [SerializeField] private SpriteRenderer shockImg;
    [SerializeField] float shockCD;
    float shockTime;

    private void Start() {
        collider = GetComponentInChildren<Collider2D>();
        collider.enabled = false;
        shockImg.enabled = false;
        shockTime=shockCD;
    }

    private void Update() {
        shockTime-=Time.deltaTime;

        if (shockTime < 0){
            StartCoroutine(nameof(ShockWave));
            shockTime=shockCD;
        }
    }

    private IEnumerator ShockWave(){
        collider.enabled=true;
        shockImg.enabled=true;
        yield return new WaitForSeconds(0.3f);
        collider.enabled=false;
        shockImg.enabled=false;
    }
}
