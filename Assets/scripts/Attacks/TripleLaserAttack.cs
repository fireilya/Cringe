using System;
using Assets.scripts.Interfaces;
using UnityEngine;
using UnityEngine.Playables;

public class TripleLaserAttack : MonoBehaviour, IAttack
{
    private readonly float speed = 9.5f;

    [SerializeField]
    private AttackController attackController;

    private float currentYPosition;

    [SerializeField]
    private Animator enemyAnimator;

    [SerializeField]
    private Transform enemyAttackControllerTransform;

    private float lastYPosition;
    private float neededYPosition;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private PlayableDirector postTripleLaserDirector;

    [SerializeField]
    private PlayableDirector tripleLaserDirector;

    public bool isPhaseEnd { get; set; }

    public void StartAttack()
    {
        neededPhaseCount = 3;
        currentPhaseCount = 0;
        isAttackStarted = true;
        isOnZero = false;
        SetNeededYPosition();
        enemyAnimator.SetBool("Common", false);
    }

    public void EndAttack()
    {
        if (isOnZero)
        {
            isAttackStarted = false;
            enemyAnimator.SetBool("Common", true);
            postTripleLaserDirector.Play();
        }
        else
        {
            ReturnToZero();
        }
    }

    public void EndPhase()
    {
        currentPhaseCount++;
        tripleLaserDirector.Stop();
        SetNeededYPosition();
    }

    public void ReturnToZero()
    {
        var direction = 0 - enemyAttackControllerTransform.localPosition.y < 0 ? -1 : 1;
        var newYPosition = enemyAttackControllerTransform.localPosition.y + 1 * speed * Time.deltaTime * direction;
        newYPosition = lastYPosition < 0
            ? Mathf.Clamp(newYPosition, lastYPosition, 0)
            : Mathf.Clamp(newYPosition, 0, lastYPosition);
        enemyAttackControllerTransform.localPosition = new Vector3(enemyAttackControllerTransform.localPosition.x,
            newYPosition, enemyAttackControllerTransform.localPosition.z);
        isOnZero = enemyAttackControllerTransform.localPosition.y == 0;
    }

    public bool isAttackStarted { get; set; }
    public bool isOnZero { get; set; }
    public int neededPhaseCount { get; set; }
    public int currentPhaseCount { get; set; }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAttackStarted)
        {
            if (currentPhaseCount == neededPhaseCount)
            {
                EndAttack();
            }
            else
            {
                var direction = neededYPosition - enemyAttackControllerTransform.localPosition.y < 0 ? -1 : 1;
                var newYPosition = enemyAttackControllerTransform.localPosition.y
                                   + 1 * speed * Time.deltaTime * direction;
                newYPosition = lastYPosition < neededYPosition
                    ? Mathf.Clamp(newYPosition, lastYPosition, neededYPosition)
                    : Mathf.Clamp(newYPosition, neededYPosition, lastYPosition);
                enemyAttackControllerTransform.localPosition = new Vector3(
                    enemyAttackControllerTransform.localPosition.x,
                    newYPosition,
                    enemyAttackControllerTransform.localPosition.z);
                if (Math.Abs(newYPosition - neededYPosition) < 1e-3) tripleLaserDirector.Play();
            }
        }
    }

    private void SetNeededYPosition()
    {
        neededYPosition = playerTransform.localPosition.y;
        lastYPosition = enemyAttackControllerTransform.localPosition.y;
    }
}