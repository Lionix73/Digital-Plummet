using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerMode : MonoBehaviour
{
    int numTimerMode;
    [SerializeField] TextMeshProUGUI mode;

    // Start is called before the first frame update
    void Start()
    {
        numTimerMode = PlayerPrefs.GetInt("GameMode");
        if (numTimerMode == 0)
        {
            mode.text = "Normal";
        }
        else
        {
            mode.text = "Timer";
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
            mode.text = "Timer";
            PlayerPrefs.SetInt("GameMode", 1);
        }
        else
        {
            mode.text = "Normal";
            PlayerPrefs.SetInt("GameMode", 0);
        }

    }
}
