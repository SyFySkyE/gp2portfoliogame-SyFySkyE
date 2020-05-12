using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs;

    private AudioSource musicPlayer;
    private float songLength;

    // Start is called before the first frame update
    void Start()
    {
        songLength = 0;
        DontDestroyOnLoad(this.gameObject);
        if (FindObjectsOfType<RandomAudioPlayer>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        musicPlayer = GetComponent<AudioSource>();
        PlayRandomSong();
    }

    private void PlayRandomSong()
    {
        musicPlayer.clip = songs[Random.Range(0, songs.Length)];
        songLength = musicPlayer.clip.length;
        musicPlayer.Play();
    }

    private void Update()
    {
        songLength -= Time.deltaTime;
        if (songLength <= 0)
        {
            PlayRandomSong();
        }
    }
}
