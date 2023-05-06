using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Timer = Assets.scripts.Timer;

public class Rocket : MonoBehaviour
{
    private readonly float boost = 15.0f;

    [SerializeField]
    [FormerlySerializedAs("Engine")]
    private GameObject engine;

    [SerializeField]
    private GameObject explosionRadius;

    [SerializeField]
    private bool isDestructible;

    private bool isRocketLaunced;

    [SerializeField]
    [FormerlySerializedAs("lifeTimer")]
    private Timer lifeTimer;

    private Rigidbody2D rb;
    private AudioSource rocketAudioSource;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (tag == "rocket")
            if (collider.tag == "missileObstacle" && isDestructible
                || collider.gameObject.name is "Morgen" or "StaticEnemyShield(Clone)" or "MorgenMouth")
            {
                Instantiate(explosionRadius, transform.position, Quaternion.identity);
                Destroy(lifeTimer);
                Destroy(gameObject);
            }
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (lifeTimer.IsEnded && isRocketLaunced)
        {
            Destroy(lifeTimer);
            Destroy(gameObject);
        }
    }

    public void Launch()
    {
        engine.SetActive(true);
        rocketAudioSource.Play();
        isRocketLaunced = true;
        var poligonColider = this.AddComponent<PolygonCollider2D>();
        poligonColider.isTrigger = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        lifeTimer.StartTimer(2f);
    }

    private void FixedUpdate()
    {
        if (isRocketLaunced) rb.AddRelativeForce(Vector2.right * boost);
    }
}