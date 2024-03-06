using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartCutScene : MonoBehaviour
{
    private VideoPlayer video;
    private bool isStarted=false;
    void Start()
    {
        video = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if (video.isPlaying && !isStarted)
        {
            isStarted=true;
        }
        if ((Input.anyKeyDown || !video.isPlaying) && isStarted)
        {
            SceneManager.LoadScene(1);
        }
    }
}
