using Assets.scripts;
using Assets.scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    [SerializeField]
    private Timer mainTimer;
    [SerializeField]
    private Rocket mainRocket;

    [SerializeField]
    private Player player;

    [SerializeField]
    private TMP_Text rocketCountText;
    private Rocket spawnedRocket;
    private Timer rocketCulDownTimer;
    private int currentRocketCount;
    private int rocketCount=10;
    private float RocketCulDown=1.0f;
    [SerializeField]
    private Image rocketImage;
    public bool IsEmpty { get; private set; } = true;

    [SerializeField]
    private Transform rocketSpawnerTransform;

    [SerializeField]
    private AudioController audioController;

    public void Reload()
    {
        currentRocketCount = rocketCount;
        rocketCountText.text = "X" + currentRocketCount;
    }
    void Start()
    {
        rocketCulDownTimer = Instantiate(mainTimer);
        rocketImage.fillAmount = 0;
    }


    void Update()
    {
        switch (rocketCulDownTimer.IsEnded)
        {
            case false:
                rocketImage.fillAmount = 1 - rocketCulDownTimer.Progress;
                break;
            case true when IsEmpty && currentRocketCount > 0:
                CreateRocket();
                break;
        }
    }

    private void CreateRocket()
    {
        audioController.Play(AudioSources.PlayerFX, FXClips.RocketReload);
        spawnedRocket = Instantiate(mainRocket, rocketSpawnerTransform.position, player.transform.rotation);
        spawnedRocket.Engine = spawnedRocket.transform.GetChild(0).gameObject;
        spawnedRocket.gameObject.transform.parent = player.transform;
        IsEmpty = false;
    }

    public void LaunchRocket()
    {
        spawnedRocket.Engine.SetActive(true);
        spawnedRocket.tag = "rocket";
        spawnedRocket.gameObject.transform.parent = null;
        spawnedRocket.Launch();
        rocketCulDownTimer.StartTimer(RocketCulDown);
        currentRocketCount--;
        IsEmpty=true;
        rocketCountText.text = "X" + currentRocketCount;
        rocketImage.fillAmount = 1;
    }
}
