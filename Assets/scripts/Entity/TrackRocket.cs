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

    private readonly float moveSpeed = 0f;

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

        currentRotation = (currentRotation < 0 ? 360 + currentRotation : currentRotation) % 360;
        neededRotation = VectorWorker.FindRotationByTarget(transform.position, playerTransform.position);
        transform.localPosition+= transform.right*moveSpeed * Time.deltaTime;
        var variant1 = currentRotation - neededRotation;
        var variant2 = 360 - Math.Abs(currentRotation - neededRotation);
        var direction =
            Math.Abs(variant1) < Math.Abs(variant2) && currentRotation - neededRotation > 0
            || Math.Abs(variant1) > Math.Abs(variant2) && currentRotation - neededRotation < 0
                ? -1
                : 1;
        currentRotation += rotationSpeed * Time.deltaTime * direction;
        currentRotation = neededRotation < currentRotation
            ? Mathf.Clamp(currentRotation, neededRotation, currentRotation)
            : Mathf.Clamp(currentRotation, currentRotation, neededRotation);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentRotation + 180f);
    }

}