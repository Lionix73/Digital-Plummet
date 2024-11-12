using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using UnityEngine.TextCore.Text;

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
    [SerializeField] private GameObject coinImage;
    [SerializeField] private AudioSource[] CharacterSounds;

    private CharacterManager characterManager;
    private int indexTutorial;
    AudioSource sound;
    private void Start()
    {
        characterManager = CharacterManager.Instance;
        SelectedCharacter();
        sound = GetComponent<AudioSource>();
    }

    public void OpenMenu()
    {
        coins = PlayerPrefs.GetInt("TotalCoins");
        index = PlayerPrefs.GetInt("PlayerIndex");
        PlayerPrefs.SetInt(characterManager.characters[0].id, 1);
        ArrayOfSkins();
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
        if (characterManager.characters[index].unlocked == 1)
        {
            PlayerPrefsManager.Instance.SetPlayerPref("PlayerIndex", index);
            //PlayerPrefs.SetInt("PlayerIndex", index);
            selectedIndex = index;
            selectionButton.text = "Selected";
            characterMainMenu.sprite = characterManager.characters[index].unlockedSprite;
            CharacterSounds[0].Play();
        }
        //Buy phase
        else if (characterManager.characters[index].unlocked == 0 && coins >= characterManager.characters[index].cost)
        {
            int spriteCost = characterManager.characters[index].cost;
           // PlayerPrefs.SetInt(characterManager.characters[index].id, 1);
            PlayerPrefsManager.Instance.SetPlayerPref(characterManager.characters[index].id, 1);
            characterManager.characters[index].unlocked = PlayerPrefs.GetInt(characterManager.characters[index].id);
            selectionButton.text = "Select";
            coinImage.SetActive(false);
            name.text = characterManager.characters[index].name;
            mainCharacter.sprite = characterManager.characters[index].unlockedSprite;
            selectedCharacter.sprite = characterManager.characters[index].unlockedSprite;
            int remainingCoins = coins - spriteCost;
            PlayerPrefsManager.Instance.SetPlayerPref("TotalCoins", remainingCoins);
            PlayerPrefs.SetInt("TotalCoins", remainingCoins);
            Debug.Log("Total coins" + PlayerPrefs.GetInt("TotalCoins"));
            characterManager.DisplayScore();
            CharacterSounds[1].Play();
        }
        else
        {
            CharacterSounds[2].Play();
        }
    }
    public void SelectedCharacter()
    {
        characterMainMenu.sprite = characterManager.characters[PlayerPrefs.GetInt("PlayerIndex")].unlockedSprite;

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
            coinImage.SetActive(false);
            selectionButton.text = "Selected";
        }
        else if(characterManager.characters[index].unlocked == 1)
        {
            coinImage.SetActive(false);
            selectionButton.text = "Select";
        }
        else if(characterManager.characters[index].unlocked == 0)
        {
            coinImage.SetActive(true);
            selectionButton.text = (characterManager.characters[index].cost).ToString();
        }
    }
    public void ArrayOfSkins()
    {
        for (int i = 1; i < characterManager.characters.Count; i++)
        {
            characterManager.characters[i].unlocked = PlayerPrefs.GetInt(characterManager.characters[i].id);
        }
    }
    public void DisplayCharacters()
    {
        
        
        if (characterManager.characters[nextIndex].unlocked == 1)
        {
            nextCharacter.sprite = characterManager.characters[nextIndex].unlockedSprite;

        }
        else
        {
            nextCharacter.sprite = characterManager.characters[nextIndex].lockedSprite;
        }

        if (characterManager.characters[index].unlocked == 1)
        {
            name.text = characterManager.characters[index].name;
            mainCharacter.sprite = characterManager.characters[index].unlockedSprite;
            selectedCharacter.sprite = characterManager.characters[index].unlockedSprite;
            coinImage.SetActive(false);
        }
        else
        {
            name.text = "???";
            mainCharacter.sprite = characterManager.characters[index].lockedSprite;
            selectedCharacter.sprite = characterManager.characters[index].lockedSprite;
            coinImage.SetActive(true);
        }
        if (characterManager.characters[previousIndex].unlocked == 1)
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
        else if(PlayerPrefs.GetInt("SelectedLevel") == 0 && indexTutorial == 1)
        {
            
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SelectedLevel"));
        }
        
    } 
}
