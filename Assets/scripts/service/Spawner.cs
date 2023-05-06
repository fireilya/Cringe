using System.Collections;
using Assets.scripts.Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    private readonly float maxSupplyShotDeviantAngle = 20f;

    private readonly Random rnd = new();

    private readonly int supplyShotBulletAmount = 5;

    private AudioController audioController;

    [SerializeField]
    private EnemyBullet EyeBullet;

    [SerializeField]
    private EnemyBullet fireBullet;

    [SerializeField]
    private EnemyBullet LJBullet;

    [SerializeField]
    private LJRocket LJRocket;

    [SerializeField]
    private EnemyBullet negrBullet;

    [SerializeField]
    [FormerlySerializedAs("gun")]
    private GameObject source;

    [SerializeField]
    private ConstantSpeedRocket trackRocket;


    private void Start()
    {
        audioController = FindAnyObjectByType<AudioController>();
    }

    private IEnumerator DoMultyExplodeShot(
        EnemyBullet bullet,
        int shotsNumber,
        float shotsDelay,
        int bulletAmount,
        float moveSpeed,
        float rotationSpeed,
        float maxDeviantAngle,
        float lifeTime = 1.0f)
    {
        var forward = Vector2.left.Rotate(rnd.Next() % 360);
        var rotationDelta = maxDeviantAngle * 2 / (bulletAmount - 1);
        var startOffset = 0f;
        var deltaOffset = rotationDelta / 2;
        for (var i = 0; i < shotsNumber; i++)
        {
            for (var j = -maxDeviantAngle + startOffset; j <= maxDeviantAngle + startOffset; j += rotationDelta)
            {
                var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.Setup(forward.Rotate(j), moveSpeed, rotationSpeed, lifeTime);
            }

            yield return new WaitForSeconds(shotsDelay);
            startOffset += deltaOffset;
        }
    }

    private void DoExplodeShot(
        EnemyBullet bullet,
        int bulletAmount,
        float moveSpeed,
        float rotationSpeed,
        float maxDeviantAngle,
        float lifeTime = 1.0f)
    {
        var forward = Vector2.left.Rotate(rnd.Next() % 360);
        var rotationDelta = maxDeviantAngle * 2 / (bulletAmount - 1);
        for (var j = -maxDeviantAngle; j <= maxDeviantAngle; j += rotationDelta)
        {
            var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.Setup(forward.Rotate(j), moveSpeed, rotationSpeed, lifeTime);
        }
    }

    private IEnumerator DoShot(
        EnemyBullet bullet,
        float shotTime,
        int shotNumber,
        float shotsDelay,
        int bulletAmount,
        float moveSpeed,
        float rotationSpeed,
        float maxDeviantAngle,
        float lifeTime = 1.0f)
    {
        for (var i = 0; i < shotNumber; i++)
        {
            var forward = Vector2.left.Rotate(source.transform.localEulerAngles.z);
            var rotationDelta = maxDeviantAngle * 2 / (bulletAmount - 1);
            for (var j = -maxDeviantAngle; j <= maxDeviantAngle; j += rotationDelta)
            {
                var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.Setup(forward.Rotate(j), moveSpeed, rotationSpeed, lifeTime);
            }

            yield return new WaitForSeconds(shotTime);
            yield return new WaitForSeconds(shotsDelay);
        }
    }


    private IEnumerator DoShot(
        EnemyBullet bullet,
        AudioSources source,
        FXClips shotClip,
        FXClips reloadClip,
        float shotTime,
        int shotNumber,
        float shotsDelay,
        int bulletAmount,
        float moveSpeed,
        float rotationSpeed,
        float maxDeviantAngle,
        float lifeTime = 1.0f)
    {
        for (var i = 0; i < shotNumber; i++)
        {
            var forward = Vector2.left.Rotate(this.source.transform.localEulerAngles.z);
            var rotationDelta = maxDeviantAngle * 2 / (bulletAmount - 1);
            for (var j = -maxDeviantAngle; j <= maxDeviantAngle; j += rotationDelta)
            {
                var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                newBullet.Setup(forward.Rotate(j), moveSpeed, rotationSpeed, lifeTime);
            }

            audioController.Play(source, shotClip);
            yield return new WaitForSeconds(shotTime);
            audioController.Play(source, reloadClip);
            yield return new WaitForSeconds(shotsDelay);
        }
    }

    private void SpawnSingleRocket(GameObject rocket)
    {
        Instantiate(rocket, transform.position, Quaternion.Euler(0, 0, 90));
    }

    public void EyeExplode()
    {
        DoExplodeShot(
            EyeBullet,
            20,
            6f,
            200f,
            180,
            6f);
    }

    public void FireExplode()
    {
        StartCoroutine(DoMultyExplodeShot(
            fireBullet,
            6,
            0.25f,
            17,
            3f,
            250f,
            180,
            10f));
    }

    public void SingleFireBurst()
    {
        DoExplodeShot(
            fireBullet,
            17,
            3f,
            250f,
            180f,
            10f
        );
    }

    public void SpawnTrackRocket()
    {
        SpawnSingleRocket(trackRocket.gameObject);
    }

    public void SpawnLJRocket()
    {
        SpawnSingleRocket(LJRocket.gameObject);
    }

    public void LJExplode()
    {
        DoExplodeShot(
            LJBullet,
            15,
            3.5f,
            250f,
            180,
            10f);
    }

    public void CommonNegrShot(int bulletAmount)
    {
        StartCoroutine(DoShot(
            negrBullet,
            AudioSources.Gun,
            FXClips.GunShot,
            FXClips.GunReload,
            0.3f,
            1,
            0.0f,
            bulletAmount,
            6f,
            0f,
            45,
            5f
        ));
    }

    public void SupplyShot()
    {
        StartCoroutine(DoShot(
            negrBullet,
            AudioSources.Gun,
            FXClips.GunShot,
            FXClips.GunReload,
            0.3f,
            3,
            0.0f,
            5,
            9f,
            0f,
            20
        ));
    }
}