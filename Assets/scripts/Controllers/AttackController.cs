using Assets.scripts;
using Assets.scripts.service;
using UnityEngine;
using UnityEngine.Playables;
using Random = System.Random;

public class AttackController : MonoBehaviour
{
    private readonly Random random = new();
    private readonly int SupplyAttackRare = 7;

    [SerializeField]
    private PlayableDirector[] _transitionAttacks;

    [HideInInspector]
    public int AttackAmount;

    [SerializeField]
    private PlayableDirector[] attacks;

    private int attackWithoutSupply;
    private int currentAttack;

    [HideInInspector]
    public int FullAttackAmount;

    private bool isChooseAllowed = true;

    [HideInInspector]
    public bool IsStateTransitionAttack;

    [SerializeField]
    private AudioSource music;

    [HideInInspector]
    public int NextTransitionAttack;

    private int previousAttack = -1;

    [SerializeField]
    private Timer waitTimer;

    private void Start()
    {
        FullAttackAmount = attacks.Length;
    }

    public void AllowFirstAttack()
    {
        waitTimer.StartTimer(Config.GameStartTime);
        isChooseAllowed = true;
    }

    public void AllowAttack(bool isStop)
    {
        isChooseAllowed = true;
        if (isStop) attacks[currentAttack].Stop();
    }

    public void LoseGame()
    {
        isChooseAllowed = false;
        foreach (var director in attacks) director.Stop();
    }

    private void StartTransitionAttack()
    {
        isChooseAllowed = false;
        IsStateTransitionAttack = false;
        music.Stop();
        _transitionAttacks[NextTransitionAttack].Play();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isChooseAllowed && waitTimer.IsEnded)
        {
            if (!Config.IsDevelopmentVersion)
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
                } while (newAttack == previousAttack || newAttack == 3 && attackWithoutSupply < SupplyAttackRare);

                if (newAttack == 3) attackWithoutSupply = 0;
                else attackWithoutSupply++;
                previousAttack = newAttack;
                currentAttack = newAttack;
                attacks[currentAttack].Play();
            }
            else
            {
                currentAttack = 3;
                attacks[3].Play();
            }

            isChooseAllowed = false;
        }
    }
}