using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int indexPlayer = PlayerPrefs.GetInt("PlayerIndex");
        Instantiate(CharacterManager.Instance.characters[indexPlayer].playableCharacter, transform.position,Quaternion.identity);
    }

}
