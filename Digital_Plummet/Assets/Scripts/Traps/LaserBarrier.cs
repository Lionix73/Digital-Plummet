using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBarrier : MonoBehaviour
{
    [Tooltip("Little wait time bewtween each bar of the same activation sequence")]
    [Range(0.1f,1.0f)][SerializeField] float timeBetweenBarriers;
    float TimeNextActivation;
    [Tooltip("Waiting time between sequences")]
    [SerializeField] float TimeNextActivationMax;
    [Tooltip("Where the barriers are gona spawn, its just for the amount and sprites, the real distance is aouto set")]
    [SerializeField] List<Transform> barrierPos;
    
    [Tooltip("The distance bewtween laser bars, calculated from the first one in the array order")]
    [SerializeField] Vector3 barrierDistance;
    [Tooltip("The size of the laser bars, needs some manual adjustments")]
    [SerializeField] Vector3 scaleOfBars;
    [Tooltip("How long the bars are activate")]
    [SerializeField] float lifeTimeLaserBars;

    [Tooltip("The prefab of the laser bars")]
    [SerializeField] LaserBar laserPrefab;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();

        for (int i = 0;i<barrierPos.Count; i++){
           barrierPos[i].position = barrierPos[0].position + barrierDistance*i;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        TimeNextActivation-= Time.deltaTime;

        if (TimeNextActivation < 1.8f && sound.isPlaying == false)
        {
            sound.Play();
        }

        if (TimeNextActivation < 0){
            StartCoroutine(nameof(ActivateBarriers));
            TimeNextActivation=TimeNextActivationMax;
        }
    }

    private IEnumerator ActivateBarriers(){
        for (int i = 0; i<barrierPos.Count; i++){
            LaserBar laserBar =Instantiate(laserPrefab, barrierPos[i].position,Quaternion.identity);
            laserBar.transform.localScale = scaleOfBars;
            laserBar.DestroyOnLifeTime(lifeTimeLaserBars);
            yield return new WaitForSeconds(timeBetweenBarriers);
        }
    }
}
