using System;
using Assets.scripts.Interfaces;
using UnityEngine;

public class PursuitAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    private AttackController attackController;

    [SerializeField]
    private Morgen enemy;

    [SerializeField]
    private Animator enemyAnimator;

    [SerializeField]
    private Transform enemyAttackControllerTransform;

    [SerializeField]
    private Spawner fireCircleSpawner;

    private bool isPositionSetted;
    private readonly float moveSpeed = 7.5f;
    private Vector2 neededPosition;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private PostAudioSource postAudioSource;

    private Vector3 returnPosition;

    public void StartAttack()
    {
        enemyAnimator.SetBool("Common", false);
        enemyAnimator.SetBool("Rotation", true);
        returnPosition = enemyAttackControllerTransform.position;
        neededPhaseCount = 5;
        currentPhaseCount = 0;
        isAttackStarted = true;
        SetNewPosition();
    }

    public void EndAttack()
    {
        if (!isOnZero)
        {
            ReturnToZero();
            return;
        }

        isAttackStarted = false;
        enemyAnimator.SetBool("Rotation", false);
        enemy.transform.rotation = Quaternion.identity;
        enemyAnimator.SetBool("Common", true);
        attackController.AllowAttack(false);
    }

    public void EndPhase()
    {
        isPositionSetted = false;
        Instantiate(postAudioSource);
        fireCircleSpawner.SingleFireBurst();
        currentPhaseCount++;
        if (currentPhaseCount < neededPhaseCount)
        {
            SetNewPosition();
            return;
        }

        neededPosition = returnPosition;
        isPositionSetted = true;
    }

    public void ReturnToZero()
    {
        enemyAttackControllerTransform.position = Vector3.MoveTowards(enemyAttackControllerTransform.position,
            returnPosition,
            moveSpeed * Time.deltaTime);
        if (enemyAttackControllerTransform.position == returnPosition) isOnZero = true;
    }

    public bool isAttackStarted { get; set; }
    public bool isOnZero { get; set; }
    public int neededPhaseCount { get; set; }
    public int currentPhaseCount { get; set; }

    private void Start()
    {
    }

    private void Update()
    {
        if (isAttackStarted && isPositionSetted)
        {
            enemyAttackControllerTransform.position = Vector3.MoveTowards(enemyAttackControllerTransform.position,
                new Vector3(neededPosition.x, neededPosition.y, enemyAttackControllerTransform.position.z),
                moveSpeed * Time.deltaTime);
            if (Math.Abs(enemyAttackControllerTransform.position.x - neededPosition.x) < 1e-3
                && Math.Abs(enemyAttackControllerTransform.position.y - neededPosition.y) < 1e-3)
            {
                if (currentPhaseCount == neededPhaseCount)
                {
                    EndAttack();
                    return;
                }

                EndPhase();
            }
        }
    }

    private void SetNewPosition()
    {
        neededPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
        isPositionSetted = true;
    }
}