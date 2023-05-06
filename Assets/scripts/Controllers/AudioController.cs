using Assets.scripts.Enums;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] audioSources;

    [SerializeField]
    private AudioClip[] musicAudioClips;

    [SerializeField]
    private AudioClip[] FXAudioClips;
    [SerializeField]
    private AudioMixerGroup[] mixerGroups;

    public void PlayFX(AudioSources source, FXClips FX, AudioMixerOutputGroups group=AudioMixerOutputGroups.NormalClips)
    {
        var requiredAudioSource = audioSources[(int)source];
        if (requiredAudioSource.gameObject.activeSelf)
        {
            requiredAudioSource.outputAudioMixerGroup = mixerGroups[(int)group];
            requiredAudioSource.clip = FXAudioClips[(int)FX];
            requiredAudioSource.Play();
        }
    }

    public void PlayMusic(Music music, AudioMixerOutputGroups group = AudioMixerOutputGroups.NormalClips)
    {
        var requiredAudioSource = audioSources[(int)AudioSources.Music];
        if (requiredAudioSource.gameObject.activeSelf)
        {
            requiredAudioSource.outputAudioMixerGroup = mixerGroups[(int)group];
            requiredAudioSource.clip = musicAudioClips[(int)music];
            requiredAudioSource.Play();
        }
    }
}
