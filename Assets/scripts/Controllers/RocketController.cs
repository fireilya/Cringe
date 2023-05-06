using Assets.scripts;
using Assets.scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    [SerializeField]
    private AudioController audioController;

    private int currentRocketCount;

    [SerializeField]
    private Image InactiveRocketIcon;

    private bool isEmpty = true;

    [SerializeField]
    private Rocket mainRocket;

    [SerializeField]
    private Timer mainTimer;

    [SerializeField]
    private Player player;

    private readonly int rocketCount = 10;
    private readonly float RocketCulDown = 1.0f;
    private Timer rocketCulDownTimer;

    [SerializeField]
    private Transform rocketSpawnerTransform;

    private Rocket spawnedRocket;

    [SerializeField]
    private WarningController warningController;

    public void Reload()
    {
        currentRocketCount = rocketCount;
        InactiveRocketIcon.fillAmount = 0;
    }

    private void Start()
    {
        rocketCulDownTimer = Instantiate(mainTimer);
    }


    private void Update()
    {
        switch (rocketCulDownTimer.IsEnded)
        {
            case false:
                break;
            case true when isEmpty && currentRocketCount > 0:
                CreateRocket();
                break;
        }
    }

    private void CreateRocket()
    {
        audioController.Play(AudioSources.PlayerFX, FXClips.RocketReload);
        spawnedRocket = Instantiate(mainRocket, rocketSpawnerTransform.position, player.transform.rotation);
        spawnedRocket.gameObject.transform.parent = player.transform;
        isEmpty = false;
    }

    public void LaunchRocket()
    {
        if (isEmpty)
        {
            warningController.ThrowWarning(WarningType.RocketAmmoEmpty);
            return;
        }

        spawnedRocket.tag = "rocket";
        spawnedRocket.gameObject.transform.parent = null;
        spawnedRocket.Launch();
        rocketCulDownTimer.StartTimer(RocketCulDown);
        currentRocketCount--;
        isEmpty = true;
        InactiveRocketIcon.fillAmount = 1 - (float)currentRocketCount / rocketCount;
    }
}