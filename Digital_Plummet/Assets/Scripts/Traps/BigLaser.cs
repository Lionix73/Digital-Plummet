using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLaser : MonoBehaviour
{
    [Tooltip("Where the laser its gona instantiate")]
    [SerializeField] Transform[] lasersPos;
    [Tooltip("From where its supose to start the laser... waiting for animations")]
    [SerializeField] Transform[] turretPos;
    private int whereNext;
    [Tooltip("How long the big laser i going to be active")]
    [SerializeField] float laserDuration;
    private float timeActive;
    [Tooltip("Waiting time after the laser end for the next one to spawn")]
    [SerializeField] float timeBetweenLasers;
    float timeLastActivation; //no serializar
    [Tooltip("For calculating the position of each laser, in review for using it on scales")]
    [SerializeField] float laserDistance;
    [Tooltip("The prefab for the big laser")]
    [SerializeField] BigLaserRay prefabLaser;
    private BigLaserRay laserActive;
    private bool laserON;

    // Start is called before the first frame update
    void Start()
    {
        timeLastActivation=timeBetweenLasers;
        whereNext=0;
        laserON=false;
        timeActive=laserDuration;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0;i<lasersPos.Length;i++){
            lasersPos[i].position = new Vector3 (turretPos[i].position.x,turretPos[i].position.y+laserDistance,turretPos[i].position.z);
        }


        if(!laserON){
            timeLastActivation+=Time.deltaTime;
        }
        else{
            timeActive-=Time.deltaTime;
        }


        if (timeLastActivation>timeBetweenLasers){
            if (lasersPos[whereNext]!=null){
                laserActive=Instantiate(prefabLaser,lasersPos[whereNext].position,Quaternion.identity);
                laserActive.LaserActivation(laserDuration,laserDistance);
            }
            timeLastActivation=0;
            laserON=true;
        }

        if(timeActive<0){
            laserON=false;
            timeActive=laserDuration;
            whereNext++;
            if (whereNext>lasersPos.Length-1){
                whereNext=0;
            }
        }

        
    }
}
