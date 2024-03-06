using System.Collections;
using UnityEngine;
using Random = System.Random;

public class MoralPressureController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] AngryNiggers;

    private readonly Random rnd = new();

    public IEnumerator DoMoralPressure()
    {
        var index = rnd.Next() % AngryNiggers.Length;
        var animator = AngryNiggers[index].GetComponent<Animator>();
        var audioSource = AngryNiggers[index].GetComponent<AudioSource>();
        var laughIcon = AngryNiggers[index].transform.GetChild(0);
        animator.SetTrigger("hitted");
        yield return new WaitForSeconds(0.5f);
        laughIcon.gameObject.SetActive(true);
        audioSource.Play();
        yield return new WaitForSeconds(1.0f);
        laughIcon.gameObject.SetActive(false);
        animator.SetTrigger("laughed");
    }
}