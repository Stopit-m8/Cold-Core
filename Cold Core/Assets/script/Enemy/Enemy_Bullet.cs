using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] private int Force;
    [SerializeField] private float DTime;
    [SerializeField] private int Damage;
    private Rigidbody2D rb;
    private float Timer;

    public Animator animator; // Reference to the bullet's Animator

    private bool hasExploded = false;  // Track whether the bullet has exploded

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only trigger explosion once
        if (!hasExploded)
        {
            // Check if the bullet hits the player or something with the "Ground" layer
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Explode();  // Trigger explosion animation and stop the bullet
                hasExploded = true;  // Prevent multiple explosions
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Force;  // Set bullet velocity
    }

    void Update()
    {
        // Timer to destroy the bullet if it doesn’t hit anything within the given time
        Timer += Time.deltaTime;
        if (Timer > DTime && !hasExploded)
        {
            Destroy(gameObject);  // Destroy the bullet if no collision occurred
            Timer = 0;
        }
    }

    // Function to trigger the explosion animation
    private void Explode()
    {
        if (animator != null)
        {
            animator.SetBool("IsExploding", true);  // Trigger the explosion animation via the IsExploding boolean parameter
        }

        // Stop the bullet's movement immediately
        rb.velocity = Vector2.zero;

        // Destroy the bullet after the explosion animation finishes playing
        Destroy(gameObject, 0.8f);  // Adjust this delay based on the length of your explosion animation
    }
}
