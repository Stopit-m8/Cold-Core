using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // To manage scene loading

public class FinishScript : MonoBehaviour
{
    public GameObject victoryMenuUI;  // Reference to the Victory Menu UI

    void Start()
    {
        // Make sure the victory menu is initially inactive
        victoryMenuUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Show the Victory Menu when the player reaches the finish line
            victoryMenuUI.SetActive(true);
            Time.timeScale = 0f;  // Pause the game (optional, can remove if you don't want to pause)
        }
    }
}
