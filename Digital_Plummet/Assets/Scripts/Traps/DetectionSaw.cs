using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSaw : MonoBehaviour
{
    [Tooltip("If the player enter this trigger, the saw will know")]
    [SerializeField] Collider2D areaDetection;
    [Tooltip("The saw...")]
    [SerializeField] Transform saw;
    [Tooltip("The speed of the saw...")]
    [SerializeField] float sawSpeed;
    private Vector3 objective;
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
            //Debug.Log("Entro");
            objective = other.gameObject.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag=="Player"){
            //Debug.Log("Entro");
            objective = other.gameObject.transform.position;
        }
    }
}
