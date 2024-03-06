using System.Collections;
using UnityEngine;

public class Popov : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private GameObject cucumber;

    private CucumberInBack cucumberInMorgen;

    [SerializeField]
    private Egg egg;

    [SerializeField]
    private Transform eggSpawnPoint;

    private bool isPursuit;

    private bool isPursuitEnded;

    private Transform morgenBack;

    [SerializeField]
    private readonly float moveSpeed = 10f;

    [SerializeField]
    private GameObject popovOut;

    private Vector3 returnCoordinates;

    private Egg spawnedEgg;

    private void Start()
    {
        morgenBack = GameObject.Find("MorgenBack").GetComponent<Transform>();
        cucumberInMorgen = GameObject.FindGameObjectWithTag("cucumberInMorgen").transform.GetChild(0)
            .GetComponent<CucumberInBack>();
        audioSource = GetComponent<AudioSource>();
        returnCoordinates = transform.position;
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
        isPursuit = true;
    }

    private void EndPursuit()
    {
        cucumberInMorgen.gameObject.SetActive(true);
        cucumber.SetActive(false);
        audioSource.Play();
        isPursuit = false;
        isPursuitEnded = true;
        StartCoroutine(cucumberInMorgen.Explode());
    }

    private IEnumerator OutPopovWithCucumber()
    {
        yield return new WaitForSeconds(3.0f);
        Instantiate(popovOut, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (isPursuit)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, morgenBack.position, moveSpeed * Time.deltaTime);
            if (transform.position == morgenBack.position) EndPursuit();
        }

        if (isPursuitEnded)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, returnCoordinates, moveSpeed * Time.deltaTime);
            if (transform.position == returnCoordinates)
            {
                isPursuitEnded = false;
                StartCoroutine(OutPopovWithCucumber());
            }
        }
    }
}