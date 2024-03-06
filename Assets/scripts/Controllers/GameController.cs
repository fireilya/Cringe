using Assets.scripts.service;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private AttackController attackController;

    private int bossState;
    private int currentLife;

    [SerializeField]
    private HealthManager healthManager;

    [SerializeField]
    private Morgen mainEnemy;

    [SerializeField]
    private MoralPressureController moralPressureController;

    [SerializeField]
    private Player player;

    [SerializeField]
    private RocketController rocketController;

    [SerializeField]
    private StateController stateController;

    [SerializeField]
    private UIController uiController;

    public int playerHealthAmount { get; private set; } = 3;

    public void ChangeStateIfNeed(int health)
    {
        var newState = stateController.CheckStateByHealth(health);
        if (newState == bossState) return;
        bossState = newState;
        stateController.SetTransitionAttack(newState);
    }

    private void DestroyScene()
    {
        foreach (var GO in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (GO.name is "Service" or "UI") continue;
            Destroy(GO);
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        uiController.Pause();
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

    private void Start()
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
        PopovEnter.ClearQueue();
        Cursor.lockState = CursorLockMode.Locked;
        UnpackData();
        Time.timeScale = 1;
        rocketController.Reload();
        player.gameObject.SetActive(true);
        attackController.AllowFirstAttack();
        playerHealthAmount = Config.commonPlayerHealth;
        player.transform.position = Vector3.zero;
        stateController.SetTransitionAttack(bossState);
        healthManager.UpdateHealth(playerHealthAmount);
    }

    public void SetMegaHealth()
    {
        ResetHealth(Config.MegaPlayerHealth);
    }

    private void ResetHealth(int health)
    {
        playerHealthAmount = health;
        healthManager.UpdateHealth(playerHealthAmount);
    }

    public void ResetHealth()
    {
        playerHealthAmount = playerHealthAmount < Config.commonPlayerHealth
            ? Config.commonPlayerHealth
            : playerHealthAmount;
        healthManager.UpdateHealth(playerHealthAmount);
    }
}