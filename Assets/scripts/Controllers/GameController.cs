using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int playerHealthAmount { get; private set; } = 3;

    [SerializeField]
    private Morgen mainEnemy;

    [SerializeField]
    private Player player;

    [SerializeField]
    private UIController uiController;

    [SerializeField]
    private HealthManager healthManager;
    [SerializeField]
    private AttackController attackController;
    [SerializeField]
    private MoralPressureController moralPressureController;
    [SerializeField]
    private RocketController rocketController;
    private int bossState;
    private int currentLife;

    private void DestroyScene()
    {
        foreach (var GO in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (GO.name is "Service" or "UI")
            {
                continue;
            }
            Destroy(GO);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        uiController.Pause();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        uiController.Resume();
    }
    public void Hit()
    {
        playerHealthAmount--;
        healthManager.Hit();
        StartCoroutine(moralPressureController.DoMoralPressure());
        if (playerHealthAmount != 0) return;
        player.StopAllCoroutines();
        attackController.LoseGame();
        player.gameObject.SetActive(false);
        DestroyScene();
        uiController.Lose(currentLife);
        currentLife++;
        GameDataContainer.PackContainer(bossState, currentLife);
    }

    void Start()
    {
        StartGame();
    }

    private void UnpackData()
    {
        bossState = GameDataContainer.BossState;
        currentLife = GameDataContainer.PlayerCurrentLife;
    }

    public void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        uiController.Win();
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UnpackData();
        Time.timeScale = 1;
        rocketController.Reload();
        player.gameObject.SetActive(true);
        attackController.AllowAttack(false);
        playerHealthAmount = 3;
        player.transform.position = Vector3.zero;
        mainEnemy.ResetCurrentState(bossState);
        attackController.AllowAttack(false);
        healthManager.UpdateHealth(playerHealthAmount);
    }

    public void SetMegaHealth()
    {
        ResetHealth(5);
    }

    private void ResetHealth(int health)
    {
        playerHealthAmount = health;
        healthManager.UpdateHealth(playerHealthAmount);
    }

    public void ResetHealth()
    {
        playerHealthAmount = 3;
        healthManager.UpdateHealth(playerHealthAmount);
    }
    public void EndGame()
    {

    }
}
