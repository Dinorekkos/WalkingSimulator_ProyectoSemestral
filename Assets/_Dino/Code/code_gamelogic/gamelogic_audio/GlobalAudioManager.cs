using UnityEngine;
using System;

public class GlobalAudioManager : MonoBehaviour
{
    
    public GlobalSounds[] sounds;
    private void Awake() 
    {
        foreach(GlobalSounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.AudioMixerGroup;
            s.source.spatialBlend = s.spacialBlend;
            s.source.maxDistance = s.maxDistance;
            s.source.minDistance = s.minDistance;
        }
    }
    private void Start()
    {
    }
    public void Play(string name)
    {
        GlobalSounds s = Array.Find(sounds,sound =>sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

}
