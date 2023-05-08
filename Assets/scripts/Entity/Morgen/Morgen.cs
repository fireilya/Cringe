using System;
using Assets.scripts.service;
using UnityEngine;
using UnityEngine.UI;

public class Morgen : MonoBehaviour
{
    private readonly int maxHealth = Config.MainEnemyStateHealth[0];
    protected bool _isHittable;
    protected int currentHealth;


    protected int fakemaxHealth = Config.MainEnemyFakeHealth;
    private float fillSpeed;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    protected Image healthBar;

    protected int healthOffset;
    private bool isBuilding;

    [SerializeField]
    protected MissileData misileData;

    private void OnEnable()
    {
        isBuilding = false;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        Debug.Log(((float)currentHealth - healthOffset) / fakemaxHealth);
    }

    public void ChangeMorgenHittableState(bool isHittable)
    {
        _isHittable = isHittable;
        healthBar.color = isHittable ? Color.red : Color.yellow;
    }

    public void StartBuilding(float buildTime)
    {
        fillSpeed = ((float)currentHealth - healthOffset) / fakemaxHealth / buildTime;
        healthBar.fillAmount = 0;
        isBuilding = true;
        _isHittable = false;
    }

    public void SetStateHealth(int state)
    {
        switch (state)
        {
            case 0:
                healthOffset = Config.MainEnemyStateHealthOffset[0];
                currentHealth = Config.MainEnemyStateHealth[0];
                healthBar.fillAmount = 0;
                break;
            case 1:
                healthOffset = Config.MainEnemyStateHealthOffset[1];
                currentHealth = Config.MainEnemyStateHealth[1];
                healthBar.fillAmount = ((float)currentHealth - healthOffset) / fakemaxHealth;
                break;
            case 2:
                healthOffset = Config.MainEnemyStateHealthOffset[2];
                currentHealth = Config.MainEnemyStateHealth[2];
                healthBar.fillAmount = 0;
                break;
        }

        Debug.Log(currentHealth);
    }

    public void Hit(int damage)
    {
        if (_isHittable)
        {
            currentHealth -= damage;
            if (currentHealth <= 0) gameController.Win();
            if (!isBuilding) healthBar.fillAmount = ((float)currentHealth - healthOffset) / fakemaxHealth;
            gameController.ChangeStateIfNeed(currentHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (misileData.DamageData.ContainsKey(collider.tag) && collider.tag != "ExplosionRadius")
            Hit(misileData.DamageData[collider.tag]);
    }

    private void Update()
    {
        if (isBuilding)
        {
            var neededFill = ((float)currentHealth - healthOffset) / fakemaxHealth;
            healthBar.fillAmount += fillSpeed * Time.deltaTime;
            healthBar.fillAmount = Mathf.Clamp(healthBar.fillAmount, 0, neededFill);
            isBuilding = !(Math.Abs(healthBar.fillAmount - neededFill) < 1e-3);
        }
    }
}