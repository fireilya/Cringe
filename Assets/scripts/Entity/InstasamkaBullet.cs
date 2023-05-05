using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstasamkaBullet : MonoBehaviour
{
    [SerializeField]
    private float growSpeed;

    [SerializeField]
    private float forcePower;
    private Rigidbody2D rb;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector2.left * forcePower);
    }
    void Update()
    {
        transform.localScale = new Vector3(
            transform.localScale.x + growSpeed * Time.deltaTime,
            transform.localScale.y + growSpeed * Time.deltaTime,
            transform.localScale.z + growSpeed * Time.deltaTime);
    }
}
