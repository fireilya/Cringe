using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    [SerializeField]
    private NiggerOnParachute nigger;

    private string[] destroyTags =
    {
        "rocket",
        "bullet",
        "Player"
    };

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (destroyTags.All(x => x != collider.tag)) return;
        nigger.onParachute = false;
        nigger.Fall();
        Destroy(gameObject);
    }
}
