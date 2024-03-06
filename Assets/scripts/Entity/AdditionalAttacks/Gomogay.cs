using UnityEngine;
using UnityEngine.Playables;

public class Gomogay : MonoBehaviour
{
    [SerializeField]
    private int attackState;

    [SerializeField]
    private readonly float boostStrength = 45;

    [SerializeField]
    private ConstantRotation constantRotation;

    [SerializeField]
    private Sprite eye;

    private PlayableDirector firstAttack;

    [SerializeField]
    private Sprite gomogay;

    private Rigidbody2D gomoRB;

    [SerializeField]
    private readonly float outBoostStrength = 90;

    private Vector2 outDirection;
    private Transform playerTransform;

    [SerializeField]
    private RotationTracker rotationTracker;

    private PlayableDirector secondAttack;

    [SerializeField]
    private Transform spawnPoint;

    private SpriteRenderer spriteRenderer;
    private PlayableDirector thirdAttack;


    public void SetNextState()
    {
        attackState++;
        switch (attackState)
        {
            case 1:
                rotationTracker.isEnable = true;
                firstAttack.Play();
                break;
            case 2:
                rotationTracker.isEnable = false;
                gomoRB.velocity = Vector2.zero;
                spriteRenderer.sprite = eye;
                secondAttack.Play();
                break;
            case 3:
                spriteRenderer.sprite = gomogay;
                constantRotation.EnableRotation(3000);
                thirdAttack.Play();
                break;
            case 4:
                var temp = playerTransform.position - transform.position;
                outDirection = new Vector2(temp.x, temp.y).normalized;
                break;
        }
    }

    private void Start()
    {
        gomoRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        firstAttack = GameObject.Find("FirstAttack").GetComponent<PlayableDirector>();
        secondAttack = GameObject.Find("SecondAttack").GetComponent<PlayableDirector>();
        thirdAttack = GameObject.Find("ThirdAttack").GetComponent<PlayableDirector>();
    }

    public void EndGomogayAttack()
    {
        gomoRB.angularVelocity = 0;
        transform.position = spawnPoint.position;
        transform.rotation = Quaternion.identity;
        gomoRB.velocity = Vector2.zero;
        constantRotation.IsEnable = false;
        attackState = 0;
    }

    private void FixedUpdate()
    {
        if (attackState == 1) gomoRB.AddRelativeForce(Vector2.up * boostStrength);

        if (attackState == 4) gomoRB.AddForce(outDirection * outBoostStrength);
    }

    private void Update()
    {
    }
}