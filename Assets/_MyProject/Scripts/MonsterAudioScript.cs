using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAudioScript : MonoBehaviour {

    public AudioClip _audioAttack;
    public AudioClip _audioDie;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void PlayAttack()
    {

        _audioSource.clip = _audioAttack;
        _audioSource.time = 0.15f;
        _audioSource.Play();
 
    }

    public void PlayDie()
    {
        _audioSource.PlayOneShot(_audioDie);
    }

}
