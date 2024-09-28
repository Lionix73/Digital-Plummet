using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovilPlatform : MonoBehaviour
{
    float platSpeed;

    //[SerializeField] float offSet; not using right now...
    private float timeChange;

    [Tooltip("How long will take for move to one point to the next, basically affect the speed of the platform")]
    [SerializeField] float timeChangeMax;
    [Tooltip("Point between the platform is gona move")]
    [SerializeField] Transform[] movementPoints;

    [Tooltip("From where will start its movement")]
    [SerializeField] int startingPoint;
    int moveObjective;
    Vector3 direction;
    private Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = gameObject.transform;
        transform.position=movementPoints[startingPoint].position;
        moveObjective=startingPoint+1;
        timeChange=timeChangeMax;
        platSpeed=(movementPoints[moveObjective].position-transform.position).magnitude/timeChange;
    }

    // Update is called once per frame
    void Update()
    {
        timeChange-=Time.deltaTime;
        if (timeChange<=0){
            NextPoint();
            timeChange=timeChangeMax;
        }

        if(moveObjective>movementPoints.Length-1){
            moveObjective=0;
        }
        direction=Vector3.Normalize(movementPoints[moveObjective].position-transform.position);
        transform.Translate(direction*platSpeed*Time.deltaTime);
        
    }

    void NextPoint(){
        moveObjective++;
        Debug.Log("Next");
    }
}
