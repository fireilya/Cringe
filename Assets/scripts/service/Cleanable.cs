using UnityEngine;

public class Cleanable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag is "CleaningShield")
            Destroy(gameObject);
    }
}