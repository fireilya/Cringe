using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using UnityEngine;
using UnityEngine.Playables;
using Random = System.Random;

public class AdditionalAttackController : MonoBehaviour
{
    private bool isAttackAllowed;
    private Random random=new();

    [SerializeField]
    private PlayableDirector[] attacks;

    private PlayableDirector currentAttack;

    [SerializeField]
    private Timer attackTimer;

    public void AllowAttack(bool isStop)
    {
        attackTimer.StartTimer(random.Next(3, 10));
        isAttackAllowed=true;
        if (isStop)
        {
            currentAttack.Stop();
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttackAllowed && attackTimer.IsEnded)
        {
            var newAttack = random.Next() % attacks.Length;
            attacks[newAttack].Play();
            currentAttack = attacks[newAttack];

            //attacks[1].Play();
            //currentAttack = attacks[1];

            isAttackAllowed = false;
        }
    }
}
