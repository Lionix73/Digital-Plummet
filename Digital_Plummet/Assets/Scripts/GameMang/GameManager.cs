using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Level Variables")]
    [Tooltip("The NAME of the Next Level (Is a String). If Null it gets you back to the Menu.")]
    [SerializeField] private string nextLevel;
    private int indexTutorial = 0;
    public List<Levels> levels;
    PlayerPrefsManager playerPrefsManager;

    void Start()
    {
        if (nextLevel == null){
            nextLevel = "Main_Menu";
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            indexTutorial = PlayerPrefs.GetInt("IndexTutorial");
            if (indexTutorial == 0)
            {
                PlayerPrefsManager.Instance.SetPlayerPref("IndexTutorial", 1);
                //PlayerPrefs.SetInt("IndexTutorial", 1);
            }
            
            SceneManager.LoadScene(nextLevel);
        }
    }
}
