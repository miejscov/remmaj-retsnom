using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceAudioScript : MonoBehaviour {

	public AudioClip OpenGateAudio;
	public AudioClip CloseGateAudio;
	private AudioSource _audioSource;
	
	private void Start ()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void PlayOpeningGateSound()
	{
		StopAudio();
		_audioSource.clip = OpenGateAudio;
		_audioSource.loop = true;
		_audioSource.Play();
	}

	public void PlayClosingGateSound()
	{
		StopAudio();
		_audioSource.clip = CloseGateAudio;
		_audioSource.Play();
	}

	public void StopAudio()
	{
		_audioSource.Stop();
	}
}
