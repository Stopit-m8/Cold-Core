using System.Collections;  // Make sure to include this for Coroutine
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenuController : MonoBehaviour
{
    public GameObject deathMenuUI;  // Reference to the Death Menu UI
    public Button retryButton;      // Reference to the Retry Button
    public Button exitButton;       // Reference to the Exit Button

    private void Start()
    {
        // Initially hide the death menu UI
        deathMenuUI.SetActive(false);

        // Add listeners to buttons
        retryButton.onClick.AddListener(Retry);
        exitButton.onClick.AddListener(ExitToMenu);
    }

    // This method is called from the Player_Health script when the player dies
    public void ShowDeathMenu()
    {
        // Start the coroutine to show the death menu with a delay
        StartCoroutine(ShowDeathMenuAfterDelay(2f));  // 2 seconds delay
    }

    // Coroutine to show death menu after delay
    private IEnumerator ShowDeathMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay time
        deathMenuUI.SetActive(true);  // Show the Death Menu after delay
        Time.timeScale = 0f;  // Pause the game (you can remove this if you don't want to pause)
    }

    // Retry: Reload the current scene
    void Retry()
    {
        Time.timeScale = 1f;  // Ensure game time resumes when retrying
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
    }

    // Exit to Level Menu: Load scene 3 (Level Menu)
    void ExitToMenu()
    {
        Time.timeScale = 1f;  // Ensure game time is resumed
        SceneManager.LoadScene(3);  // Load the level menu scene (scene index 3)
    }
}
