using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldController : MonoBehaviour
{
    [SerializeField]
    private StaticEnemyShield staticEnemyShield;
    [SerializeField]
    private DynamicEnemyShield dynamicEnemyShield;

    [SerializeField]
    private Transform shieldsSpawnPoint;

    [SerializeField]
    private Transform morgenTransform;

    public void CreateDynamicShield()
    {
        var dynamicShield = Instantiate(dynamicEnemyShield, shieldsSpawnPoint.position, Quaternion.identity);
        dynamicShield.transform.parent=morgenTransform;
    }

    public void CreateStaticShield(float buildTime)
    {
        var staticShield = Instantiate(staticEnemyShield, shieldsSpawnPoint.position, Quaternion.identity);
        staticShield.transform.parent = morgenTransform;
        staticShield.StartBuilding(buildTime);
    }
}
