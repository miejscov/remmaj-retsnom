using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlAudioScript : MonoBehaviour
{
	public AudioClip MusicSettingOneAudioClip;
	public AudioClip MusicSettingTwoAudioClip;
	public AudioClip PlayerInExitAudioClip;
//	public AudioClip GameOverAudioClip;
//	public AudioClip MainMenuAudioClip;

//	private AudioClip _currentClip;
	private AudioSource _audio;

	private void Awake()
	{
		_audio = GetComponent<AudioSource>();
	}
	
	public void PlayAudioPlayerInExit()
	{
		_audio.Stop();
		_audio.volume = 1f;
		_audio.PlayOneShot(PlayerInExitAudioClip);
	}
//
//	public void PlayGameOverCanvasAudioCLip()
//	{
//		_audio.Stop();
//		_audio.PlayOneShot(GameOverAudioClip);
//	}
//
//	public void PlayMainMenuAudioCLip()
//	{
//		_audio.Stop();
//		_audio.PlayOneShot(MainMenuAudioClip);
//	}

	public void PlaySettingAudioClip(int setting)
	{
		switch (setting)  //zrobię to ładniej jak ogarnę sterowanie wsyztskimi dźwękami
		{
			case 1:
				_audio.volume = .3f;
				_audio.clip = MusicSettingOneAudioClip;
				_audio.loop = true;
				if (_audio.isPlaying == false) _audio.Play();
				break;
			case 2:
				_audio.volume = .3f;
				_audio.clip = MusicSettingTwoAudioClip;
				_audio.loop = true;
				_audio.Play();
				break;
		}
//		_audio.clip = MusicSettingOneAudioClip;
//		_audio.loop = true;
//		_audio.Play();
	}
}
