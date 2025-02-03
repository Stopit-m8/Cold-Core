using UnityEngine;
using UnityEngine.SceneManagement;  // To load scenes

public class VictoryMenuController : MonoBehaviour
{
    public void ExitToLevelMenu()
    {
        // Unpause the game and load the level menu (Scene index 2)
        Time.timeScale = 1f;  // Ensure the game is unpaused
        SceneManager.LoadScene(3);  // Load the Level Menu scene (Scene 2)
    }
}
