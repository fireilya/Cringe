using System;
using Assets.scripts;
using Assets.scripts.service;
using UnityEngine;
using UnityEngine.Serialization;

public class ConstantSpeedRocket : MonoBehaviour
{
    [SerializeField]
    private Spawner explodeSpawner;

    [SerializeField]
    private Timer lifeTimer;
    [SerializeField] 
    private float lifeTime;
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private PostAudioSource explodeAudioSource;

    [SerializeField]
    private MissileData data;


    private float health = 25;


    private void Start()
    {
        
        lifeTimer.StartTimer(lifeTime);
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
    }

}