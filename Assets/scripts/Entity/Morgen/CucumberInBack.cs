using System.Collections;
using UnityEngine;

public class CucumberInBack : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private Sprite commonCucumber;

    [SerializeField]
    private GameObject explode;

    [SerializeField]
    private Morgen morgen;

    [SerializeField]
    private Sprite redCucumber;

    private SpriteRenderer spriteRenderer;

    public IEnumerator Explode()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        for (var i = 0; i < 3; i++)
        {
            spriteRenderer.sprite = redCucumber;
            audioSource.Play();
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = commonCucumber;
            yield return new WaitForSeconds(0.5f);
        }

        Instantiate(explode, transform.position, Quaternion.identity);
        morgen.Hit(200);
        gameObject.SetActive(false);
    }
}