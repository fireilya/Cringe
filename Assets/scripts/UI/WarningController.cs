using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

public class WarningController : MonoBehaviour
{
    [SerializeField]
    private float fallAlphaSpeed=1.2f;

    [SerializeField]
    private Image RocketEmptyWarning;
    [SerializeField]
    private Image TitorNotReadyWarning;
    [SerializeField]
    private AudioController audioController;


    private HashSet<Image> warnings=new();

    void Start()
    {
        warnings.Add(RocketEmptyWarning);
        warnings.Add(TitorNotReadyWarning);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var warning in warnings)
        {
            warning.color= new Color(warning.color.r, warning.color.g,
                warning.color.b, warning.color.a-fallAlphaSpeed*Time.deltaTime);
        }

    }

    public void ThrowWarning(WarningType type)
    {
        switch (type)
        {
            case WarningType.RocketAmmoEmpty:
                RocketEmptyWarning.color = new Color(RocketEmptyWarning.color.r, RocketEmptyWarning.color.g,
                    RocketEmptyWarning.color.b, 1);
                break;


            case WarningType.TitorNotReady:
                TitorNotReadyWarning.color = new Color(TitorNotReadyWarning.color.r, TitorNotReadyWarning.color.g,
                    TitorNotReadyWarning.color.b, 1);
                break;

        }
        audioController.Play(AudioSources.UIFX, FXClips.Warning);
    }
}
