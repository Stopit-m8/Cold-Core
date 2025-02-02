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

        // Subscribe to the sceneLoaded event to unpause after scene reload
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // This method is called from the Player_Health script when the player dies
    public void ShowDeathMenu()
    {
        deathMenuUI.SetActive(true);  // Show the Death Menu
        Time.timeScale = 0f;  // Pause the game when the death menu is shown
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

    // This method is called after the scene reloads
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset Time.timeScale to 1 to unpause the game
        Time.timeScale = 1f;

        // Optionally reset other things (e.g., Animator or health) if needed
        // ResetAnimator(); // If you have any reset logic for your player

        // You can also remove the listener here if you only need it once
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
