using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Looping3DSound : MonoBehaviour
{

	[SerializeField] private bool canPlayAudio;
	public float delay = 2;

	public bool CanPlayAudio
	{
		get { return canPlayAudio; }
		set { canPlayAudio = value; }
	}
	
	AudioSource Source;
	AudioClip Clip;
	float cliplength;
	float elapsedTime;
	
	
	void Awake()
	{
		Source = GetComponent<AudioSource>();
		Clip = Source.clip;
		cliplength = Clip.length;
	}
	void Update()
	{
		if (CanPlayAudio)
		{
			elapsedTime += Time.deltaTime;
			if(elapsedTime >= cliplength + delay)
			{
				elapsedTime = 0;
				Source.Play();
			}
		}
		
	}
}
