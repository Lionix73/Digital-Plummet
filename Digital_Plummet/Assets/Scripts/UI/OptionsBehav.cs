using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsBehav : MonoBehaviour
{
    [SerializeField] GameObject previosMenu;
    private bool audioButtonStatus;
    //[SerializeField] GameObject actualMenu;
    public List<GameObject> audioState = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        audioButtonStatus = SoundManager.Instance.IsAudioMuted;
        gameObject.SetActive(false);
    }
    public void OpenOptionsMenu()
    {
        previosMenu.SetActive(false);
        gameObject.SetActive(true);
        if (audioButtonStatus)
        {
            audioState[0].SetActive(true);
            audioState[1].SetActive(false);
        }
        else
        {
            audioState[0].SetActive(false);
            audioState[1].SetActive(true);
        }
    }
    public void CloseOptionsMenu()
    {
        previosMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ChangeOptionsMenu()
    {
        if (audioButtonStatus)
        {
            audioState[0].SetActive(true);
            audioState[1].SetActive(false);
        }
        else
        {
            audioState[0].SetActive(false);
            audioState[1].SetActive(true);
        }
    }

    public void ToggleMuteButton()
    {
        audioButtonStatus = !audioButtonStatus;
        SoundManager.Instance.ToggleMute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
