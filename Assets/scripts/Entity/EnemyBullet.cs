using UnityEngine;
using Timer = Assets.scripts.Timer;

public class EnemyBullet : MonoBehaviour
{
    public Vector2 Direction { get; private set; }
    public float MoveSpeed { get; private set; }
    public float RotationSpeed { get; private set; }
    private bool isSetuped;

    [SerializeField]
    private Timer lifeTimer;
    public void Setup(Vector2 direction, float moveSpeed, float rotationSpeed, float lifeTime)
    {
        isSetuped=true;
        lifeTimer.StartTimer(lifeTime);
        Direction =direction;
        MoveSpeed=moveSpeed;
        RotationSpeed=rotationSpeed;
    }

    //void OnEnable()
    //{
        
    //}
    void Start()
    {
        
    }

    void Update()
    {
        if (lifeTimer.IsEnded && isSetuped)
        {
            Destroy(gameObject);
        }
        transform.Rotate(Vector3.forward*RotationSpeed*Time.deltaTime);
        transform.localPosition += new Vector3(Direction.x, Direction.y, 0f) * MoveSpeed*Time.deltaTime;
    }
}
