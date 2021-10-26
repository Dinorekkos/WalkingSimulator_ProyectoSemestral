using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using  UnityEngine.UI;

public class AudioMixerManager : MonoBehaviour
{
    [Header("Slider Audio UI")] 
    [SerializeField] private Slider mainAudioSlider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private FloatVariables volume;
    void Start()
    {
        mainAudioSlider.value = volume.value;
    }

    private void Update()
    {
        SetAudioMixerVolume();
    }
    
    public void SetAudioMixerVolume()
    {
        audioMixer.SetFloat("MasterAudio", volume.value);
    }
}
