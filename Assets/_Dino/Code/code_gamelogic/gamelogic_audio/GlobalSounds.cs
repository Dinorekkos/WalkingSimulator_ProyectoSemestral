
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class GlobalSounds
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
    public bool loop;
    [Range(0.1f,3f)]public float pitch;
    [Range(0f, 1f)] [SerializeField] private float _volume;
    [Range(0f, 1f)] [SerializeField] public float spacialBlend;
    public float minDistance;
    public float maxDistance;
    public float delayOfLoop;
    
    
    
    [HideInInspector] public AudioSource source;

}
