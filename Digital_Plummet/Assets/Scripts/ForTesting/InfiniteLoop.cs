using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method is called when another collider enters the trigger collider attached to the object where this script is attached
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Get the player's current position
            Vector3 playerPosition = other.transform.position;

            // Set the player's Y position to 0 while keeping the X position the same
            playerPosition.y = 0;

            // Apply the new position to the player
            other.transform.position = playerPosition;
        }
    }
}
