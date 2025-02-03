using UnityEngine;
using UnityEngine.SceneManagement;  // For loading scenes
using UnityEngine.UI;  // For handling UI buttons

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Reference to the Pause Menu UI
    public Button resumeButton;     // Reference to the Resume Button
    public Button exitButton;       // Reference to the Exit Button
    private bool isPaused = false;  // Tracks whether the game is paused

    void Start()
    {
        // Add listeners for the buttons
        resumeButton.onClick.AddListener(Resume);
        exitButton.onClick.AddListener(ExitToMenu);
    }

    void Update()
    {
        // Toggle pause menu when pressing Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Function to pause the game and show the pause menu
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Show the pause menu UI
        Time.timeScale = 0f;  // Pause the game (time scale is 0)
        isPaused = true;
    }

    // Function to resume the game and hide the pause menu
    void Resume()
    {
        pauseMenuUI.SetActive(false);  // Hide the pause menu UI
        Time.timeScale = 1f;  // Resume the game (time scale is 1)
        isPaused = false;
    }

    // Function to exit to the Level Menu scene (Scene 3)
    void ExitToMenu()
    {
        Time.timeScale = 1f;  // Ensure the game time resumes before loading the scene
        SceneManager.LoadScene(3);  // Load the level menu scene (make sure the correct scene index is used)
    }
}
