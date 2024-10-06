using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        angle=0f;
    }

    // Update is called once per frame
    private void Update() {
        angle += Time.deltaTime*rotationSpeed;
        transform.RotateAround(transform.position,Vector3.back,angle);
    }

}
