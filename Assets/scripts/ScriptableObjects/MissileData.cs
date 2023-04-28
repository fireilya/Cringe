using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Missile data", menuName = "Scriptable data/Missile")]
public class MissileData : ScriptableObject
{
    public Dictionary<string, int> DamageData = new()
    {
        { "rocket", 50 },
        {"bullet", 5},
        {"ExplosionRadius", 30}
    };
}
