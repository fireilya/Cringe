using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    protected GameOverScreen gameOverScreen;

    [SerializeField]
    protected GameScreen gameScreen;

    [SerializeField]
    protected LoseScreen loseScreen;

    [SerializeField]
    protected PauseScreen pauseScreen;

    private List<GameObject> screens = new();

    [SerializeField]
    protected StartScreen startScreen;

    [SerializeField]
    protected WinScreen winScreen;

    protected void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Win()
    {
        gameScreen.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(true);
    }

    public void Lose(int currentLife)
    {
        gameScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
        loseScreen.Lose(currentLife);
    }

    public void Resume()
    {
        pauseScreen.gameObject.SetActive(false);
        gameScreen.gameObject.SetActive(true);
    }

    public void Pause()
    {
        pauseScreen.gameObject.SetActive(true);
        gameScreen.gameObject.SetActive(false);
    }

    protected void UIModeFromTo(GameObject from, GameObject to)
    {
        from.SetActive(false);
        to.SetActive(true);
    }
}