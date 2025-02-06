using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public int timerMax;
    private float timer;
    private GameObject player;
    [SerializeField] private float LOS;

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
        player = GameObject.FindGameObjectWithTag("Player");
        
        // Debugging: Check if the player was found
        if (player == null)
        {
            Debug.LogError("Player not found! Ensure the player has the 'Player' tag.");
        }
        else
        {
            Debug.Log("Player found: " + player.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return; // If player is not found, skip the update logic

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log("Distance to player: " + distance); // Debugging: Log the distance

        if (distance < LOS)
        {
            Debug.Log("Player is within LOS!"); // Debugging: Confirm LOS condition is met

            timer += Time.deltaTime;
            Debug.Log("Timer: " + timer); // Debugging: Log timer value

            if (timer > timerMax)
            {
                Debug.Log("Timer reached max, shooting..."); // Debugging: Confirm shoot condition
                shoot();
                timer = 0; // Reset timer after shooting
            }
        }
        else
        {
            Debug.Log("Player is out of LOS."); // Debugging: Log when player is out of LOS
        }
    }

    void shoot()
    {
        audioManager.PlaySFX(audioManager.DroneShoot);
        Debug.Log("Shooting bullet from: " + bulletPos.position); // Debugging: Log the bullet's spawn position
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
