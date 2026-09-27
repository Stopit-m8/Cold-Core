using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    private bool isStopped = false; // Flag to indicate if the item has stopped

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the item collides with the Player
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the item when it touches the player
        }
        
        // Check if the item collides with the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Stop the item by disabling its physics interactions
            if (!isStopped)
            {
                isStopped = true;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Stop any movement
                GetComponent<Rigidbody2D>().isKinematic = true; // Disable physics so it doesn't fall
            }
        }
    }
}
