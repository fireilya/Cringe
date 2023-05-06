using UnityEngine;

public class PostAudioSource : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!source.isPlaying) Destroy(gameObject);
    }
}