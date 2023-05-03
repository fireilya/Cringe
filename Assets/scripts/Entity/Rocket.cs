using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Timer = Assets.scripts.Timer;

public class Rocket : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("lifeTimer")]
    private Timer lifeTimer;
    [SerializeField, FormerlySerializedAs("Engine")]
    private GameObject engine;

    [SerializeField]
    private GameObject explosionRadius;

    private Rigidbody2D rb;
    private bool isRocketLaunced;
    private float boost = 15.0f;
    private AudioSource rocketAudioSource;
    [SerializeField]
    private bool isDestructible;



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (tag=="rocket")
        {
            if (collider.tag == "missileObstacle" && isDestructible || collider.gameObject.name is "Morgen" or "MorgenMouth")
            {
                Instantiate(explosionRadius, transform.position, Quaternion.identity);
                Destroy(lifeTimer);
                Destroy(gameObject);
            }
        }

    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rocketAudioSource=GetComponent<AudioSource>();
    }

    void Update()
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
        isRocketLaunced=true;
        var poligonColider=this.AddComponent<PolygonCollider2D>();
        poligonColider.isTrigger=true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        lifeTimer.StartTimer(2f);
    }

    void FixedUpdate()
    {
        if (isRocketLaunced)
        {
            rb.AddRelativeForce(Vector2.right * boost);
        }
    }
}
