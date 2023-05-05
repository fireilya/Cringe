using UnityEngine;
using Timer = Assets.scripts.Timer;

public class EnemyBullet : MonoBehaviour
{
    public Vector2 Direction { get; private set; }
    public float MoveSpeed { get; private set; }
    public float RotationSpeed { get; private set; }
    private bool isSetuped;
    [SerializeField]
    private int health=20;

    [SerializeField]
    private MissileData data;

    [SerializeField]
    private Timer lifeTimer;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Popov")
        {
            Destroy(gameObject);
        }
        if (data.DamageData.ContainsKey(collider.tag))
        {
            health -= data.DamageData[collider.tag];
            return;
        }
    }
    public void Setup(Vector2 direction, float moveSpeed, float rotationSpeed, float lifeTime)
    {
        isSetuped=true;
        lifeTimer.StartTimer(lifeTime);
        Direction =direction;
        MoveSpeed=moveSpeed;
        RotationSpeed=rotationSpeed;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if ((lifeTimer.IsEnded || health<=0) && isSetuped)
        {
            Destroy(gameObject);
        }
        transform.Rotate(Vector3.forward*RotationSpeed*Time.deltaTime);
        transform.localPosition += new Vector3(Direction.x, Direction.y, 0f) * MoveSpeed*Time.deltaTime;
    }
}
