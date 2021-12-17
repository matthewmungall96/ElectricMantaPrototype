using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer;

    public void SetMusicSound(float soundLevel)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(soundLevel) * 20);
        Debug.Log("Music Volume is currently at " + musicMixer);
    }    
    
    public void SetSFXSound(float soundLevel)
    {
        musicMixer.SetFloat("SFXVol", Mathf.Log10(soundLevel) * 20);
    }

}
