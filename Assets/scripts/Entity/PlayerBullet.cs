using Assets.scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBullet : MonoBehaviour
{
    private float bulletSpeed = 30f;

    [SerializeField, FormerlySerializedAs("mainTimer")]
    private Timer lifeTimer;
    void Start()
    {
        lifeTimer.StartTimer(1.0f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "missileObstacle")
        {
            Destroy(lifeTimer);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition+=Vector3.right*Time.deltaTime*bulletSpeed;
        if (lifeTimer.IsEnded)
        {
            Destroy(lifeTimer);
            Destroy(gameObject);
        }
    }
}
