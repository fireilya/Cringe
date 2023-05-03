using System;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts.Enums;
using UnityEngine;
using UnityEngine.UI;



public class AbilityController : MonoBehaviour
{
    [SerializeField]
    private Transform SpawnPoint;

    [SerializeField]
    private Titor titorWithRockets;
    [SerializeField]
    private Titor titorWithBunuses;
    [SerializeField]
    private WarningController warningController;
    [SerializeField]
    private AudioController audioController;

    [SerializeField]
    private GameObject titorChooseScreen;
    [SerializeField] 
    private GameObject popovChooseScreen;

    [SerializeField]
    private PopovEnter popovWithCucumber;

    [SerializeField]
    private PopovEnter popovWithEgg;

    [SerializeField]
    private Transform popovSpawnPoint;
    private float chooseTimeScale = 0.05f;
    private GameObject currentChooseScreen;
    private float popovEggTime = 30f;
    private int readyPopovEggCount;

    [SerializeField]
    private Image[] popovEggs;

    [SerializeField]
    private Image[] inactiveIcons;

    private AbilityData[] data =
    {
        new( 100, "Titor", FXClips.TitorReady),
        new( 90, "Popov", FXClips.PopovReady),
    };

    public void ApplyTimerBoostBonus(AbilityIndex ability, float boost)
    {
        data[(int)ability].Timer += boost;
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


    private void FillPopovEggs()
    {
        var index = (int)AbilityIndex.Popov;
        var fullEggMaxIndex = (int)Math.Floor(data[index].Timer / popovEggTime);
        if (fullEggMaxIndex>readyPopovEggCount && fullEggMaxIndex!=3)
        {
            audioController.Play(AudioSources.UIFX, FXClips.PopovEggReady, AudioMixerOutputGroups.SilentClips);
        }

        readyPopovEggCount = fullEggMaxIndex;
        for (var i = 0; i < popovEggs.Length; i++)
        {
            popovEggs[i].fillAmount=i<fullEggMaxIndex ? 1 : i>fullEggMaxIndex?0: (data[index].Timer % popovEggTime) / popovEggTime;
        }
    }

    private void UpdateAllTimers()
    {
        for (var i = 0; i < data.Length; i++)
        {
            if (data[i].IsReady) continue;
            data[i].Timer += Time.deltaTime;
            data[i].Timer = Mathf.Clamp(data[i].Timer, 0, data[i].CulDownTime);
            if (Math.Abs(data[i].Timer - data[i].CulDownTime) < 1e-3)
            {
                data[i].IsReady = true;
                audioController.Play(AudioSources.UIFX, data[i].ReadyClip, AudioMixerOutputGroups.SilentClips);
            }
            if (data[i].Name=="Popov")
            {
                FillPopovEggs();
                continue;
            }
            inactiveIcons[i].fillAmount = 1 - (data[i].Timer / data[i].CulDownTime);
        }
    }

    void Update()
    {
        UpdateAllTimers();
        if (Input.GetKeyDown(KeyCode.E))
        {
            CallTitor();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CallPopov();
        }
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

    private void CallPopov()
    {
        const int Index = (int)AbilityIndex.Popov;
        if (data[Index].Timer>=popovEggTime)
        {
            ToggleChooseScreen(popovChooseScreen);
            var cucumberButton = FindCucumberButton();
            var buttonComponent = cucumberButton.GetComponent<Button>();
            var image = cucumberButton.GetComponent<Image>();
            if (!data[Index].IsReady)
            {
                buttonComponent.enabled = false;
                image.color=Color.gray;
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
        Instantiate(popovWithCucumber, popovSpawnPoint.position, Quaternion.identity);
        data[(int)AbilityIndex.Popov].Timer =0;
        data[(int)AbilityIndex.Popov].IsReady = false;
    }

    public void EggAttack()
    {
        Instantiate(popovWithEgg, popovSpawnPoint.position, Quaternion.identity);
        data[(int)AbilityIndex.Popov].Timer -= popovEggTime;
        data[(int)AbilityIndex.Popov].IsReady = false;
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
        public AbilityData(float culDownTime, string name, FXClips readyClip)
        {
            Timer = 0;
            CulDownTime = culDownTime;
            Name = name;
            ReadyClip = readyClip;
            IsReady = false;
        }

        public string Name;
        public float Timer;
        public float CulDownTime;
        public FXClips ReadyClip;
        public bool IsReady;

    }
}
