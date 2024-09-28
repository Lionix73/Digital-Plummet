using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour
{
    [Tooltip("The area of the effect")]
    [SerializeField] private Collider2D collider;
    [Tooltip("Placeholder for the alpha, This will probably change, waiting for animation")]
    [SerializeField] private SpriteRenderer shockImg;
    [Tooltip("Time bewtween each pulse")]
    [SerializeField] float shockCD;
    [Tooltip("How long the area of the pulse is active")]
    [SerializeField] private float shockWaveDuration;
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
        yield return new WaitForSeconds(shockWaveDuration);
        collider.enabled=false;
        shockImg.enabled=false;
    }
}
