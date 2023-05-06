using System;
using System.Collections;
using Assets.scripts.Enums;
using Assets.scripts.Interfaces;
using Assets.scripts.service;
using UnityEngine;

public class TrackRocketAttack : MonoBehaviour, IAttack
{
    private readonly float _rotationSpeed = 120f;

    [SerializeField]
    private AttackController attackController;

    [SerializeField]
    private AudioController audioController;

    private float currentRotation;

    [SerializeField]
    private Animator enemyAnimator;

    [SerializeField]
    private Transform gun;

    private bool isRotationSetted;

    private float neededRotation;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Spawner rocketSpawner;


    public void StartAttack()
    {
        enemyAnimator.SetBool("Common", false);
        gun.eulerAngles = new Vector3(gun.eulerAngles.x, gun.eulerAngles.y, 0);
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
        enemyAnimator.SetBool("Common", true);
        attackController.AllowAttack(false);
    }

    public void EndPhase()
    {
        isRotationSetted = false;
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

    public IEnumerator DelayShot()
    {
        rocketSpawner.SpawnTrackRocket();
        audioController.Play(AudioSources.Bazuka, FXClips.BazukaShot, AudioMixerOutputGroups.SilentClips);
        yield return new WaitForSeconds(0.5f);
        SetNextRotation();
    }

    private void Update()
    {
        if (isAttackStarted && isRotationSetted)
        {
            gun.gameObject.SetActive(true);
            currentRotation = (currentRotation < 0 ? 360 + currentRotation : currentRotation) % 360;
            currentRotation =
                VectorWorker.RotateToTargetWithSpeed(currentRotation, neededRotation, _rotationSpeed * Time.deltaTime);
            gun.eulerAngles = new Vector3(gun.eulerAngles.x, gun.eulerAngles.y, currentRotation);
            if (Math.Abs(currentRotation - neededRotation) < 1e-3) EndPhase();
        }
    }

    private void SetNextRotation()
    {
        currentRotation = currentRotation < 0 ? 360 + currentRotation : currentRotation;
        neededRotation = VectorWorker.FindRotationByTarget(gun.position, playerTransform.position);
        isRotationSetted = true;
    }
}