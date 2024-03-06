using System.Linq;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    private readonly string[] destroyTags =
    {
        "rocket",
        "bullet",
        "Player"
    };

    [SerializeField]
    private NiggerOnParachute nigger;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (destroyTags.All(x => x != collider.tag)) return;
        nigger.onParachute = false;
        nigger.Fall();
        Destroy(gameObject);
    }
}