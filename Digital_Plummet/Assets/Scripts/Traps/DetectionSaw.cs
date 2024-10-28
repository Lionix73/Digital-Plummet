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
    [SerializeField] float sawSpeedBack;
    private Vector3 objective; private Vector3 origin;
    [SerializeField] bool vertical;
    private bool following;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        areaDetection=GetComponent<Collider2D>();
        sound = GetComponent<AudioSource>();

        origin =saw.position; 
        objective = saw.position;
        following= false;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 direction = Vector3.Normalize((objective)-(saw.position));
        if (!following){
            direction = Vector3.Normalize((origin)-(saw.position));
        }

        if (vertical){
            
            if (!following){
                saw.Translate(0,direction.y*sawSpeedBack*Time.deltaTime,0);
            }
            else{saw.Translate(0,direction.y*sawSpeed*Time.deltaTime,0);}
        }
        else {
            
            if (!following){
                saw.Translate(direction.x*(sawSpeedBack)*Time.deltaTime,0,0);
            }
            else{saw.Translate(direction.x*sawSpeed*Time.deltaTime,0,0);}
        }
    }

    private void FixedUpdate() {
        if (vertical){
            if (Mathf.Abs(saw.position.y-objective.y) < 0.5){
                following=false;
            }
        }
        else {
            if (Mathf.Abs(saw.position.x-objective.x) < 0.5){
                following=false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag=="Player"){
            //Debug.Log("Entro");
            following=true;
            sound.Play();
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
