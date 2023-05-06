using UnityEngine;
using UnityEngine.Playables;
using Random = System.Random;

public class InstaSamkaController : MonoBehaviour
{
    private PlayableDirector[] instaSamkaDirectors;

    [SerializeField]
    private InstaSamka[] instaSamkas;

    private readonly Random rnd = new();

    private void Start()
    {
        instaSamkaDirectors = new PlayableDirector[instaSamkas.Length];
        for (var i = 0; i < instaSamkas.Length; i++)
            instaSamkaDirectors[i] = instaSamkas[i].GetComponent<PlayableDirector>();
    }

    public void DoHit1(string message)
    {
        instaSamkaDirectors[0].Play();
        instaSamkas[0].message = message;
    }

    public void DoHit2(string message)
    {
        instaSamkaDirectors[1].Play();
        instaSamkas[1].message = message;
    }

    public void DoHit3(string message)
    {
        instaSamkaDirectors[2].Play();
        instaSamkas[2].message = message;
    }

    public void DoRandomHit(string message)
    {
        var nextPlayable = rnd.Next() % instaSamkas.Length;
        instaSamkaDirectors[nextPlayable].Play();
        instaSamkas[nextPlayable].message = message;
    }
}