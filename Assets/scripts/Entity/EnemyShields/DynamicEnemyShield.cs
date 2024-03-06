using UnityEngine;

public class DynamicEnemyShield : MonoBehaviour
{
    [SerializeField]
    private readonly float growSpeed = -7f;

    [SerializeField]
    private StaticEnemyShield referens;

    private void Start()
    {
        transform.localScale = new Vector3(7, 7, 7);
    }

    private void Update()
    {
        transform.localScale = new Vector3(
            Mathf.Clamp(transform.localScale.x + growSpeed * Time.deltaTime, referens.transform.localScale.x,
                float.PositiveInfinity),
            Mathf.Clamp(transform.localScale.y + growSpeed * Time.deltaTime, referens.transform.localScale.y,
                float.PositiveInfinity),
            Mathf.Clamp(transform.localScale.z + growSpeed * Time.deltaTime, referens.transform.localScale.z,
                float.PositiveInfinity));
        if (transform.localScale == referens.transform.localScale) Destroy(gameObject);
    }
}