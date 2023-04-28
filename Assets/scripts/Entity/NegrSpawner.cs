using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class NegrSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;

    [SerializeField]
    private NiggerbBullet bullet;

    private int supplyShotBulletAmount=5;

    private float maxSupplyShotDeviantAngle=20f;


    public void CommonNegrShot(int bulletAmount, float moveSpeed, float rotationSpeed, float maxDeviantAngle, float lifeTime=1.0f)
    {
        var forward = Vector2.left.Rotate(gun.transform.localEulerAngles.z);
        var rotationDelta = maxDeviantAngle * 2 / (bulletAmount - 1);
        for (var i = -maxDeviantAngle; i <= maxDeviantAngle; i += rotationDelta)
        {
            var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.Setup(forward.Rotate(i), moveSpeed, rotationSpeed, lifeTime);
        }
    }

    public void SupplyShot()
    {
        var forward = Vector2.left.Rotate(gun.transform.localEulerAngles.z);
        var rotationDelta = maxSupplyShotDeviantAngle*2 / (supplyShotBulletAmount - 1);
        for (var i = -maxSupplyShotDeviantAngle; i <= maxSupplyShotDeviantAngle; i+=rotationDelta)
        {
            var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.Setup(forward.Rotate(i), 12f, 0f, 1.0f);
        }
    }


}
