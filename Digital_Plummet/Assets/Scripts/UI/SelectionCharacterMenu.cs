using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class SelectionCharacterMenu : MonoBehaviour
{
    private int index;
    private int coins;
    private int selectedIndex;
    private int nextIndex;
    private int previousIndex;
    [SerializeField] private string scene;
    [SerializeField] private Image mainCharacter;
    [SerializeField] private Image nextCharacter;
    [SerializeField] private Image selectedCharacter;
    [SerializeField] private Image previousCharacter;
    [SerializeField] private Image characterMainMenu;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI selectionButton;
    private CharacterManager characterManager;
    private int indexTutorial;
    private void Start()
    {
        characterManager = CharacterManager.Instance;
        characterMainMenu.sprite = characterManager.characters[PlayerPrefs.GetInt("PlayerIndex")].unlockedSprite; 

    }

    public void OpenMenu()
    {
        coins = PlayerPrefs.GetInt("TotalCoins");
        index = PlayerPrefs.GetInt("PlayerIndex");
        selectedIndex = index;

        if (index > characterManager.characters.Count - 1)
        {
            index = 0;
        }

        ChangeScreen();
    }
    public void SelectSkin()
    {
        coins = PlayerPrefs.GetInt("TotalCoins");
        if (characterManager.characters[index].unlocked == true)
        {
            PlayerPrefs.SetInt("PlayerIndex", index);
            selectedIndex = index;
            selectionButton.text = "Selected";
            characterMainMenu.sprite = characterManager.characters[index].unlockedSprite;
        }
        //Buy phase
        else if (characterManager.characters[index].unlocked == false && coins >= characterManager.characters[index].cost)
        {
            int spriteCost = characterManager.characters[index].cost;
            characterManager.characters[index].unlocked = true;
            selectionButton.text = "Select";
            name.text = characterManager.characters[index].name;
            mainCharacter.sprite = characterManager.characters[index].unlockedSprite;
            selectedCharacter.sprite = characterManager.characters[index].unlockedSprite;
            int remainingCoins = coins - spriteCost;
            PlayerPrefs.SetInt("TotalCoins", remainingCoins);
            Debug.Log("Total coins" + PlayerPrefs.GetInt("TotalCoins"));
            characterManager.DisplayScore();
        }
    }

    private void ChangeScreen()
    {
        if (index == characterManager.characters.Count - 1)
        {
            previousIndex = index - 1;
            nextIndex = 0;
        }
        else if (index == 0)
        {
            previousIndex = characterManager.characters.Count - 1;
            nextIndex = index + 1;
        }
        else
        {
            previousIndex = index - 1;
            nextIndex = index + 1;
        }
        //PlayerPrefs.SetInt("PlayerIndex", index);
        Debug.Log("PlayerIndex: " + PlayerPrefs.GetInt("PlayerIndex"));

        DisplayCharacters();

        if (index == selectedIndex)
        {
            selectionButton.text = "Selected";
        }
        else if(characterManager.characters[index].unlocked == true)
        {
            selectionButton.text = "Select";
        }
        else if(characterManager.characters[index].unlocked == false)
        {
            selectionButton.text = "BUY";
        }
    }

    public void DisplayCharacters()
    {
        
        
        if (characterManager.characters[nextIndex].unlocked == true)
        {
            nextCharacter.sprite = characterManager.characters[nextIndex].unlockedSprite;

        }
        else
        {
            nextCharacter.sprite = characterManager.characters[nextIndex].lockedSprite;
        }

        if (characterManager.characters[index].unlocked == true)
        {
            name.text = characterManager.characters[index].name;
            mainCharacter.sprite = characterManager.characters[index].unlockedSprite;
            selectedCharacter.sprite = characterManager.characters[index].unlockedSprite;
        }
        else
        {
            name.text = "???";
            mainCharacter.sprite = characterManager.characters[index].lockedSprite;
            selectedCharacter.sprite = characterManager.characters[index].lockedSprite;
        }
        if (characterManager.characters[previousIndex].unlocked == true)
        {
            previousCharacter.sprite = characterManager.characters[previousIndex].unlockedSprite;
        }
        else
        {
            previousCharacter.sprite = characterManager.characters[previousIndex].lockedSprite;
        }
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
            previousIndex = index - 1;

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
            previousIndex = index - 1;

        }
        ChangeScreen() ;
    }
    public void StartGame()
    {
        indexTutorial = PlayerPrefs.GetInt("IndexTutorial");
        Debug.Log("Cargando escena # " + indexTutorial);
        if(indexTutorial == 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
        
    } 
}
