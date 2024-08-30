using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectionCharacterMenu : MonoBehaviour
{
    private int index;
    private int nextIndex;
    private int previousIndex;
    [SerializeField] private string scene;
    [SerializeField] private Image mainCharacter;
    [SerializeField] private Image nextCharacter;
    [SerializeField] private Image selectedCharacter;
    [SerializeField] private Image previousCharacter;
    [SerializeField] private TextMeshProUGUI name;
    private CharacterManager characterManager;

    private void Start()
    {
        characterManager = CharacterManager.Instance;

        index = PlayerPrefs.GetInt("PlayerIndex");

        if (index > characterManager.characters.Count - 1)
        {
            index = 0;
        }

        if (index == characterManager.characters.Count - 1)
        {
            previousIndex = index - 1;
            nextIndex = 0;
        }
        if(index == 0)
        {
            previousIndex = characterManager.characters.Count - 1;
            nextIndex = nextIndex + 1;
        }
        else
        {
            previousIndex = index - 1;
            nextIndex = index + 1;
        }
        ChangeScreen();
    }

    private void ChangeScreen()
    {
        
        PlayerPrefs.SetInt("PlayerIndex", index);
        Debug.Log("PlayerIndex: " + PlayerPrefs.GetInt("PlayerIndex"));
        mainCharacter.sprite = characterManager.characters[index].image;
        name.text = characterManager.characters[index].name;
        nextCharacter.sprite = characterManager.characters[nextIndex].image;
        selectedCharacter.sprite = characterManager.characters[index].image;
        previousCharacter.sprite = characterManager.characters[previousIndex].image;
    }

    public void NextCharacter()
    {
        if(index == characterManager.characters.Count - 1)
        {
            index = 0;
            previousIndex = characterManager.characters.Count - 1;
            nextIndex = index + 1;

        }
        else
        {
            index++;
            nextIndex = index + 1;
            if (index == characterManager.characters.Count - 1)
            {
                nextIndex = 0;
            }
            previousIndex = index - 1;
            if(index == 0)
            {
                previousIndex = characterManager.characters.Count - 1;
            }
        }
        ChangeScreen();
    }
    public void PreviousCharacter()
    {
        if (index == 0)
        {
            index = characterManager.characters.Count - 1;
            nextIndex = 0;
            previousIndex = index - 1;
        }
        else
        {
            index--;
            nextIndex = index + 1;
            if (index == characterManager.characters.Count - 1)
            {
                nextIndex = 0;
            }
            previousIndex = index - 1;
            if (index == 0)
            {
                previousIndex = characterManager.characters.Count - 1;
            }
        }
        ChangeScreen() ;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(scene);
    }
}
