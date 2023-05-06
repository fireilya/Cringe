using UnityEngine;

public class LJRocket : MonoBehaviour
{
    private readonly float boostForce = 2.5f;

    [SerializeField]
    private MissileData data;

    [SerializeField]
    private Spawner explodeSpawner;

    private int health = 5;

    [SerializeField]
    private AudioSource postExplodeSound;

    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (data.DamageData.ContainsKey(collider.tag) && collider.tag != "ExplosionRadius")
            health -= data.DamageData[collider.tag];
    }

    private void Update()
    {
        rigidbody.AddForce(Vector2.left * boostForce);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, 0, float.PositiveInfinity),
            transform.position.y,
            transform.position.z);
        if (health <= 0 || transform.position.x == 0)
        {
            Instantiate(postExplodeSound, transform.position, Quaternion.identity);
            explodeSpawner.LJExplode();
            Destroy(gameObject);
        }
    }
}