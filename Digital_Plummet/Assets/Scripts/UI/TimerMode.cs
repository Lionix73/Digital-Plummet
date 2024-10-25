using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerMode : MonoBehaviour
{
    int numTimerMode;
    [SerializeField] GameObject[] mode; 
    

    // Start is called before the first frame update
    void Start()
    {
        numTimerMode = PlayerPrefs.GetInt("GameMode");
        if (numTimerMode == 0)
        {
            mode[0].SetActive(true);
            mode[1].SetActive(false);
        }
        else
        {
            mode[0].SetActive(false);
            mode[1].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMode()
    {
         numTimerMode = PlayerPrefs.GetInt("GameMode");
        if (numTimerMode == 0 )
        {
            mode[0].SetActive(false);
            mode[1].SetActive(true);
            PlayerPrefs.SetInt("GameMode", 1);
        }
        else
        {
            mode[0].SetActive(true);
            mode[1].SetActive(false);
            PlayerPrefs.SetInt("GameMode", 0);
        }

    }
}
