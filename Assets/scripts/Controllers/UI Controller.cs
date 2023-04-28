using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    protected GameOverScreen gameOverScreen;
    [SerializeField]
    protected LoseScreen loseScreen;
    [SerializeField]
    protected StartScreen startScreen;
    [SerializeField]
    protected GameScreen gameScreen;

    private List<GameObject> screens=new();

    protected void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Lose(int currentLife)
    {
        gameScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
        loseScreen.Lose(currentLife);
    }

    protected void UIModeFromTo(GameObject from, GameObject to)
    {
        from.SetActive(false);
        to.SetActive(true);
    }

}
