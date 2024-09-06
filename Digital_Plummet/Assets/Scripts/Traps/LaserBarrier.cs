using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBarrier : MonoBehaviour
{
    [SerializeField] float timeBetweenBarriers;
    [SerializeField] float TimeNextActivation;
    [SerializeField] float TimeNextActivationMax;
    [SerializeField] List<Transform> barrierPos;
    [SerializeField] LaserBar laserPrefab;
    [SerializeField] Vector3 barrierDistance;
    [SerializeField] Vector3 scaleOfBars;
    [SerializeField] float lifeTimeLaserBars;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i<barrierPos.Count; i++){
           barrierPos[i].position = barrierPos[0].position + barrierDistance*i;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        TimeNextActivation-= Time.deltaTime;

        if (TimeNextActivation < 0){
            StartCoroutine(nameof(ActivateBarriers));
            TimeNextActivation=TimeNextActivationMax;
        }
    }

    private IEnumerator ActivateBarriers(){
        for (int i = 0; i<barrierPos.Count; i++){
            LaserBar laserBar=Instantiate(laserPrefab, barrierPos[i].position,Quaternion.identity);
            laserBar.transform.localScale = scaleOfBars;
            laserBar.DestroyOnLifeTime(lifeTimeLaserBars);
            yield return new WaitForSeconds(timeBetweenBarriers);
        }
    }
}
