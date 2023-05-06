using System;
using System.Collections;
using System.Linq;
using Assets.scripts;
using Assets.scripts.Enums;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    [SerializeField]
    private AbilityController abilityController;

    [SerializeField]
    private AudioController audioController;

    private readonly string[] bonusTags =
    {
        "FullRockets",
        "FullHealth",
        "MegaHealth",
        "TitorBoost",
        "PopovBoost",
        "CleanerBoost"
    };

    private readonly float boost = 35f;

    [SerializeField]
    private VisualEffect boostVFX;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform bulletSpawnerTransform;

    private Timer bulletTimer;

    [SerializeField]
    private float currentSpeed;

    [SerializeField]
    private GameController gameController;

    private readonly string[] goodTags =
    {
        "rocket",
        "bullet",
        "Titor",
        "Popov",
        "Egg",
        "CleaningShield"
    };

    private readonly float gyperJumpCulDownTime = 0.2f;
    private Timer gyperJumpCulDownTimer;
    private readonly float gyperJumpSpeed = 19f;

    [SerializeField]
    private HealthManager healthMenager;

    private readonly float highLimit = 4.74f;
    private bool isMovementBlocked;
    private readonly float leftLimit = -8.34f;
    private readonly float lowLimit = -4.04f;

    [SerializeField]
    private Timer mainTimer;

    [SerializeField]
    private Sprite normalPlayerSprite;

    private Animator playerAnimator;

    private Rigidbody2D playerRB;
    private SpriteRenderer playerSpriteRenderer;

    [SerializeField]
    private HitFromTrigger[] playerTriggers;

    private readonly float repulsionForce = 20f;
    private readonly float repulsionSlowing = 40f;
    private Vector2 repulsionVector;
    private readonly float rightLimit = 8.34f;

    [SerializeField]
    private RocketController rocketController;

    [SerializeField]
    private readonly float speed = 5.0f;

    private Timer unhitableTimer;

    [SerializeField]
    private Sprite unhittablePlayerSprite;

    private void KeepInBorder()
    {
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.localPosition.y, lowLimit, highLimit),
            transform.localPosition.z);
    }

    public HitFromTrigger[] GetHitFromTriggers()
    {
        return playerTriggers;
    }

    private void OnEnable()
    {
        unhitableTimer = Instantiate(mainTimer);
        gyperJumpCulDownTimer = Instantiate(mainTimer);
        bulletTimer = Instantiate(mainTimer);
        currentSpeed = speed;
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerSpriteRenderer.sprite = normalPlayerSprite;
        unhitableTimer.StartTimer(1.0f);
        StartCoroutine(ShowUnhittablePlayer());
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        var colliderIdentifier = IdentifyCollider(collider);
        switch (colliderIdentifier)
        {
            case ColliderIdentifier.good:
                return;
            case ColliderIdentifier.bad:
                Hit();
                break;
            case ColliderIdentifier.bonus:
                ApplyBonus(collider.tag);
                Destroy(collider.gameObject);
                break;
        }
    }

    private void ApplyBonus(string bonus)
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
                abilityController.ApplyTimerBoostBonus(AbilityIndex.Titor, 20f);
                break;
            case "PopovBoost":
                abilityController.ApplyTimerBoostBonus(AccumulateAbilityIndex.Popov, 15f);
                break;
            case "CleanerBoost":
                abilityController.ApplyTimerBoostBonus(AccumulateAbilityIndex.CleaningShield, 20f);
                break;
        }

        audioController.Play(AudioSources.BonusFX, FXClips.Bonus, AudioMixerOutputGroups.SilentClips);
    }

    private IEnumerator ShowUnhittablePlayer()
    {
        while (!unhitableTimer.IsEnded)
        {
            playerSpriteRenderer.sprite = unhittablePlayerSprite;
            yield return new WaitForSeconds(0.03f);
            playerSpriteRenderer.sprite = normalPlayerSprite;
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void Hit()
    {
        if (!unhitableTimer.IsEnded) return;
        unhitableTimer.StartTimer(1.0f);
        audioController.Play(AudioSources.PlayerFX, FXClips.Hit, AudioMixerOutputGroups.SilentClips);
        StartCoroutine(ShowUnhittablePlayer());
        gameController.Hit();
        for (var i = 0; i < playerTriggers.Length; i++)
        {
            if (playerTriggers[i].isLocked) continue;
            repulsionVector = ConvertSideToVector((HitSide)i);
            break;
        }

        Debug.Log("HOT!!!" + repulsionVector);
        currentSpeed = repulsionForce;
        isMovementBlocked = true;
        playerAnimator.SetTrigger("Repulse");
        playerRB.velocity = repulsionVector * currentSpeed;
    }

    private Vector2 ConvertSideToVector(HitSide side)
    {
        switch (side)
        {
            case HitSide.FromTop:
                return Vector2.down;
            case HitSide.FromBottom:
                return Vector2.up;
            case HitSide.FromLeft:
                return Vector2.right;
            case HitSide.FromRight:
                return Vector2.left;
        }

        return new Vector2(0, 0);
    }

    private ColliderIdentifier IdentifyCollider(Collider2D collider)
    {
        var colliderTag = collider.tag;
        return goodTags.Any(x => x == colliderTag)
            ? ColliderIdentifier.good
            : bonusTags.Any(x => x == colliderTag)
                ? ColliderIdentifier.bonus
                : ColliderIdentifier.bad;
    }

    private void FixedUpdate()
    {
        if (isMovementBlocked)
        {
            DoRepulsion();
            return;
        }

        GetFixedInput();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Math.Abs(Time.timeScale - 1) < 1e-3)
            {
                gameController.Pause();
                isMovementBlocked = true;
            }
            else
            {
                gameController.Resume();
                isMovementBlocked = false;
            }
        }

        if (isMovementBlocked) return;
        if (currentSpeed > speed)
        {
            currentSpeed -= boost * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, speed, float.MaxValue);
        }

        GetInput();
    }

    private void DoRepulsion()
    {
        playerRB.velocity = repulsionVector * currentSpeed;
        KeepInBorder();
        currentSpeed -= repulsionSlowing * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, float.MaxValue);
        if (currentSpeed != 0) return;
        isMovementBlocked = false;
        currentSpeed = speed;
    }

    private void GetFixedInput()
    {
        var velocityVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            var direction = Input.GetKey(KeyCode.W) ? 1 : -1;
            velocityVector += new Vector3(0, 1, 0) * direction;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            var direction = Input.GetKey(KeyCode.D) ? 1 : -1;
            velocityVector += new Vector3(1, 0, 0) * direction;
        }

        playerRB.velocity = velocityVector.normalized * currentSpeed;
        KeepInBorder();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gyperJumpCulDownTimer.IsEnded)
        {
            currentSpeed = gyperJumpSpeed;
            boostVFX.Play();
            gyperJumpCulDownTimer.StartTimer(gyperJumpCulDownTime);
            unhitableTimer.StartTimer(0.35f);
        }

        if (Input.GetMouseButtonDown(1)) rocketController.LaunchRocket();

        if (Input.GetMouseButton(0) && bulletTimer.IsEnded)
        {
            Instantiate(bullet, bulletSpawnerTransform.position, Quaternion.identity);
            audioController.Play(AudioSources.PlayerFX, FXClips.PlayerLaserShoot);
            bulletTimer.StartTimer(0.1f);
        }
    }
}