using Assets.scripts;
using UnityEngine;

public class ConstantSpeedRocket : MonoBehaviour
{
    [SerializeField]
    private MissileData data;

    [SerializeField]
    private PostAudioSource explodeAudioSource;

    [SerializeField]
    private Spawner explodeSpawner;


    private float health = 5;

    [SerializeField]
    private float lifeTime;

    [SerializeField]
    private Timer lifeTimer;

    [SerializeField]
    private float moveSpeed;


    private void Start()
    {
        lifeTimer.StartTimer(lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (data.DamageData.ContainsKey(collider.tag) && collider.tag != "ExplosionRadius")
            health -= data.DamageData[collider.tag];
    }

    private void Update()
    {
        if (lifeTimer.IsEnded || health <= 0)
        {
            Instantiate(explodeAudioSource);
            explodeSpawner.SingleFireBurst();
            Destroy(gameObject);
        }

        transform.localPosition += transform.right * moveSpeed * Time.deltaTime;
    }
}