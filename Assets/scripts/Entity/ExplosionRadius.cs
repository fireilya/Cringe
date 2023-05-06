using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    private readonly float fallSpeed = 1.2f;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b,
            sprite.color.a - fallSpeed * Time.deltaTime);
        if (sprite.color.a <= 0) Destroy(gameObject);
    }
}