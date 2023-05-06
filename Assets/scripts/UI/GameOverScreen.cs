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