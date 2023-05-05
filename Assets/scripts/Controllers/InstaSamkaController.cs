using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Playables;
using Random = System.Random;

public class InstaSamkaController : MonoBehaviour
{
    [SerializeField]
    private InstaSamka[] instaSamkas;
    private Random rnd=new();
    private PlayableDirector[] instaSamkaDirectors;

    void Start()
    {
        instaSamkaDirectors = new PlayableDirector[instaSamkas.Length];
        for (int i = 0; i < instaSamkas.Length; i++)
        {
            instaSamkaDirectors[i] = instaSamkas[i].GetComponent<PlayableDirector>();
            
        }
    }

    public void DoHit1(string message)
    {
        instaSamkaDirectors[0].Play();
        instaSamkas[0].message=message;
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
