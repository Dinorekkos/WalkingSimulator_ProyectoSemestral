using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Looping3DSound : MonoBehaviour
{
	
	[SerializeField] private Letter memorie;
	
	public float delay = 2;

	public bool CanPlayAudio
	{
		get { return canPlayAudio; }
		set { canPlayAudio = value; }
	}
	bool canPlayAudio;
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

	private void Start()
	{
		CanPlayAudio = true;
	}

	void Update()
	{
		if (CanPlayAudio)
		{
			if (!memorie.ReadLetter)
			{
				elapsedTime += Time.deltaTime;

				if (elapsedTime >= cliplength + delay)
				{
					elapsedTime = 0;
					Source.Play();
				}
			}
			else
			{
				CanPlayAudio = false;
			}
		}
		
	}
}
