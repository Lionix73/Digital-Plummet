using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] PlayerControllerV2 player;
    int sceneIndex;

    [SerializeField] float timeLeft;
    [SerializeField] float timer;
    [SerializeField]float timeRecord;
    [SerializeField] int timerOn = 0;
    [SerializeField] UIManager pausePanel;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI activeLevel;
    [SerializeField] TextMeshProUGUI timerPanel;
    [SerializeField] TextMeshProUGUI recordPanel;
    

    [Header("Level Variables")]
    [Tooltip("The NAME of the Next Level (Is a String). If Null it gets you back to the Menu.")]
    [SerializeField] private string nextLevel;
    private int indexTutorial = 0;
    public List<Levels> levels;
    PlayerPrefsManager playerPrefsManager;
    [SerializeField] FadesAnimation animatedPanel;
    private UIManager uiManager;
    [SerializeField] GameObject nextLevelPanel;

    [Header("Alarm Variables")]
    [SerializeField] private GameObject alarm;

    private AudioSource audioSource;
    private AudioClip countdownClip;

    private void Awake()
    {
        nextLevelPanel = GameObject.Find("NextLevelPanel");
        uiManager = nextLevelPanel.GetComponent<UIManager>();
    }
    void Start()
    {
        gameObject.AddComponent<AudioSource>();

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.priority = 10;

        countdownClip = Resources.Load<AudioClip>("alarma");
        audioSource.clip = countdownClip;

        indexTutorial = PlayerPrefs.GetInt("IndexTutorial");
        Debug.Log("Index tutorial: " + indexTutorial);
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        timerOn = PlayerPrefs.GetInt("GameMode");
        Debug.Log("Timer: " + timerOn);
        if (sceneIndex >= 2)
        {
            timeRecord = PlayerPrefs.GetFloat(levels[sceneIndex - 2].levelRecord);
            timeLeft = levels[sceneIndex - 2].timerLevel;

        }
        player = FindObjectOfType<PlayerControllerV2>();
        timer = 0;
        if (timerOn == 0 || indexTutorial == 0)
        {
            Debug.Log("Timer desactivado");
            timerText.text = "";
        }

        if (nextLevel == null)
        {
            nextLevel = "Main_Menu";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player.Life > 0 && timerOn == 0 && indexTutorial == 1)
        {
            timer += Time.deltaTime;
            UpdateTimer(timer, timerText);
        }
        else if (player.Life > 0 && timerOn == 1 && indexTutorial ==1)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimer(timeLeft, timerText);
        }

        if(timeLeft <= 0 && timerOn == 1)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        if (timeLeft <= 10f && !audioSource.isPlaying && timerOn == 1)
        {
            audioSource.Play();
        }

        if (timeLeft <= 10 && timerOn == 1)
        {
            alarm.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (animatedPanel == null)
        {
            animatedPanel = FindObjectOfType<FadesAnimation>();
        }

        if (other.CompareTag("Player"))
        {
            if (indexTutorial == 1 /*&& sceneIndex < 4*/)
            {
                pausePanel.PauseGame();
                timerText.text = "";
                UpdateTimer(timeLeft, timerPanel);
                uiManager.PanelFadeIn();
                GetActiveLevel();
                Debug.Log("Nivel Terminado");
            }
            /*if (sceneIndex == 4)
            {
                animatedPanel.LoadIntScene(5);
            }*/
            else if(indexTutorial == 0 )
            {

                PlayerPrefsManager.Instance.SetPlayerPref("IndexTutorial", 1);
                animatedPanel.LoadIntScene(2);
                //PlayerPrefs.SetInt("IndexTutorial", 1);
            }
        }
    }

    private void GetActiveLevel()
    {

        if ((timer < timeRecord || timeRecord == 0) && timerOn == 0)
        {
            PlayerPrefs.SetFloat(levels[sceneIndex -2].levelRecord, timer);
            timerPanel.text = "NEW RECORD";
            recordPanel.text = GetTimer(timer);
            //recordPanel.text = timeLeft.ToString();
        }
        else if (timer > timeRecord && timerOn == 0)
        {
            timerPanel.text = "Time: " + GetTimer(timer);
            recordPanel.text = "Record: " + GetTimer(timeRecord);
            //timerPanel.text = "Time: " + timeLeft.ToString();
            //recordPanel.text = "Record: " + timeRecord.ToString();
        }
        else if (timerOn == 1)
        {
            audioSource.mute = true;
            timerPanel.text = "";
            recordPanel.text = "";
        }
        //activeLevel.text = levels[sceneIndex].levelName + "COMPLETE";
        PlayerPrefs.Save();
    }
    private void UpdateTimer(float currentTime, TextMeshProUGUI textVisual)
    {
        //currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        textVisual.text = string.Format("{00}:{1:00}", minutes, seconds);
    }

    private string GetTimer(float currentTime)
    {
        //currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        return string.Format("{00} : {1:00}", minutes, seconds);
    }

    public void ChangeLevel()
    {
        animatedPanel.LoadNameScene(nextLevel);
    }
}
