using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(3);
    }

    public void lv1()
    {
        SceneManager.LoadScene(1);
    }
    public void lv2()
    {
        SceneManager.LoadScene(1);
    }
    public void lv3()
    {
        SceneManager.LoadScene(1);
    }
    public void uhoh()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
