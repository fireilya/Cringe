using UnityEngine;
using UnityEngine.UI;

public class StaticEnemyShield : MonoBehaviour
{
    private int currentHealth;

    private float fallingAlpha;
    private float fillSpeed;

    [SerializeField]
    private readonly float growSpeed = 7;

    private Image healthBar;

    private bool isBuilding;

    private bool isDestroyed;

    private bool isHittable;

    [SerializeField]
    private readonly int maxHealth = 200;

    [SerializeField]
    private MissileData missileData;

    private SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (missileData.DamageData.ContainsKey(collider.tag) && collider.tag != "ExplosionRadius" && isHittable)
        {
            currentHealth -= missileData.DamageData[collider.tag];
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.Find("ShieldHealthBar").GetComponent<Image>();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void StartBuilding(float buildTime)
    {
        isHittable = false;
        fallingAlpha = 1 / buildTime;
        fillSpeed = 1 / buildTime;
        healthBar.fillAmount = 0;
        isBuilding = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isBuilding)
        {
            spriteRenderer.color += new Color(0, 0, 0, fallingAlpha * Time.deltaTime);
            healthBar.fillAmount += fillSpeed * Time.deltaTime;
            isHittable = healthBar.fillAmount >= 1;
            isBuilding = !isHittable;
            return;
        }

        if (isDestroyed)
        {
            transform.localScale = new Vector3(
                transform.localScale.x + growSpeed * Time.deltaTime,
                transform.localScale.y + growSpeed * Time.deltaTime,
                transform.localScale.z + growSpeed * Time.deltaTime);
            return;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject, 3f);
            transform.parent=null;
            isDestroyed = true;
        }
    }
}