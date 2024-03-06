using Assets.scripts.Enums;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] audioSources;

    [SerializeField]
    private AudioClip[] FXAudioClips;

    [SerializeField]
    private AudioMixerGroup[] mixerGroups;

    [SerializeField]
    private AudioClip[] musicAudioClips;

    public void Play(AudioSources source, FXClips FX, AudioMixerOutputGroups group = AudioMixerOutputGroups.NormalClips)
    {
        var requiredAudioSource = audioSources[(int)source];
        if (requiredAudioSource.gameObject.activeSelf)
        {
            requiredAudioSource.outputAudioMixerGroup = mixerGroups[(int)group];
            requiredAudioSource.clip = FXAudioClips[(int)FX];
            requiredAudioSource.Play();
        }
    }

    public void Play(AudioSources source, Music music,
        AudioMixerOutputGroups group = AudioMixerOutputGroups.NormalClips)
    {
        var requiredAudioSource = audioSources[(int)source];
        if (requiredAudioSource.gameObject.activeSelf)
        {
            requiredAudioSource.outputAudioMixerGroup = mixerGroups[(int)group];
            requiredAudioSource.clip = musicAudioClips[(int)music];
            requiredAudioSource.Play();
        }
    }
}