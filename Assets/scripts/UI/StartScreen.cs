using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : UIController
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}