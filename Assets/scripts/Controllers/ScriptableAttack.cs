using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.Interfaces;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Random = System.Random;

public class ScriptableAttack : MonoBehaviour
{
    [SerializeField]
    private TripleLaserAttack tripleLaserAttack;
    [SerializeField]
    private NegrGunAttack negrGunAttack;

    void Start()
    {
        
    }

    void Update()
    {
       
    }

    public void StartNegrGunAttack()
    {
        negrGunAttack.StartAttack();
    }
    public void StartTripleLaserAttack()
    {
        tripleLaserAttack.StartAttack();
    }

}
