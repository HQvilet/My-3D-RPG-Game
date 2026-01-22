using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public AudioSource backgroundAudioPlayer;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private AudioClip normalMusic; 
    
    public void SwitchToBossMusic()
    {
        backgroundAudioPlayer.Stop();
        backgroundAudioPlayer.clip = bossMusic;
        backgroundAudioPlayer.Play();
    }

    public void SwitchToNormalMusic()
    {
        backgroundAudioPlayer.Stop();
        backgroundAudioPlayer.clip = normalMusic;
        backgroundAudioPlayer.Play();
    }

    public void Stop() => backgroundAudioPlayer.Stop();

    public void Resume() => backgroundAudioPlayer.Play();
}
