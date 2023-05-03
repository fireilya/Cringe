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
    private Image inactiveTitorIcon;

    [SerializeField]
    private GameObject titorChooseScreen;

    private float titorTimer;
    private float titorCulDownTime = 100f;
    private float popovTimer;
    private float popovCulDownTime = 90f;
    private float chooseTimeScale = 0.05f;
    void Start()
    {
        
    }

    public void ApplyTimerBoostBonus(Ability ability)
    {
        switch (ability)
        {
            case Ability.Titor:
                titorTimer += 20f;
                break;
        }
    }
    void Update()
    {
        titorTimer += Time.deltaTime;
        titorTimer = Mathf.Clamp(titorTimer, 0, titorCulDownTime);
        inactiveTitorIcon.fillAmount = 1 - (titorTimer / titorCulDownTime);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!(Math.Abs(titorTimer - titorCulDownTime) < 1e-3))
            {
                warningController.ThrowWarning(WarningType.TitorNotReady);
                return;
            }

            if (Math.Abs(Time.timeScale - 1) < 1e-3)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = chooseTimeScale;
                titorChooseScreen.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                titorChooseScreen.SetActive(false);
            }
        }
    }

    public void ReturnToGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        titorChooseScreen.SetActive(false);
    }

    public void FireSupport()
    {
        Instantiate(titorWithRockets, SpawnPoint.position, Quaternion.identity);
        titorTimer = 0;
    }

    public void SupplySupport()
    {
        Instantiate(titorWithBunuses, SpawnPoint.position, Quaternion.identity);
        titorTimer = 0;
    }
}
