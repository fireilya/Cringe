using Assets.scripts;
using UnityEngine;
using UnityEngine.Experimental.Playables;

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
        if (collider.tag=="Popov")
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(explodeAudioSource);
        explodeSpawner.SingleFireBurst();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (lifeTimer.IsEnded || health <= 0)
        {
            Explode();
        }

        transform.localPosition += transform.right * moveSpeed * Time.deltaTime;
    }
}