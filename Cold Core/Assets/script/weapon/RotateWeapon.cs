using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBeweenFire;

    // Reference to the player's facing direction
    public EXPPlayer_Movement playerMovementScript;

    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        originalScale = transform.localScale;  // Store the original scale of the weapon
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction of the mouse relative to the weapon
        Vector3 rotation = mousePos - transform.position;

        // Find the angle to rotate the weapon
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        // Apply rotation to the weapon, keeping it pointing at the mouse
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        // Handle firing
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBeweenFire)
            {
                canFire = true;
                timer = 0;
            }
        }

        // Fire bullet if mouse button is held
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }

        // Flip the weapon based on the player's facing direction
        FlipWeapon();
    }

    void FlipWeapon()
    {
        // Use the player's facing direction to flip the weapon
        if (playerMovementScript.isFacingRight)
        {
            // Player is facing right, no flip needed
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            // Player is facing left, flip the weapon
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
