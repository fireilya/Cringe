using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Popov : MonoBehaviour
{
    [SerializeField]
    private GameObject cucumber;

    [SerializeField]
    private Egg egg;
    [SerializeField] 
    private Transform eggSpawnPoint;

    private Transform morgenBack;

    private CucumberInBack cucumberInMorgen;

    private bool isPursuit;

    private bool isPursuitEnded;

    private Vector3 returnCoordinates;
    private AudioSource audioSource;

    [SerializeField]
    private GameObject popovOut;

    private Egg spawnedEgg;

    [SerializeField]
    private float moveSpeed = 10f;

    void Start()
    {
        morgenBack=GameObject.Find("MorgenBack").GetComponent<Transform>();
        cucumberInMorgen = GameObject.FindGameObjectWithTag("cucumberInMorgen").transform.GetChild(0).GetComponent<CucumberInBack>();
        audioSource = GetComponent<AudioSource>();
        returnCoordinates=transform.position;
    }

    public void SpawnEgg()
    {
        spawnedEgg = Instantiate(egg, eggSpawnPoint.position, Quaternion.identity);
    }

    private void OutPopov()
    {
        Instantiate(popovOut, transform.position, Quaternion.identity);
    }
    public void LaunchEgg()
    {
        spawnedEgg.Launch();
        OutPopov();
    }

    public void ToggleCucumber()
    {
        cucumber.SetActive(true);
    }

    public void StartPursuit()
    {
        isPursuit=true;
    }

    private void EndPursuit()
    {
        cucumberInMorgen.gameObject.SetActive(true);
        cucumber.SetActive(false);
        audioSource.Play();
        isPursuit=false;
        isPursuitEnded = true;
        StartCoroutine(cucumberInMorgen.Explode());
    }

    private IEnumerator OutPopovWithCucumber()
    {
        yield return new WaitForSeconds(3.0f);
        Instantiate(popovOut, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (isPursuit)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, morgenBack.position, moveSpeed * Time.deltaTime);
            if (transform.position==morgenBack.position)
            {
                EndPursuit();
            }
        }

        if (isPursuitEnded)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, returnCoordinates, moveSpeed * Time.deltaTime);
            if (transform.position==returnCoordinates)
            {
                isPursuitEnded = false;
                StartCoroutine(OutPopovWithCucumber());
            }
        }
    }
}
