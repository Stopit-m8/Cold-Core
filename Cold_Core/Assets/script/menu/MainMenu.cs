using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void lv1()
    {
        SceneManager.LoadScene(2);
    }
    public void lv2()
    {
        SceneManager.LoadScene(3);
    }
    public void lv3()
    {
        SceneManager.LoadScene(4);
    }
    public void uhoh()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
