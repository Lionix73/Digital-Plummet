using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] PlayerControllerV2 player;
    int sceneIndex;

    [SerializeField] float timeLeft;
    float timeRecord;
    [SerializeField] int timerOn = 0;

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
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControllerV2>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        timeLeft = 0;
        timerOn = 1;
        if (timerOn == 0 || indexTutorial == 0)
        {
            timerText.text = "";
        }

        if (nextLevel == null)
        {
            nextLevel = "Main_Menu";
        }
        nextLevelPanel = GameObject.Find("NextLevelPanel");
        uiManager = nextLevelPanel.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Life > 0 && timerOn == 1)
        {
            timeLeft += Time.deltaTime;
            UpdateTimer(timeLeft, timerText);
        }
        else
        {

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        timerOn = 0;
        if (animatedPanel == null)
        {
            animatedPanel = FindObjectOfType<FadesAnimation>();
        }

        if (other.CompareTag("Player"))
        {
            indexTutorial = PlayerPrefs.GetInt("IndexTutorial");
            if (indexTutorial == 0)
            {
                PlayerPrefsManager.Instance.SetPlayerPref("IndexTutorial", 1);
                animatedPanel.LoadIntScene(2);
                //PlayerPrefs.SetInt("IndexTutorial", 1);
            }
            else
            {
                timerText.text = "";
                UpdateTimer(timeLeft, timerPanel);
                uiManager.PanelFadeIn();
                GetActiveLevel();
            }
        }
    }

    private void GetActiveLevel()
    {
         
        if (sceneIndex >= 2)
        {
            PlayerPrefsManager.Instance.SetPlayerPref(levels[sceneIndex - 2].levelName, 1);
            timeRecord = PlayerPrefs.GetFloat(levels[sceneIndex - 2].levelRecord);
        }

        if (timeLeft > timeRecord)
        {
            PlayerPrefs.SetFloat(levels[sceneIndex -2].levelRecord, timeLeft);
            timerPanel.text = "NEW RECORD";
            recordPanel.text = timeLeft.ToString();
        }
        else if (timeLeft < timeRecord)
        {
            timerPanel.text = "Time: " + timeLeft.ToString();
            recordPanel.text = "Record: " + timeRecord.ToString();
        }
        activeLevel.text = levels[sceneIndex].levelName + "COMPLETE";
    }
    private void UpdateTimer(float currentTime, TextMeshProUGUI textVisual)
    {
        //currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        textVisual.text = string.Format("{00} : {1:00}", minutes, seconds);
    }

    public void ChangeLevel()
    {
        animatedPanel.LoadNameScene(nextLevel);
    }
}
