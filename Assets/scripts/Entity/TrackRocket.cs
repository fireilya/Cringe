using System;
using Assets.scripts;
using Assets.scripts.service;
using UnityEngine;

public class TrackRocket : MonoBehaviour
{
    [SerializeField]
    private float currentRotation;

    [SerializeField]
    private Spawner explodeSpawner;

    [SerializeField]
    private Timer lifeTimer;

    private readonly float moveSpeed = 8f;

    [SerializeField]
    private float neededRotation;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private PostAudioSource explodeAudioSource;

    [SerializeField]
    private MissileData data;

    private readonly float rotationSpeed = 175f;

    private float health = 30;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lifeTimer.StartTimer(550f);
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
        transform.localPosition += transform.right * moveSpeed * Time.deltaTime;
        currentRotation = (currentRotation < 0 ? 360 + currentRotation : currentRotation) % 360;
        neededRotation = VectorWorker.FindRotationByTarget(transform.position, playerTransform.position);
        currentRotation =
            VectorWorker.RotateToTargetWithSpeed(currentRotation, neededRotation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotation + 180f);
    }

}