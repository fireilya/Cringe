using System;
using Assets.scripts;
using UnityEngine;
using UnityEngine.Playables;
using Random = System.Random;

public class AttackController : MonoBehaviour
{
    private int attackWithoutSupply;
    private int SupplyAttackRare = 7;
    [HideInInspector]
    public int AttackAmount;
    private bool isChooseAllowed = true;
    private Random random = new();
    private int currentAttack;
    private int previousAttack = -1;
    [HideInInspector]
    public int FullAttackAmount;

    [SerializeField]
    private PlayableDirector[] attacks;

    [SerializeField]
    private PlayableDirector[] _transitionAttacks;

    [SerializeField]
    private Timer waitTimer;

    [SerializeField]
    private AudioSource music;
    [HideInInspector]
    public bool IsStateTransitionAttack;
    [HideInInspector]
    public int NextTransitionAttack;

    void Start()
    {
        FullAttackAmount=attacks.Length;
    }

    public void AllowFirstAttack()
    {
        waitTimer.StartTimer(1.0f);
        isChooseAllowed = true;
    }

    public void AllowAttack(bool isStop)
    {
        isChooseAllowed = true;
        if (isStop)
        {
            attacks[currentAttack].Stop();
        }
    }

    public void LoseGame()
    {
        isChooseAllowed = false;
        foreach (var director in attacks)
        {
            director.Stop();
        }
    }

    private void StartTransitionAttack()
    {
        isChooseAllowed = false;
        IsStateTransitionAttack = false;
        music.Stop();
        _transitionAttacks[NextTransitionAttack].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChooseAllowed && waitTimer.IsEnded)
        {
            if (IsStateTransitionAttack)
            {
                StartTransitionAttack();
                return;
            }
            int newAttack;
            do
            {
                newAttack = random.Next(0, int.MaxValue) % AttackAmount;

            } while (newAttack == previousAttack || (newAttack == 3 && attackWithoutSupply < SupplyAttackRare));

            if (newAttack == 3)
            {
                attackWithoutSupply = 0;
            }
            else
            {
                attackWithoutSupply++;
            }
            previousAttack = newAttack;
            currentAttack = newAttack;
            attacks[currentAttack].Play();


            //currentAttack = 3;
            //directors[3].Play();
            isChooseAllowed = false;
            
            
        }
    }
}

