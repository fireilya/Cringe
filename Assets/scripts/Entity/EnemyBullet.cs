using Assets.scripts;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private MissileData data;

    [SerializeField]
    private int health = 20;

    private bool isSetuped;

    [SerializeField]
    private Timer lifeTimer;

    public Vector2 Direction { get; private set; }
    public float MoveSpeed { get; private set; }
    public float RotationSpeed { get; private set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag is "Popov" or "CleaningShield")
        {
            Destroy(gameObject);
            return;
        }

        if (data.DamageData.ContainsKey(collider.tag)) health -= data.DamageData[collider.tag];
    }

    public void Setup(Vector2 direction, float moveSpeed, float rotationSpeed, float lifeTime)
    {
        isSetuped = true;
        lifeTimer.StartTimer(lifeTime);
        Direction = direction;
        MoveSpeed = moveSpeed;
        RotationSpeed = rotationSpeed;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if ((lifeTimer.IsEnded || health <= 0) && isSetuped) Destroy(gameObject);
        transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
        transform.localPosition += new Vector3(Direction.x, Direction.y, 0f) * MoveSpeed * Time.deltaTime;
    }
}