using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStart : MonoBehaviour
{
    private GameObject player;
    private GameObject followCamera;
    public List<Levels> levels;
    PlayerPrefsManager playerPrefsManager;
    // Start is called before the first frame update
    void Awake()
    {
        int indexPlayer = PlayerPrefs.GetInt("PlayerIndex");
        Instantiate(CharacterManager.Instance.characters[indexPlayer].playableCharacter, transform.position,Quaternion.identity);
        player = GameObject.FindWithTag("Player");
        followCamera = GameObject.Find("CameraFollowObject");
        //cineMachineCamera = GameObject.Find("Virtual Camera 1");
        // cineMachineCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.GetComponent<Transform>();
        if (SceneManager.GetActiveScene().buildIndex >= 2)
        {
            PlayerPrefsManager.Instance.SetPlayerPref(levels[SceneManager.GetActiveScene().buildIndex - 2].levelName, 1);
        }
    }

    void Update()
    {
        followCamera.transform.position = player.transform.position;
    }

}
