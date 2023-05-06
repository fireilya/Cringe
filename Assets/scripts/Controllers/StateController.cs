using Assets.scripts.Enums;
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
    private GameObject healthMarker;

    [SerializeField]
    private Morgen morgen;

    public void SetTransitionAttack(int state)
    {
        morgen.ChangeMorgenHittableState(false);
        morgen.SetStateHealth(state);
        if (state == 0)
        {
            morgen.StartBuilding(1.0f);
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
                attackController.AttackAmount = 4;
                healthMarker.SetActive(true);
                break;
            case 1:
                audioController.Play(AudioSources.Music, Music.Pursuit);
                attackController.AttackAmount = 7;
                Destroy(healthMarker);
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
        return health switch
        {
            >= 1750 => 0,
            > 1000 and < 1750 => 1,
            _ => 2
        };
    }
}