using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Popov : MonoBehaviour
{
    [SerializeField]
    private GameObject cucumber;

    [SerializeField]
    private GameObject egg;
    [SerializeField] 
    private Transform eggSpawnPoint;

    private Transform morgenBack;

    private GameObject cucumberInMorgen;

    private bool isPursuit;

    private bool isPursuitEnded;

    private Vector3 returnCoordinates;
    private AudioSource audioSource;

    [SerializeField]
    private GameObject popovOut;

    [SerializeField]
    private float moveSpeed = 10f;

    void Start()
    {
        morgenBack=GameObject.Find("MorgenBack").GetComponent<Transform>();
        cucumberInMorgen=GameObject.Find("cucumberInMorgenBack");
        audioSource = GetComponent<AudioSource>();
        returnCoordinates=transform.position;
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
        cucumberInMorgen.SetActive(true);
        audioSource.Play();
        isPursuit=false;
        isPursuitEnded = true;
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
            if (!audioSource.isPlaying&&transform.position==returnCoordinates)
            {
                isPursuitEnded = false;
                Instantiate(popovOut);
            }
        }
    }
}
