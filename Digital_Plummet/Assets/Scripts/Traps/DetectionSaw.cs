using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSaw : MonoBehaviour
{
    [SerializeField] Collider2D areaDetection;
    [SerializeField] Transform saw;
    [SerializeField] float sawSpeed;
    [SerializeField] Vector3 objective;
    // Start is called before the first frame update
    void Start()
    {
        areaDetection=GetComponent<Collider2D>();
        objective=saw.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.Normalize(objective-saw.position);

        saw.Translate(0,direction.y*sawSpeed*Time.deltaTime,0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag=="Player"){
            Debug.Log("Entro");
            objective = other.gameObject.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag=="Player"){
            Debug.Log("Entro");
            objective = other.gameObject.transform.position;
        }
    }
}
