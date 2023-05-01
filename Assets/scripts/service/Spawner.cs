using System.Collections;
using Assets.scripts.Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private EnemyBullet negrBullet;
    [SerializeField]
    private EnemyBullet LJBullet;
    [SerializeField]
    private EnemyBullet fireBullet;
    [SerializeField]
    private LJRocket LJRocket;

    [SerializeField]
    private TrackRocket trackRocket;

    [SerializeField, FormerlySerializedAs("gun")]    
    private GameObject source;

    private Random rnd= new();

    [SerializeField]
    private AudioController audioController;

    private readonly float maxSupplyShotDeviantAngle = 20f;

    private readonly int supplyShotBulletAmount = 5;



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
        for (int i = 0; i < shotsNumber; i++)
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
        var forward = Vector2.left.Rotate(rnd.Next()%360);
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

    public void FireExplode()
    {
        StartCoroutine(DoMultyExplodeShot(
            bullet: fireBullet,
            shotsNumber: 6,
            shotsDelay: 0.25f,
            bulletAmount: 17,
            moveSpeed: 3f,
            rotationSpeed: 250f,
            maxDeviantAngle: 180,
            lifeTime: 10f));
    }

    public void SingleFireBurst()
    {
        DoExplodeShot(
            bullet:fireBullet,
            bulletAmount:17,
            moveSpeed:3f,
            rotationSpeed:250f,
            maxDeviantAngle:180f,
            lifeTime:10f
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
            bullet: LJBullet,
            bulletAmount: 15,
            moveSpeed: 3.5f,
            rotationSpeed: 250f,
            maxDeviantAngle: 180,
            lifeTime: 10f);

    }

    public void CommonNegrShot()
    {
        StartCoroutine(DoShot(
            bullet: negrBullet,
            source: AudioSources.Gun,
            shotClip: FXClips.GunShot,
            reloadClip: FXClips.GunReload,
            shotTime: 0.3f,
            shotNumber: 1,
            shotsDelay: 0.0f,
            bulletAmount: 7,
            moveSpeed: 6f,
            rotationSpeed: 0f,
            maxDeviantAngle: 45,
            lifeTime: 5f
            ));
    }

    public void SupplyShot()
    {
        StartCoroutine(DoShot(
            bullet:negrBullet,
            source: AudioSources.Gun,
            shotClip: FXClips.GunShot,
            reloadClip: FXClips.GunReload,
            shotTime:0.3f,
            shotNumber:3,
            shotsDelay: 0.0f,
            bulletAmount:5,
            moveSpeed:9f,
            rotationSpeed:0f,
            maxDeviantAngle:20
        ));
    }
}