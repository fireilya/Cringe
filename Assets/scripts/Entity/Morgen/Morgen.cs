using System;
using UnityEngine;
using UnityEngine.UI;

public class Morgen : MonoBehaviour
{
    private readonly int maxHealth = 2000;
    protected bool _isHittable;
    protected int currentHealth;


    protected int fakemaxHealth = 1000;
    private float fillSpeed;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    protected Image healthBar;

    protected int healthOffset;
    private bool isBuilding;

    [SerializeField]
    protected MissileData misileData;

    void OnEnable()
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
        fillSpeed = (((float)currentHealth - healthOffset) / fakemaxHealth) / buildTime;
        isBuilding = true;
        _isHittable = false;
    }

    public void SetStateHealth(int state)
    {
        switch (state)
        {
            case 0:
                healthOffset = 1000;
                currentHealth = 2000;
                
                break;
            case 1:
                currentHealth = 1750;
                healthBar.fillAmount = ((float)currentHealth - healthOffset) / fakemaxHealth;
                break;
            case 2:
                healthOffset = 0;
                currentHealth = 1000;
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
            healthBar.fillAmount = ((float)currentHealth - healthOffset) / fakemaxHealth;
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