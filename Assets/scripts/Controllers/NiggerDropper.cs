using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using UnityEngine;

public class NiggerDropper : MonoBehaviour
{
    [SerializeField]
    private NiggerOnParachute niggerToDrop;

    private float offset = 1.2566f;
    public void DropNiggers()
    {
        for (var i = -offset*2; i <= offset*2; i+=offset)
        {
            Instantiate(niggerToDrop,
                new Vector3(transform.localPosition.x + i, transform.localPosition.y, transform.localPosition.z),
                Quaternion.identity);
        }
    }
}
