using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col : MonoBehaviour
{
    private bool tag;
    public Tank_Patrol Tank;

    // Reference to the LOS object
    public Transform LOS;  // Drag your LOS object here in the inspector

    // Reference to the LOS's BoxCollider2D
    private BoxCollider2D losCollider;

    void Start()
    {
        // Ensure the LOS doesn't collide with Bullet objects
        IgnoreBulletCollisionsWithLOS();

        // Get the BoxCollider2D component of LOS
        losCollider = LOS.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Make the LOS object follow the tank's position
        if (LOS != null && Tank != null)
        {
            // Keep the LOS position synchronized with the tank's position
            LOS.position = Tank.transform.position;

            // Flip the LOS and its collider based on Tank's facing direction
            FlipLOSCollider();
        }
    }

    private void IgnoreBulletCollisionsWithLOS()
    {
        // Find all bullets in the scene
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        
        // Ignore collisions between the LOS collider and all bullets
        foreach (GameObject bullet in bullets)
        {
            Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
            if (bulletCollider != null && losCollider != null)
            {
                // Ignore collision between Bullet and LOS
                Physics2D.IgnoreCollision(bulletCollider, losCollider, true);
            }
        }
    }

    private void FlipLOSCollider()
    {
        // Get the Tank's local scale
        Vector3 tankScale = Tank.transform.localScale;

        // If the Tank is flipped (facing left), adjust the collider size
        if (tankScale.x < 0)
        {
            // Flip the LOS collider size horizontally by inverting the width
            losCollider.size = new Vector2(Mathf.Abs(losCollider.size.x), losCollider.size.y);
            losCollider.offset = new Vector2(-Mathf.Abs(losCollider.offset.x), losCollider.offset.y);
        }
        else
        {
            // Reset the collider size and offset for facing right
            losCollider.size = new Vector2(Mathf.Abs(losCollider.size.x), losCollider.size.y);
            losCollider.offset = new Vector2(Mathf.Abs(losCollider.offset.x), losCollider.offset.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore bullets and other non-player objects
        if (collision.gameObject.CompareTag("Bullet"))
        {
            return;  // Skip processing for bullets
        }

        // Check if the collision is with the player (i.e., detect LOS with player)
        tag = collision.gameObject.CompareTag("Player");
        if (tag)
        {
            Tank.HasLOS = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Ignore bullets and other non-player objects
        if (collision.gameObject.CompareTag("Bullet"))
        {
            return;  // Skip processing for bullets
        }

        // Check if the collision is with the player
        tag = collision.gameObject.CompareTag("Player");
        if (tag)
        {
            Tank.HasLOS = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Ignore bullets and other non-player objects
        if (collision.gameObject.CompareTag("Bullet"))
        {
            return;  // Skip processing for bullets
        }

        // Check if the collision is with the player
        tag = collision.gameObject.CompareTag("Player");
        if (tag)
        {
            Tank.HasLOS = false;
        }
    }
}
