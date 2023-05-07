using System;
using Assets.scripts.Enums;
using Assets.scripts.service;
using UnityEngine;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{
    private readonly float chooseTimeScale = 0.05f;
    private AccumulateAbilityData[] accumulateData;

    [SerializeField]
    private AudioController audioController;

    [SerializeField]
    private CleaningShield bulletsCleaner;

    [SerializeField]
    private Transform bulletsCleanerSpawnPoint;

    [SerializeField]
    private Image[] cleaners;

    private GameObject currentChooseScreen;

    private AbilityData[] data;

    [SerializeField]
    private GameObject popovChooseScreen;

    [SerializeField]
    private Image[] popovEggs;

    [SerializeField]
    private Transform popovSpawnPoint;

    [SerializeField]
    private PopovEnter popovWithCucumber;

    [SerializeField]
    private PopovEnter popovWithEgg;

    [SerializeField]
    private Transform SpawnPoint;

    [SerializeField]
    private GameObject titorChooseScreen;

    [SerializeField]
    private Image titorInactiveIcon;

    [SerializeField]
    private Titor titorWithBunuses;

    [SerializeField]
    private Titor titorWithRockets;

    [SerializeField]
    private WarningController warningController;

    private void Start()
    {
        data = new AbilityData[]
        {
            new(Config.TitorCulDown, AudioSources.TitorNotification, FXClips.TitorReady, titorInactiveIcon)
        };
        accumulateData = new AccumulateAbilityData[]
        {
            new(Config.PopovEggCulDown, Config.PopovEggsCount, AudioSources.PopovNotification, FXClips.PopovReady,
                FXClips.PopovEggReady, popovEggs),
            new(Config.CleanerChargeCulDown, Config.CleanerChargeCount, AudioSources.CleanerNotification,
                FXClips.CleanerReady, FXClips.CleanerReady, cleaners)
        };
    }

    public void ApplyTimerBoostBonus(AbilityIndex ability, float boost)
    {
        data[(int)ability].Timer += boost;
    }

    public void ApplyTimerBoostBonus(AccumulateAbilityIndex ability, float boost)
    {
        accumulateData[(int)ability].Timer += boost;
    }

    private void CallTitor()
    {
        if (!data[(int)AbilityIndex.Titor].IsReady)
        {
            warningController.ThrowWarning(WarningType.TitorNotReady);
            return;
        }

        ToggleChooseScreen(titorChooseScreen);
    }

    private void ToggleChooseScreen(GameObject chooseScreen)
    {
        if (Math.Abs(Time.timeScale - 1) < 1e-3)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = chooseTimeScale;
            chooseScreen.SetActive(true);
            currentChooseScreen = chooseScreen;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            currentChooseScreen.SetActive(false);
        }
    }


    private void FillAccumulateAbility()
    {
        foreach (var accumulateAbility in accumulateData)
        {
            if (accumulateAbility.IsReady) continue;
            accumulateAbility.Timer += Time.deltaTime;
            accumulateAbility.Timer = Mathf.Clamp(accumulateAbility.Timer, 0, accumulateAbility.CulDownTime);
            if (Math.Abs(accumulateAbility.Timer - accumulateAbility.CulDownTime) < 1e-3)
                accumulateAbility.IsReady = true;
            var fullEggMaxIndex = (int)Math.Floor(accumulateAbility.Timer / accumulateAbility.OneChargeTime);
            if (fullEggMaxIndex > accumulateAbility.CurrentReadyChargeAmount)
                audioController.Play(accumulateAbility.AudioSource,
                    fullEggMaxIndex == accumulateAbility.ChargeAmount
                        ? accumulateAbility.ReadyClip
                        : accumulateAbility.OneChargeReady, AudioMixerOutputGroups.SilentClips);
            accumulateAbility.CurrentReadyChargeAmount = fullEggMaxIndex;
            for (var j = 0; j < accumulateAbility.Icons.Length; j++)
                accumulateAbility.Icons[j].fillAmount = j < fullEggMaxIndex ? 1 :
                    j > fullEggMaxIndex ? 0 :
                    accumulateAbility.Timer % accumulateAbility.OneChargeTime / accumulateAbility.OneChargeTime;
        }
    }

    private void FillCommonAbility()
    {
        foreach (var ability in data)
        {
            if (ability.IsReady) continue;
            ability.Timer += Time.deltaTime;
            ability.Timer = Mathf.Clamp(ability.Timer, 0, ability.CulDownTime);
            if (Math.Abs(ability.Timer - ability.CulDownTime) < 1e-3)
            {
                ability.IsReady = true;
                audioController.Play(ability.AudioSource, ability.ReadyClip, AudioMixerOutputGroups.SilentClips);
            }

            ability.InactiveIcon.fillAmount = 1 - ability.Timer / ability.CulDownTime;
        }
    }

    private void Update()
    {
        FillCommonAbility();
        FillAccumulateAbility();
        if (Input.GetKeyDown(KeyCode.E)) CallTitor();

        if (Input.GetKeyDown(KeyCode.R)) CallPopov();

        if (Input.GetKeyDown(KeyCode.Q)) CallCleaningShield();
    }

    private GameObject FindCucumberButton()
    {
        for (var i = 0; i < currentChooseScreen.transform.childCount; i++)
        {
            if (currentChooseScreen.transform.GetChild(i).name != "CucumberAttackButton") continue;
            return currentChooseScreen.transform.GetChild(i).gameObject;
        }

        return null;
    }

    private void CallCleaningShield()
    {
        var cleanerData = accumulateData[(int)AccumulateAbilityIndex.CleaningShield];
        if (cleanerData.Timer >= cleanerData.OneChargeTime)
        {
            Instantiate(bulletsCleaner, bulletsCleanerSpawnPoint.position, Quaternion.identity);
            cleanerData.Timer -= cleanerData.OneChargeTime;
            cleanerData.IsReady = false;
            return;
        }

        warningController.ThrowWarning(WarningType.CleanerNotready);
    }

    private void CallPopov()
    {
        var popovData = accumulateData[(int)AccumulateAbilityIndex.Popov];
        if (popovData.Timer >= popovData.OneChargeTime)
        {
            ToggleChooseScreen(popovChooseScreen);
            var cucumberButton = FindCucumberButton();
            var buttonComponent = cucumberButton.GetComponent<Button>();
            var image = cucumberButton.GetComponent<Image>();
            if (!popovData.IsReady)
            {
                buttonComponent.enabled = false;
                image.color = Color.gray;
            }
            else
            {
                buttonComponent.enabled = true;
                image.color = Color.white;
            }

            return;
        }

        warningController.ThrowWarning(WarningType.PopovNotReady);
    }

    public void CucumberAttack()
    {
        var popovData = accumulateData[(int)AccumulateAbilityIndex.Popov];
        Instantiate(popovWithCucumber, popovSpawnPoint.position, Quaternion.identity);
        popovData.Timer = 0;
        popovData.IsReady = false;
    }

    public void EggAttack()
    {
        var popovData = accumulateData[(int)AccumulateAbilityIndex.Popov];
        Instantiate(popovWithEgg, popovSpawnPoint.position, Quaternion.identity);
        popovData.Timer -= popovData.OneChargeTime;
        popovData.IsReady = false;
    }

    public void ReturnToGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        currentChooseScreen.SetActive(false);
    }

    public void FireSupport()
    {
        Instantiate(titorWithRockets, SpawnPoint.position, Quaternion.identity);
        data[(int)AbilityIndex.Titor].Timer = 0;
        data[(int)AbilityIndex.Titor].IsReady = false;
    }

    public void SupplySupport()
    {
        Instantiate(titorWithBunuses, SpawnPoint.position, Quaternion.identity);
        data[(int)AbilityIndex.Titor].Timer = 0;
        data[(int)AbilityIndex.Titor].IsReady = false;
    }

    private class AbilityData
    {
        public readonly AudioSources AudioSource;
        public readonly float CulDownTime;
        public readonly Image InactiveIcon;
        public readonly FXClips ReadyClip;
        public bool IsReady;
        public float Timer;

        public AbilityData(float culDownTime, AudioSources audioSource, FXClips readyClip, Image inactiveIcon)
        {
            Timer = 0;
            CulDownTime = culDownTime;
            ReadyClip = readyClip;
            InactiveIcon = inactiveIcon;
            AudioSource = audioSource;
            IsReady = false;
        }
    }

    private class AccumulateAbilityData
    {
        public readonly AudioSources AudioSource;
        public readonly int ChargeAmount;
        public readonly float CulDownTime;
        public readonly Image[] Icons;
        public readonly FXClips OneChargeReady;
        public readonly float OneChargeTime;
        public readonly FXClips ReadyClip;
        public int CurrentReadyChargeAmount;
        public bool IsReady;
        public float Timer;

        public AccumulateAbilityData(float oneChargeTime, int chargeAmount, AudioSources audioSource, FXClips readyClip,
            FXClips oneChargeReady,
            Image[] icons)
        {
            Timer = 0;
            CulDownTime = oneChargeTime * chargeAmount;
            OneChargeTime = oneChargeTime;
            ReadyClip = readyClip;
            ChargeAmount = chargeAmount;
            IsReady = false;
            CurrentReadyChargeAmount = 0;
            Icons = icons;
            AudioSource = audioSource;
            OneChargeReady = oneChargeReady;
        }
    }
}