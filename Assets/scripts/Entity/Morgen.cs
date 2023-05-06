using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Morgen : MonoBehaviour
{
    protected int currentHealth;
    private int maxHealth = 2000;
    private float fillSpeed;
    private bool isBuilding;
    [SerializeField]
    protected Image healthBar;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private MissileData misileData;


    protected int fakemaxHealth = 1000;
    protected int healthOffset;
    private bool _isHittable;

    void Awake()
    {
        currentHealth = maxHealth;
    }
    void Start()
    {
        healthOffset = 1000;
        Debug.Log(((float)currentHealth - healthOffset) / fakemaxHealth);
        _isHittable=true;
    }

    public void ChangeMorgenHittableState(bool isHittable)
    {
        _isHittable=isHittable;
        healthBar.color=isHittable?Color.red : Color.yellow;
    }

    public void StartBuilding(float buildTime)
    {
        fillSpeed = (((float)currentHealth-healthOffset)/fakemaxHealth) / buildTime;
        isBuilding=true;
        _isHittable=false;
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
                break;
            case 2:
                healthOffset = 0;
                currentHealth = 1000;
                break;
        }
        healthBar.fillAmount = ((float)currentHealth - healthOffset) / fakemaxHealth;
        Debug.Log(currentHealth);
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        gameController.ChangeStateIfNeed(currentHealth);
        if (currentHealth<=0)
        {
            gameController.Win();
        }
        healthBar.fillAmount = ((float)currentHealth - fakemaxHealth) / fakemaxHealth;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (_isHittable)
        {
            if (misileData.DamageData.ContainsKey(collider.tag) && collider.tag!= "ExplosionRadius")
            {
                Hit(misileData.DamageData[collider.tag]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding)
        {
            var neededFill= ((float)currentHealth - healthOffset) / fakemaxHealth;
            healthBar.fillAmount += fillSpeed*Time.deltaTime;
            healthBar.fillAmount = Mathf.Clamp(healthBar.fillAmount, 0,neededFill);
            _isHittable = Math.Abs(healthBar.fillAmount - neededFill) < 1e-3;
            isBuilding = !_isHittable;
        }
    }
}
