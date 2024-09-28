using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLaserRay : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaserActivation(float laserDuration, float lenght){
        transform.localScale = new Vector3(transform.localScale.x,lenght,transform.localScale.z);
        StartCoroutine(nameof(LaserOn), laserDuration);
    }
    
    private IEnumerator LaserOn(float laserDuration){
        yield return new WaitForSeconds(laserDuration);
        animator.SetTrigger("End");
    }

    private void LaserOff(){
        Destroy(gameObject);
    }
}
