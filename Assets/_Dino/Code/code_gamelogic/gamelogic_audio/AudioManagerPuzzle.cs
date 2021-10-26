using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManagerPuzzle : MonoBehaviour
{
    public SoundsPuzzle[] sounds;
    private void Awake() 
    {
        foreach(SoundsPuzzle s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = s.AudioMixerGroup;
        }
    }
    private void Start()
    {
        
    }
    public void Play(string name)
    {
        SoundsPuzzle s = Array.Find(sounds,sound =>sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void PlayArray(string[] soundsArray)
    {
        
    }

    


}
