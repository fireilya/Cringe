using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Missile data", menuName = "Scriptable data/Missile")]
public class MissileData : ScriptableObject
{
    public Dictionary<string, int> DamageData = new()
    {
        { "rocket", 50 },
        { "bullet", 1 },
        { "ExplosionRadius", 30 },
        { "Egg", 40 }
    };
}