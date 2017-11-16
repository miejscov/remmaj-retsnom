﻿using UnityEngine;

public class PlayerStatusScript : MonoBehaviour
{
    private int _numberOfLives;
    private bool _isDead = false;
    public int _energy;
    private int _score;
    private int _diamonds;

    private const int _defaultNumberOfLives = 3;
    private const int _defaultEnergy = 5;
    private const int _defaultScore = 0;
    private const int _defaultDiamond = 0;

    private PlayerAudioControlScript _playerAudio;
    private CapsuleCollider _capsuleCollider;
    private SerializePlayerStatus _serializePlayer;

    private void Start()
    {
        _serializePlayer = GetComponent<SerializePlayerStatus>();
        _playerAudio = GetComponent<PlayerAudioControlScript>();

        if(ButtonContolScript.isStarting)
        {
            ResetPlayerStatus();
            ButtonContolScript.isStarting = false;
        }
    }

    public void ResetPlayerStatus()
    {
        _numberOfLives = _defaultNumberOfLives;
        _energy = _defaultEnergy;
        _score = _defaultScore;
        _diamonds = _defaultDiamond;
        _serializePlayer.SavePlayerStatus();
    }


    public bool PlayerIsDead() { return _isDead; }

    public void SetPlayerDead(bool dead)
    {
        DeductPlayerLife();
        _serializePlayer.SavePlayerStatus();
        _playerAudio.PlayerIsDyingSound();
        GetComponent<Rigidbody>().isKinematic = true;
        _isDead = dead;

        GameObject obj = GameObject.Find("ButtonCtrl");
        obj.GetComponent<DeathCanvasScript>().ShowCanvas();
    }

    // lives
    public int GetNumberOfLives() { return _numberOfLives; }
    public void SetNumberOfLives(int lives) { _numberOfLives = lives; }
    public void AddPlayerLife() { _numberOfLives++; }

    public void DeductPlayerLife()
    {
        _numberOfLives -= 1;
    }

    // score
    public int GetPlayerScore() { return _score; }
    public void SetPlayerScore(int score) { _score = score; }
    public void AddPlayerScore(int score) { _score += score; }

    // diamonds
    public int GetDiamondsAmount() { return _diamonds; }
    public void SetDiamondAmount(int x) { _diamonds = x; }
    public void AddDiamond()
    {
        _diamonds += 1;
        _playerAudio.PlayGetDiamondSound();
    }

    // energy
    public int GetAmountOfEnergy() { return _energy; }
    public void SetPlayerEnergy(int energy) { _energy = energy; }
    public void AddEnergy()
    {
        _energy += 1;
        _playerAudio.PlayGetFoodSound();
    }
    public void DeductEnergy()
    {
        _energy -= 1;
        _playerAudio.PlayDestroyCrateSound();
    }
}