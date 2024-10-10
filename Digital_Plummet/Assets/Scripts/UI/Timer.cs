using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class Timer : MonoBehaviour
{
    [SerializeField] PlayerControllerV2 player;
    [SerializeField] float timeLeft;
    [SerializeField] bool timerOn = false;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI activeLevel;

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
        timeLeft = 0;
        timerOn = true;
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
        if (player.Life > 0)
        {
            timeLeft += Time.deltaTime;
            UpdateTimer(timeLeft);
        }
        else
        {

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
            indexTutorial = PlayerPrefs.GetInt("IndexTutorial");
            if (indexTutorial == 0)
            {
                PlayerPrefsManager.Instance.SetPlayerPref("IndexTutorial", 1);
                //PlayerPrefs.SetInt("IndexTutorial", 1);
            }
            uiManager.PanelFadeIn();
            GetActiveLevel();
        }
    }

    private void GetActiveLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex >= 2)
        {
            PlayerPrefsManager.Instance.SetPlayerPref(levels[sceneIndex - 2].levelName, 1);
        }
        activeLevel.text = levels[sceneIndex].levelName;
    }
    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
