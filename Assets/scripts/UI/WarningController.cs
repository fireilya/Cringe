using System.Collections.Generic;
using Assets.scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

public class WarningController : MonoBehaviour
{
    [SerializeField]
    private Image _cleanerNotReadyWarning;

    [SerializeField]
    private Image _popovNotReadyWarning;

    [SerializeField]
    private Image _rocketEmptyWarning;

    [SerializeField]
    private Image _titorNotReadyWarning;

    [SerializeField]
    private AudioController audioController;

    [SerializeField]
    private readonly float fallAlphaSpeed = 1.2f;


    private readonly HashSet<Image> warnings = new();

    private void Start()
    {
        warnings.Add(_rocketEmptyWarning);
        warnings.Add(_titorNotReadyWarning);
        warnings.Add(_popovNotReadyWarning);
        warnings.Add(_cleanerNotReadyWarning);
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var warning in warnings)
            warning.color = new Color(warning.color.r, warning.color.g,
                warning.color.b, warning.color.a - fallAlphaSpeed * Time.deltaTime);
    }

    public void ThrowWarning(WarningType type)
    {
        switch (type)
        {
            case WarningType.RocketAmmoEmpty:
                _rocketEmptyWarning.color = new Color(_rocketEmptyWarning.color.r, _rocketEmptyWarning.color.g,
                    _rocketEmptyWarning.color.b, 1);
                break;


            case WarningType.TitorNotReady:
                _titorNotReadyWarning.color = new Color(_titorNotReadyWarning.color.r, _titorNotReadyWarning.color.g,
                    _titorNotReadyWarning.color.b, 1);
                break;
            case WarningType.PopovNotReady:
                _popovNotReadyWarning.color = new Color(_popovNotReadyWarning.color.r, _popovNotReadyWarning.color.g,
                    _popovNotReadyWarning.color.b, 1);
                break;
            case WarningType.CleanerNotready:
                _cleanerNotReadyWarning.color = new Color(_cleanerNotReadyWarning.color.r,
                    _cleanerNotReadyWarning.color.g,
                    _cleanerNotReadyWarning.color.b, 1);
                break;
        }

        audioController.Play(AudioSources.UIFX, FXClips.Warning);
    }
}