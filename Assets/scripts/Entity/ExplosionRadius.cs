using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float fallSpeed = 1.0f;
    void Start()
    {
        sprite=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b,
            sprite.color.a - fallSpeed * Time.deltaTime);
        if (sprite.color.a<=0)
        {
            Destroy(gameObject);
        }
    }
}
