using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCharacter", menuName = "Character")]
public class Characters : ScriptableObject
{
    public GameObject playableCharacter;
    public Sprite unlockedSprite;
    public Sprite lockedSprite;
    public string name;
    public int cost;
    public bool unlocked;
}
