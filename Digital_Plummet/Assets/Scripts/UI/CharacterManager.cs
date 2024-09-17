using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    public List<Characters> characters;
    private int score;
    private int totalScore;

    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        score = PlayerPrefs.GetInt("TotalScore");
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
        score = PlayerPrefs.GetInt("TotalScore");
        score += amount;
        PlayerPrefs.SetInt("TotalScore", score);
        Debug.Log("Score: " + score);
        DisplayScore();
        
    }

    public void DisplayScore()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
    }

}

