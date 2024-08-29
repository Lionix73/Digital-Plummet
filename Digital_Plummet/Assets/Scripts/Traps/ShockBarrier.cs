using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockBarrier : MonoBehaviour
{
    [SerializeField] Transform[] RayPoints;
    float timeGapActivation;
    [SerializeField] float timeGapActivationMax;
    [SerializeField] float gapTime;
    [SerializeField] GameObject ray;

    private void Start() {
        ray.transform.position = Vector3.Lerp(RayPoints[0].position,RayPoints[1].position,0.5f);
        //ray.transform.rotation = Quaternion.Lerp(RayPoints[0].rotation,RayPoints[1].rotation,0.5f); not using...
        ray.transform.localScale= new Vector3((RayPoints[0].position - RayPoints[1].position).magnitude,0.1f,0);

        timeGapActivation=timeGapActivationMax;
    }

    private void Update() {
        timeGapActivation-=Time.deltaTime;

        if (timeGapActivation<0){
            StartCoroutine(nameof(Activation));
            timeGapActivation=timeGapActivationMax;
        }
    }

    private IEnumerator Activation(){
        ray.SetActive(false);
        yield return new WaitForSeconds(gapTime);
        ray.SetActive(true);
    }
}
