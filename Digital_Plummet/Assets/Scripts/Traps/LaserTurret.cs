using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    [Header("Turret aspects")]
    [Tooltip("Where the bullets spawn")]
    [SerializeField] Transform shootPos;
    [Tooltip("Where the bullets go")]
    [SerializeField] Transform objetive;
    Vector3 position;
    Vector3 direction;
    [SerializeField] float rotationSpeed;

    [Tooltip("Prefab used for the bullets")]
    [SerializeField] LaserProjectile laserPrefab;

    [Header("Bullets aspects")]
    [Tooltip("Time for the next burst of bullets to be fired")]
    [SerializeField] float timeBetweenBurst;
    float timeBetweenBurstDelta;

    [Tooltip("Amount of bullets of each burst")]
    [SerializeField] int burstAmount;

    [Tooltip("Delay for each bullet of the same burst")]
    [Range(0.1f,2f)][SerializeField] float deltaTweenLaser;
    LaserProjectile[] laser = new LaserProjectile[3];

    AudioSource sound;

    void Start()
    {
        position= gameObject.transform.position;
        timeBetweenBurstDelta=timeBetweenBurst;
        laser= new LaserProjectile[burstAmount];

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenBurstDelta-= Time.deltaTime;

        direction = objetive.position-position;
        transform.right=Vector2.MoveTowards(transform.right, direction, rotationSpeed*Time.deltaTime);

        if (timeBetweenBurstDelta < 0){
            StartCoroutine(nameof(Shooting));
            timeBetweenBurstDelta=timeBetweenBurst;
        }
    }

    private IEnumerator Shooting(){

        for (int i=0;i<burstAmount;i++) {
            sound.Play();
            laser[i]=Instantiate(laserPrefab, shootPos.position, Quaternion.identity);
            laser[i].ShootLaser(transform.right);
            yield return new WaitForSeconds(deltaTweenLaser);
        }
    } 
}
