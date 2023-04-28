using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : UIController
{
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void Again()
    {
        GameDataContainer.ResetContainer();
        RestartGame();
    }
}
