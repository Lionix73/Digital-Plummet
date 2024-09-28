using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSaw : MonoBehaviour
{
    float sawSpeed;

    //[SerializeField] float offSet; not using right now...
    private float timeChange;

    [Tooltip("How long the saw will take form each point to another, basically affects the speed of the saw")]
    [SerializeField] float timeChangeMax;

    [Tooltip("Point where the saw will move, in order of the list")]
    [SerializeField] Transform[] movementPoints;

    [Tooltip("Where the saw will start its movement")]
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
        sawSpeed=(movementPoints[moveObjective].position-transform.position).magnitude/timeChange;
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
        transform.Translate(direction*sawSpeed*Time.deltaTime);
        
    }

    void NextPoint(){
        moveObjective++;
        Debug.Log("Next");
    }
}
