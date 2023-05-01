using System;
using Assets.scripts;
using UnityEngine;

public class TrackRocket : MonoBehaviour
{
    [SerializeField]
    private float currentRotation;

    [SerializeField]
    private Spawner explodeSpawner;

    [SerializeField]
    private float lastRotation;

    [SerializeField]
    private Timer lifeTimer;

    private readonly float moveSpeed = 7.5f;

    [SerializeField]
    private float neededRotation;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private PostAudioSource explodeAudioSource;

    [SerializeField]
    private MissileData data;

    private readonly float rotationSpeed = 150f;

    private float health = 30;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lifeTimer.StartTimer(5f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (data.DamageData.ContainsKey(collider.tag) && collider.tag != "ExplosionRadius")
        {
            health -= data.DamageData[collider.tag];
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (lifeTimer.IsEnded || health <= 0)
        {
            Instantiate(explodeAudioSource);
            explodeSpawner.SingleFireBurst();
            Destroy(gameObject);
        }

        SetNextRotation();
        transform.localPosition+= transform.right*moveSpeed * Time.deltaTime;
        var v1 = currentRotation - neededRotation;
        var v2 = 360 - Math.Abs(currentRotation - neededRotation);
        var direction =
            Math.Abs(v1) < Math.Abs(v2) && currentRotation - neededRotation > 0
            || Math.Abs(v1) > Math.Abs(v2) && currentRotation - neededRotation < 0
                ? -1
                : 1;
        currentRotation += rotationSpeed * Time.deltaTime * direction;
        lastRotation = currentRotation is > 360 or < 0 ? lastRotation - 360 : lastRotation;
        currentRotation = neededRotation < lastRotation
            ? Mathf.Clamp(currentRotation, neededRotation, lastRotation)
            : Mathf.Clamp(currentRotation, lastRotation, neededRotation);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotation + 180f);
    }

    private void SetNextRotation()
    {
        currentRotation = currentRotation < 0 ? 360 + currentRotation : currentRotation;
        currentRotation %= 360;
        lastRotation = currentRotation;
        var k = transform.position.x - playerTransform.position.x < 0 ? 180 : 0;
        var aimRotationAngle = -Math.Atan(
                                   (playerTransform.position.y - transform.position.y)
                                   / (transform.position.x - playerTransform.position.x))
                               * Mathf.Rad2Deg
                               + k;
        aimRotationAngle = aimRotationAngle < 0 ? 360 + aimRotationAngle : aimRotationAngle;
        neededRotation = (float)aimRotationAngle;
    }
}