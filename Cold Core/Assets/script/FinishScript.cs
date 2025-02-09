using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // To manage scene loading

public class FinishScript : MonoBehaviour
{
    public GameObject victoryMenuUI;  // Reference to the Victory Menu UI

    [SerializeField] private AudioManager audioManager;
    [SerializeField] AudioSource musicSource;

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

    void Start()
    {
        // Make sure the victory menu is initially inactive
        victoryMenuUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            musicSource.Stop();
            audioManager.PlaySFX(audioManager.Victory);  // Play victory sound effect
            victoryMenuUI.SetActive(true);  // Show the Victory Menu
            Time.timeScale = 0f;  // Pause the game (optional, can remove if you don't want to pause)

            // Stop the background music
            
        }
    }
}
