using UnityEngine;

public class InstasamkaBullet : MonoBehaviour
{
    [SerializeField]
    private float forcePower;

    [SerializeField]
    private float growSpeed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.left * forcePower);
    }

    private void Update()
    {
        transform.localScale = new Vector3(
            transform.localScale.x + growSpeed * Time.deltaTime,
            transform.localScale.y + growSpeed * Time.deltaTime,
            transform.localScale.z + growSpeed * Time.deltaTime);
    }
}