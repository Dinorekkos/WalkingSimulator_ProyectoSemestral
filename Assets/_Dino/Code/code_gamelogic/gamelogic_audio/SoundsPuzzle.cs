using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundsPuzzle
{
    public float volume
    {
        get { return _volume; }
        set { 
            _volume = value;
            source.volume = value;
        }
    }
    public string name;
    public AudioClip clip;
    public AudioMixerGroup AudioMixerGroup;
    [Range(0.1f,3f)]public float pitch;
    [Range(0f, 1f)] [SerializeField] private float _volume;
    
    [HideInInspector] public AudioSource source;
 
}
