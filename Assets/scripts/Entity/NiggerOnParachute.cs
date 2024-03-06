using System.Collections;
using Assets.scripts;
using UnityEngine;
using UnityEngine.Audio;
using Random = System.Random;

public class NiggerOnParachute : MonoBehaviour
{
    private readonly Random rand = new();
    private readonly Random rnd = new();

    [SerializeField]
    private GameObject[] bonuses;

    [SerializeField]
    private MissileData data;

    private int health = 5;

    [SerializeField]
    private Sprite hittedNiggerSprite;

    [SerializeField]
    private GameObject laughIcon;

    [SerializeField]
    private Timer lifeTimer;

    private AudioSource niggerAudioSource;

    [SerializeField]
    private AudioClip niggerFallingClip;

    [SerializeField]
    private AudioClip niggerHitClip;

    [SerializeField]
    private AudioClip niggerLaughClip;

    private Rigidbody2D niggerRB;
    private SpriteRenderer niggerSpriteRenderer;

    [SerializeField]
    private AudioMixerGroup normalClips;

    private Sprite normalNiggerSprite;
    public bool onParachute = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (data.DamageData.ContainsKey(collider.tag))
        {
            health -= data.DamageData[collider.tag];
            StartCoroutine(SetHitImage());
            if (!onParachute) return;
            niggerAudioSource.volume = 1f;
            niggerAudioSource.clip = niggerHitClip;
            niggerAudioSource.Play();
        }
    }

    private IEnumerator ShowLaughIcon()
    {
        laughIcon.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        laughIcon.SetActive(false);
    }

    private void DoRandomLaugh()
    {
        if (niggerAudioSource.isPlaying || rnd.Next() % 3912 != 0) return;
        niggerAudioSource.volume = 0.8f;
        niggerAudioSource.clip = niggerLaughClip;
        StartCoroutine(ShowLaughIcon());
        niggerAudioSource.Play();
    }

    public void Fall()
    {
        niggerRB.drag = 0;
        niggerAudioSource.volume = 1;
        niggerAudioSource.clip = niggerFallingClip;
        niggerAudioSource.spatialBlend = 1f;
        niggerAudioSource.Play();
    }

    private IEnumerator SetHitImage()
    {
        niggerSpriteRenderer.sprite = hittedNiggerSprite;
        yield return new WaitForSeconds(0.05f);
        niggerSpriteRenderer.sprite = normalNiggerSprite;
    }

    private void Start()
    {
        niggerRB = GetComponent<Rigidbody2D>();
        lifeTimer.StartTimer(10f);
        niggerSpriteRenderer = GetComponent<SpriteRenderer>();
        normalNiggerSprite = niggerSpriteRenderer.sprite;
        niggerAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        DoRandomLaugh();
        if (health <= 0)
        {
            var nextBonus = rand.Next();
            var accessDouble = rand.NextDouble();
            if (accessDouble > 0.5)
            {
                if (bonuses[nextBonus % bonuses.Length].tag == "MegaHealth" && accessDouble < 0.80) return;

                Instantiate(bonuses[nextBonus % bonuses.Length], transform.localPosition, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        if (lifeTimer.IsEnded) Destroy(gameObject);
    }
}