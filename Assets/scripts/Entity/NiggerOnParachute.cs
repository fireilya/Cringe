using System.Collections;
using Assets.scripts;
using UnityEngine;
using UnityEngine.Audio;
using Random = System.Random;

public class NiggerOnParachute : MonoBehaviour
{
    [SerializeField]
    private MissileData data;

    [SerializeField]
    private GameObject[] bonuses;

    [SerializeField]
    private GameObject laughIcon;

    [SerializeField] 
    private AudioClip niggerHitClip;
    [SerializeField]
    private AudioClip niggerLaughClip;
    [SerializeField]
    private AudioClip niggerFallingClip;

    private AudioSource niggerAudioSource;
    private Random rnd=new();

    [SerializeField]
    private Timer lifeTimer;
    private Random rand=new();
    private int health = 5;
    private SpriteRenderer niggerSpriteRenderer;

    [SerializeField]
    private Sprite hittedNiggerSprite;

    [SerializeField]
    private AudioMixerGroup normalClips;

    private Sprite normalNiggerSprite;
    private Rigidbody2D niggerRB;
    public bool onParachute = true;

    void OnTriggerEnter2D(Collider2D collider)  
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
        niggerAudioSource.clip= niggerFallingClip;
        niggerAudioSource.spatialBlend = 1f;
        niggerAudioSource.Play();
    }

    private IEnumerator SetHitImage()
    {
        niggerSpriteRenderer.sprite = hittedNiggerSprite;
        yield return new WaitForSeconds(0.05f);
        niggerSpriteRenderer.sprite= normalNiggerSprite;
    }

    void Start()
    {
        niggerRB = GetComponent<Rigidbody2D>();
        lifeTimer.StartTimer(10f);
        niggerSpriteRenderer=GetComponent<SpriteRenderer>();
        normalNiggerSprite=niggerSpriteRenderer.sprite;
        niggerAudioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DoRandomLaugh();
        if (health<=0)
        {
            var nextBonus = rand.Next();
            var accessDouble = rand.NextDouble();
            if (accessDouble > 0.5 || bonuses[nextBonus % bonuses.Length].tag=="MegaHealth" && accessDouble>0.85)
            { 
                Instantiate(bonuses[nextBonus % bonuses.Length], transform.localPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        if (lifeTimer.IsEnded)
        {
            Destroy(gameObject);
        }
    }
}
