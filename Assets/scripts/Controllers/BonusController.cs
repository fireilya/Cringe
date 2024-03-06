using Assets.scripts.Enums;
using Assets.scripts.service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private RocketController rocketController;
    [SerializeField]
    private AbilityController abilityController;
    [SerializeField]
    private AudioController audioController;
    public void ApplyBonus(string bonus)
    {
        switch (bonus)
        {
            case "FullHealth":
                gameController.ResetHealth();
                break;
            case "FullRockets":
                rocketController.Reload();
                break;
            case "MegaHealth":
                gameController.SetMegaHealth();
                break;
            case "TitorBoost":
                abilityController.ApplyTimerBoostBonus(AbilityIndex.Titor, Config.TitorBoost);
                break;
            case "PopovBoost":
                abilityController.ApplyTimerBoostBonus(AccumulateAbilityIndex.Popov, Config.PopovBoost);
                break;
            case "CleanerBoost":
                abilityController.ApplyTimerBoostBonus(AccumulateAbilityIndex.CleaningShield, Config.CleanerBoost);
                break;
        }

        audioController.Play(AudioSources.BonusFX, FXClips.Bonus, AudioMixerOutputGroups.SilentClips);
    }
}
