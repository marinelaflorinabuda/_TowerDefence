using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public AudioMixer gameAudioMixer;

    private const string _musicVolumeParameter = "MusicVolume";
    public void SetLevel(float sliderValue)
    {
        gameAudioMixer.SetFloat(_musicVolumeParameter, Mathf.Log10(sliderValue) * 20);
    }
}
