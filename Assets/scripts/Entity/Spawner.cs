using System.Collections;
using Assets.scripts.Enums;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private EnemyBullet negrBullet;

    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private AudioController audioController;

    private readonly float maxSupplyShotDeviantAngle = 20f;

    private readonly int supplyShotBulletAmount = 5;


    public IEnumerator DoShot(
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
            var forward = Vector2.left.Rotate(gun.transform.localEulerAngles.z);
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