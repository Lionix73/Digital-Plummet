using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] string collectibleID; // Unique ID for each collectible
    [SerializeField] AudioClip collectSound;
    [SerializeField] GameObject collectEffect;
    private UIManager uIManager;

    void Start()
    {
        // Check if this collectible has already been collected
        if (PlayerPrefs.GetInt(collectibleID, 0) == 1)
        {
            // If collected, disable or destroy the collectible
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();

            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            if (collectEffect != null)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }

            // Mark the collectible as collected
            PlayerPrefs.SetInt(collectibleID, 1);
            PlayerPrefs.Save();  // Save the changes

            // Remove the collectible from the scene
            gameObject.SetActive(false);
        }
    }

    private void Collect()
    {
        // Add any effects on the player like increasing score, health, etc.
        CharacterManager playerScore = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        playerScore.AddScore(1); // Add score or other effect
    }
}
