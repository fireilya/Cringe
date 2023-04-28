using System.Collections;
using System.Collections.Generic;
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


    public void Hit()
    {
        playerHealthAmount--;
        healthManager.Hit();
        StartCoroutine(moralPressureController.DoMoralPressure());
        if (playerHealthAmount != 0) return;
        player.StopAllCoroutines();
        attackController.LoseGame();
        player.gameObject.SetActive(false);
        uiController.Lose(currentLife);
        currentLife++;
        GameDataContainer.PackContainer(bossState, currentLife);
    }

    void Start()
    {
        StartGame();
    }

    public void UnpackData()
    {
        bossState = GameDataContainer.BossState;
        currentLife = GameDataContainer.PlayerCurrentLife;
    }

    public void StartGame()
    {
        UnpackData();
        rocketController.Reload();
        player.gameObject.SetActive(true);
        attackController.AllowAttack(false);
        playerHealthAmount = 3;
        player.transform.position = Vector3.zero;
        mainEnemy.ResetCurrentState(bossState);
        attackController.AllowAttack(false);
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
