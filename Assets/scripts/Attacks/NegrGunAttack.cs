using System;
using System.Collections;
using Assets.scripts;
using Assets.scripts.Enums;
using Assets.scripts.Interfaces;
using UnityEngine;

public class NegrGunAttack : MonoBehaviour, IAttack
{
    private readonly float _rotationSpeed = 100f;

    [SerializeField]
    private AttackController attackController;

    [SerializeField]
    private Animator enemyAnimator;

    [SerializeField]
    private Transform gun;

    private double lastRotation;

    private double neededRotation;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Spawner negrSpawner;

    [SerializeField]
    private AudioController audioController;

    //[SerializeField]
    //private Timer PhaseTimer;

    private float currentRotation;
    private bool isRotationSetted;


    public void StartAttack()
    {
        enemyAnimator.SetBool("Common", false);
        gun.eulerAngles=new Vector3(gun.eulerAngles.x, gun.eulerAngles.y, 0);
        currentRotation = 0;
        SetNextRotation();
        gun.gameObject.SetActive(true);
        neededPhaseCount = 3;
        currentPhaseCount = -1;
        isOnZero = false;
        isAttackStarted = true;
    }

    public void EndAttack()
    {
        isAttackStarted = false;
        gun.gameObject.SetActive(false);
        attackController.AllowAttack(false);
    }

    public IEnumerator DelayShot()
    {
        negrSpawner.CommonNegrShot();
        yield return new WaitForSeconds(0.5f);
        SetNextRotation();
    }

    public void EndPhase()
    {
        isRotationSetted=false;
        currentPhaseCount++;
        if (currentPhaseCount == neededPhaseCount)
        {
            EndAttack();
            return;
        }

        StartCoroutine(DelayShot());
    }

    public void ReturnToZero()
    {
        throw new NotImplementedException();
    }

    public bool isAttackStarted { get; set; }
    public bool isOnZero { get; set; }
    public int neededPhaseCount { get; set; }
    public int currentPhaseCount { get; set; }

    private void Update()
    {
        if (isAttackStarted && isRotationSetted)
        {
            gun.gameObject.SetActive(true);
            var direction = currentRotation > neededRotation ? -1 : 1;
            currentRotation += _rotationSpeed * Time.deltaTime * direction;
            currentRotation = neededRotation < lastRotation
                ? Mathf.Clamp(currentRotation, (float)neededRotation, (float)lastRotation)
                : Mathf.Clamp(currentRotation, (float)lastRotation, (float)neededRotation);
            gun.eulerAngles = new Vector3(gun.eulerAngles.x, gun.eulerAngles.y, currentRotation);
            gun.eulerAngles = new Vector3(
                gun.eulerAngles.x,
                gun.eulerAngles.y, 
                currentRotation);
            if (Math.Abs(currentRotation - neededRotation) < 1e-3)
            {
                EndPhase();
            }
        }
    }

    private void SetNextRotation()
    {
        lastRotation = currentRotation;
        var aimRotationAngle = -Math.Atan(
            (playerTransform.position.y-gun.position.y)/(gun.position.x-playerTransform.position.x))*Mathf.Rad2Deg;
        isRotationSetted=true;
        neededRotation = aimRotationAngle;
    }
}