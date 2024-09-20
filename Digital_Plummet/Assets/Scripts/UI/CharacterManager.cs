using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    public List<Characters> characters;
    private int coins;
    private int totalCoins;

    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        coins = PlayerPrefs.GetInt("TotalCoins");
        DisplayScore();
        if(CharacterManager.Instance == null)
        {
            CharacterManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    
    }
    public void AddScore(int amount)
    {
        coins = PlayerPrefs.GetInt("TotalCoins");
        coins += amount;
        PlayerPrefs.SetInt("TotalCoins", coins);
        Debug.Log("Score: " + coins);
        DisplayScore(); 
        
    }

    public void DisplayScore()
    {
        coins = PlayerPrefs.GetInt("TotalCoins");
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = coins.ToString();
    }
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        DisplayScore();
    }
}

