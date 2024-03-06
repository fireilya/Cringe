using Assets.scripts.Enums;
using Assets.scripts.service;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField]
    private AdditionalAttackController additionalAttackController;

    [SerializeField]
    private AttackController attackController;

    [SerializeField]
    private AudioController audioController;

    [SerializeField]
    private Morgen morgen;

    public void SetTransitionAttack(int state)
    {
        morgen.ChangeMorgenHittableState(false);
        morgen.SetStateHealth(state);
        if (state == 0)
        {
            morgen.StartBuilding(Config.GameStartTime);
            SetState(state);
            return;
        }

        attackController.NextTransitionAttack = state - 1;
        attackController.IsStateTransitionAttack = true;
    }

    public void SetState(int state)
    {
        morgen.ChangeMorgenHittableState(true);
        switch (state)
        {
            case 0:
                audioController.Play(AudioSources.Music, Music.Celerity);
                attackController.AttackAmount = Config.AllowedAttackAmountByState[0];
                break;
            case 1:
                audioController.Play(AudioSources.Music, Music.Pursuit);
                attackController.AttackAmount = Config.AllowedAttackAmountByState[1];
                break;
            case 2:
                additionalAttackController.AllowAttack(false);
                attackController.AttackAmount = attackController.FullAttackAmount;
                audioController.Play(AudioSources.Music, Music.Realistic);
                break;
        }

        attackController.AllowAttack(false);
    }

    public int CheckStateByHealth(int health)
    {
        if (health >= Config.MainEnemyStateHealth[1]) return 0;
        if (health > Config.MainEnemyStateHealth[2] && health < Config.MainEnemyStateHealth[1]) return 1;
        return 2;
    }
}