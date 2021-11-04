using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNotes : MonoBehaviour
{
    private AudioManagerPuzzle audioManagerPuzzle;
    public string clipName;
    
    void Start()
    {
        audioManagerPuzzle = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerPuzzle>();
    }

    public void ReproduceAudio()
    {
        audioManagerPuzzle.Play(clipName);
    }
}
