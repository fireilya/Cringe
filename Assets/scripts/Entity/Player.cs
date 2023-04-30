using System;
using Assets.scripts;
using System.Collections;
using System.Linq;
using Assets.scripts.Enums;
using UnityEngine;
using UnityEngine.VFX;
using Quaternion = UnityEngine.Quaternion;
using Timer = Assets.scripts.Timer;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField]
    private HealthManager healthMenager;
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float currentSpeed;
    private float gyperJumpSpeed = 19f;

    [SerializeField]
    private VisualEffect boostVFX;
    private float boost = 35f;
    private float gyperJumpCulDownTime = 0.2f;

    [SerializeField]
    private Timer mainTimer;

    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject bullet;

    private Rigidbody2D playerRB;
    [SerializeField]
    private HitFromTrigger[] playerTriggers;
    private float highLimit=4.74f;
    private float lowLimit = -4.04f;
    private float leftLimit = -8.34f;
    private float rightLimit= 8.34f;
    private bool isMovementBlocked;
    private Timer unhitableTimer;
    private Timer gyperJumpCulDownTimer;
    private Timer bulletTimer;
    private float repulsionForce = 20f;
    private float repulsionSlowing = 40f;
    private Vector2 repulsionVector;
    [SerializeField]
    private AudioController audioController;

    [SerializeField]
    private Transform bulletSpawnerTransform;

    [SerializeField]
    private RocketController rocketController;
    private string[] goodTags = {
        "rocket",
        "bullet",
    };

    private string[] bonusTags =
    {
        "FullRockets",
        "FullHealth"
    };

    [SerializeField]
    private Sprite unhittablePlayerSprite;
    [SerializeField]
    private Sprite normalPlayerSprite;
    private SpriteRenderer playerSpriteRenderer;

    private void KeepInBorder()
    {
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.localPosition.y, lowLimit, highLimit),
            transform.localPosition.z);
    }

    private Animator playerAnimator;

    public HitFromTrigger[] GetHitFromTriggers()
    {
        return playerTriggers;
    }

    void OnEnable()
    {
        unhitableTimer = Instantiate(mainTimer);
        gyperJumpCulDownTimer = Instantiate(mainTimer);
        bulletTimer = Instantiate(mainTimer);
        currentSpeed = speed;
        playerRB=GetComponent<Rigidbody2D>();
        playerAnimator=GetComponent<Animator>();
        playerSpriteRenderer=GetComponent<SpriteRenderer>();
        playerSpriteRenderer.sprite = normalPlayerSprite;
        unhitableTimer.StartTimer(1.0f);
        StartCoroutine(ShowUnhittablePlayer());
    }

    void OnTriggerStay2D(Collider2D collider)
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
        audioController.Play(AudioSources.PlayerFX,FXClips.Hit, AudioMixerOutputGroups.SilentClips);
        StartCoroutine(ShowUnhittablePlayer());
        gameController.Hit();
        for (var i = 0; i < playerTriggers.Length; i++)
        {
            if (playerTriggers[i].isLocked) continue;
            repulsionVector=ConvertSideToVector((HitSide)i);
            break;
        }
        Debug.Log("HOT!!!"+repulsionVector);
        currentSpeed=repulsionForce;
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
        var colliderTag= collider.tag;
        return goodTags.Any(x=>x==colliderTag) 
            ? ColliderIdentifier.good 
            : bonusTags.Any(x=>x==colliderTag)
                ? ColliderIdentifier.bonus
                : ColliderIdentifier.bad;
    }

    void FixedUpdate()
    {
        if(isMovementBlocked)
        {
            DoRepulsion();
            return;
        }
        GetFixedInput();
    }

    void Update()
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
            currentSpeed-=boost*Time.deltaTime;
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

    void GetFixedInput()
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
        
        if (Input.GetMouseButton(1) && !rocketController.IsEmpty)
        {
            rocketController.LaunchRocket();
        }

        if (Input.GetMouseButton(0) && bulletTimer.IsEnded)
        {
            Instantiate(bullet, bulletSpawnerTransform.position, Quaternion.identity);
            audioController.Play(AudioSources.PlayerFX, FXClips.PlayerLaserShoot);
            bulletTimer.StartTimer(0.1f);
        }
    }
}
