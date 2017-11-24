using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlAudioScript : MonoBehaviour
{


	public AudioClip MusicSettingOneAudioClip;
	public AudioClip PlayerInExitAudioClip;
	public AudioClip GameOverAudioClip;
	public AudioClip MainMenuAudioClip;

//	private AudioClip _currentClip;
	private AudioSource _audio;

	private void Awake()
	{
		_audio = GetComponent<AudioSource>();
	}
	
	public void PlayPlayerInExitAudioClip()
	{
		_audio.Stop();
		_audio.PlayOneShot(PlayerInExitAudioClip);
	}

	public void PlayGameOverCanvasAudioCLip()
	{
		_audio.Stop();
		_audio.PlayOneShot(GameOverAudioClip);
	}

	public void PlayMainMenuAudioCLip()
	{
		_audio.Stop();
		_audio.PlayOneShot(MainMenuAudioClip);
	}

	public void PlaySettingAudioClip(int setting)
	{
		if (setting == 1)
		{
			_audio.Stop();
			_audio.volume = .3f;
			_audio.clip = MusicSettingOneAudioClip;
			_audio.loop = true;
			_audio.Play();
		}
	}
}
