using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    private GameObject player;
    private GameObject followCamera;
    // Start is called before the first frame update
    void Awake()
    {
        int indexPlayer = PlayerPrefs.GetInt("PlayerIndex");
        Instantiate(CharacterManager.Instance.characters[indexPlayer].playableCharacter, transform.position,Quaternion.identity);
        player = GameObject.FindWithTag("Player");
        followCamera = GameObject.Find("CameraFollowObject");
        //cineMachineCamera = GameObject.Find("Virtual Camera 1");
       // cineMachineCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.GetComponent<Transform>();
    }

    private void Update()
    {
        followCamera.transform.position = player.transform.position;
        
    }

}
