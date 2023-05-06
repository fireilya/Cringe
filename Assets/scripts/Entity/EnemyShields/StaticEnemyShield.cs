using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticEnemyShield : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 200;
    private int currentHealth;
    [SerializeField]
    private MissileData missileData;

    private Image healthBar;

    private float fallingAlpha;
    private float fillSpeed;

    private SpriteRenderer spriteRenderer;

    private bool isBuilding;

    private bool isHittable;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (missileData.DamageData.ContainsKey(collider.tag) && collider.tag != "ExplosionRadius" && isHittable)
        {
           currentHealth -= missileData.DamageData[collider.tag];
           healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }
    void Awake()
    {
        currentHealth = maxHealth;
    }
    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        healthBar =GameObject.Find("ShieldHealthBar").GetComponent<Image>();
    }

    public void StartBuilding(float buildTime)
    {
        isHittable = false;
        fallingAlpha = 1 / buildTime;
        fillSpeed= ((float)currentHealth / maxHealth) / buildTime;
        isBuilding=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding)
        {
            spriteRenderer.color += new Color(0, 0, 0, fallingAlpha*Time.deltaTime);
            healthBar.fillAmount += fillSpeed * Time.deltaTime;
            isHittable = healthBar.fillAmount >= 1;
            isBuilding = !isHittable;
            return;
        }
        if (currentHealth<=0)
        {
            Destroy(gameObject);
        }
    }
}
