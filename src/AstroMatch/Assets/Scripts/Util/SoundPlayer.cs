using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum SoundClips
{
    Countdown,
    Move,
    Match,
    End
}

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip countdownSound;
    [SerializeField] private float countdownSoundVolume;
    [SerializeField] private AudioClip[] moveSounds;
    [SerializeField] private float moveSoundVolume;
    [SerializeField] private AudioClip[] matchSounds;
    [SerializeField] private float matchSoundVolume;
    [SerializeField] private AudioClip endGameSound;
    [SerializeField] private float endGameSoundVolume;

    private AudioSource gameAudioSource;

    public static SoundPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                
            }
            return instance;
        }
    }
    private static SoundPlayer instance;

    private void Awake()
    {
        instance = this;
        gameAudioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(SoundClips clip)
    {
        switch (clip)
        {
            case SoundClips.Countdown:
                gameAudioSource.PlayOneShot(countdownSound, countdownSoundVolume);
                break;
            case SoundClips.End:
                gameAudioSource.PlayOneShot(endGameSound, endGameSoundVolume);
                break;
            case SoundClips.Match:
                gameAudioSource.PlayOneShot(matchSounds[Random.Range(0, matchSounds.Length)], matchSoundVolume);
                break;
            case SoundClips.Move:
                gameAudioSource.PlayOneShot(moveSounds[Random.Range(0, moveSounds.Length)], moveSoundVolume);
                break;
        }
    }
}
