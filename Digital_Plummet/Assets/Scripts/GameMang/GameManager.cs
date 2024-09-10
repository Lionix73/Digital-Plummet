using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Level Variables")]
    [Tooltip("The NAME of the Next Level (Is a String). If Null it gets you back to the Menu.")]
    [SerializeField] private string nextLevel;

    void Start()
    {
        if (nextLevel == null){
            nextLevel = "Main_Menu";
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            SceneManager.LoadScene(nextLevel);
        }
    }
}
