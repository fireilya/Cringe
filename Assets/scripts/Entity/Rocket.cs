using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Timer = Assets.scripts.Timer;

public class Rocket : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("lifeTimer")]
    private Timer lifeTimer;
    public GameObject Engine;
    private float timer;

    [SerializeField]
    private GameObject explosionRadius;

    private Rigidbody2D rb;
    private bool isRocketLaunced;
    private float boost = 15.0f;
    private AudioSource rocketAudioSource;



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "missileObstacle" && tag=="rocket")
        {
            Instantiate(explosionRadius, transform.localPosition, Quaternion.identity);
            Destroy(lifeTimer);
            Destroy(gameObject);
        }

    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rocketAudioSource=GetComponent<AudioSource>();
        //Engine=transform.GetChild(0).gameObject;
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
            rb.AddForce(Vector2.right * boost);
        }
    }
}
