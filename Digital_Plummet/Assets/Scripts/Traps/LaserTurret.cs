using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    [SerializeField] Transform objetive;
    [SerializeField] Transform shootPos;
    Vector3 position;
    Vector3 direction;
    [SerializeField] float rotationSpeed;
    [SerializeField] LaserProjectile laserPrefab;
    [SerializeField] float timeBetweenBurst;
    float timeBetweenBurstDelta;

    [SerializeField] int burstAmount;
    [SerializeField] int burstCount;
    [Range(0.1f,2f)][SerializeField] float deltaTweenLaser;
    LaserProjectile[] laser = new LaserProjectile[3];

    void Start()
    {
        position= gameObject.transform.position;
        timeBetweenBurstDelta=timeBetweenBurst;
        laser= new LaserProjectile[burstAmount];
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenBurstDelta-= Time.deltaTime;

        direction = objetive.position-position;
        transform.right=Vector2.MoveTowards(transform.right, direction, rotationSpeed*Time.deltaTime);

        if (timeBetweenBurstDelta < 0){
            StartCoroutine(nameof(Shooting),burstCount);
                
            if (burstCount>burstAmount){
            burstCount=0; //not use...
            }
            timeBetweenBurstDelta=timeBetweenBurst;
        }
    }

    private IEnumerator Shooting(int pos){
        burstCount++; //Not use...

        for (int i=0;i<burstAmount;i++) {
            laser[i]=Instantiate(laserPrefab, shootPos.position, Quaternion.identity);
            laser[i].ShootLaser(transform.right);
            yield return new WaitForSeconds(deltaTweenLaser);
        }
    } 
}
