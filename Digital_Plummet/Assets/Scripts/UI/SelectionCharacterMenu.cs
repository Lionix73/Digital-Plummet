using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionCharacterMenu : MonoBehaviour
{
    private int index;
    [SerializeField] private Image image;
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
        ChangeScreen();
    }

    private void ChangeScreen()
    {
        PlayerPrefs.SetInt("PlayerIndex", index);
        image.sprite = characterManager.characters[index].image;
        name.text = characterManager.characters[index].name;
    }

    public void NextCharacter()
    {
        if(index == characterManager.characters.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        ChangeScreen();
    }
    public void PreviousCharacter()
    {
        if (index == 0)
        {
            index = characterManager.characters.Count - 1;
        }
        else
        {
            index--;
        }
        ChangeScreen() ;
    }
}
