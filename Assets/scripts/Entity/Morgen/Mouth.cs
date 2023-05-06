using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    [SerializeField]
    private Morgen morgen;
    [SerializeField]
    private MissileData missileData;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (missileData.DamageData.ContainsKey(collider.tag) && collider.tag != "ExplosionRadius")
            morgen.Hit(missileData.DamageData[collider.tag]);
    }
}
