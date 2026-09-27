using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Drone : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float force;
    private Rigidbody2D rb;
    private float bulletTimer;
    [SerializeField] private float maxBulletTime;
    [SerializeField] private int damage;

    // Reference to the animator and the explosion animation trigger
    private Animator bulletAnimator;
    private CapsuleCollider2D capsuleCollider;

   [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager == null)
        {
            Debug.LogError("AudioManager is NOT found in the scene!");
        }
        else
        {
            Debug.Log("AudioManager successfully found using FindObjectOfType!");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletAnimator = GetComponent<Animator>();  // Assuming the Animator is attached to the same object as the bullet
        capsuleCollider = GetComponent<CapsuleCollider2D>();  // Get the CapsuleCollider2D component
        
        // Attempt to find the player by looking for a PlayerController component
        player = FindObjectOfType<EXPPlayer_Movement>().gameObject;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the PlayerController script is attached to the player.");
        }
        else
        {
            // Calculate direction from the bullet to the player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Set bullet velocity towards the player
            rb.velocity = direction * force;

            // Calculate rotation to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    // Update is called once per frame
    void Update()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > maxBulletTime)
        {
            // Destroy the bullet after maxBulletTime if it didn't hit anything
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Decrease player health if the bullet hits the player
            collision.gameObject.GetComponent<Player_Health>().currentHealth -= damage;

            // Play the explosion animation
            PlayExplosion();

            // Stop bullet velocity immediately after collision
            StopBullet();

            // Destroy the bullet after the animation plays
            StartCoroutine(WaitForAnimationAndDestroy());
        }
        if (collision.gameObject.layer == 6) // Assuming layer 6 is obstacles (ground)
        {
            // Play the explosion animation when it hits an obstacle
            PlayExplosion();

            // Stop bullet velocity immediately after collision
            StopBullet();

            // Destroy the bullet after the animation plays
            StartCoroutine(WaitForAnimationAndDestroy());
        }
    }

    private void PlayExplosion()
    {
        if (bulletAnimator != null)
        {
            bulletAnimator.SetTrigger("Explode");  // Make sure you have an "Explode" trigger in your Animator
        }

        if (capsuleCollider != null)
        {
            capsuleCollider.enabled = false;  // Disable the collider to prevent further collisions
        }
    }

    private void StopBullet()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;  // Stop bullet velocity immediately
        }
    }

    private IEnumerator WaitForAnimationAndDestroy()
    {
        audioManager.PlaySFX(audioManager.Dronebulimp);
        // Wait for the animation to finish (assuming it's 1 second long, adjust as needed)
        yield return new WaitForSeconds(1f);  // Adjust this value based on the animation duration
        Destroy(gameObject);  // Destroy the bullet after the animation plays
    }
}
