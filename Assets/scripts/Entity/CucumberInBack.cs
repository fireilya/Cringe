using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CucumberInBack : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite commonCucumber;

    [SerializeField]
    private Sprite redCucumber;

    [SerializeField]
    private GameObject explode;

    [SerializeField]
    private Morgen morgen;

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
