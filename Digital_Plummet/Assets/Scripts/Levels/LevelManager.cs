using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelManager : MonoBehaviour
{
    string levelName;
    int status;
    public List<GameObject> levelButton;
    public List<Levels> levels;
    [SerializeField]  FadesAnimation animatedPanel;
    
    // Start is called before the first frame update
    public void LevelStatus()
    {
        for (int i = 0; i < levelButton.Count; i++)
        {
           levels[i].status = PlayerPrefs.GetInt(levels[i].levelName);
            Debug.Log(PlayerPrefs.GetInt(levels[i].levelName, levels[i].status));
            if (levels[i].status == 1)
            {
                levelButton[i].gameObject.SetActive(true);
            }
            else
            {
                levelButton[i].gameObject.SetActive(false);
            }
        }
        
    }
    public void SelectLevel(int level)
    {
        if (animatedPanel == null)
        {
            animatedPanel = FindObjectOfType<FadesAnimation>();
        }
        PlayerPrefsManager.Instance.SetPlayerPref("SelectedLevel", level);
       // PlayerPrefs.SetInt("SelectedLevel", level);
        animatedPanel.LoadIntScene(level);
        //SceneManager.LoadScene(level);
    }
}
