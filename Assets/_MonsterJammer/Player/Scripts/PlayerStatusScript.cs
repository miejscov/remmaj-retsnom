﻿using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerStatusScript : MonoBehaviour
{
    private const int DefaultNumberOfLives = 3;
    private const int DefaultEnergy = 5;
    private const int DefaultScore = 0;

    private int _numberOfLives;
    private int _extraLifeScore = 0;
    private int _extraLifeScoreThreshold = 50;

    private bool _isDead = false;
    public int _energy;
    private int _score;
    private int _diamondsCollectedInLevel;
    private int _diamondsLevelTarget;
    private int _diamondsLeft = 0;


    private CanvasScript _canvas;
    private LevelControlScript _levelControl;
    private PlayerAudioControlScript _playerAudio;
    private CapsuleCollider _capsuleCollider;
    private SerializePlayerStatus _serializePlayer;
    private ExitControlScript _exitControl;

    private void Start()
    {
        _serializePlayer = GetComponent<SerializePlayerStatus>();
        _playerAudio = GetComponent<PlayerAudioControlScript>();
        _levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
        _canvas = GameObject.Find("Canvas").GetComponent<CanvasScript>();

        if (ButtonContolScript.isStarting)
        {
            ResetPlayerStatus();
            ButtonContolScript.isStarting = false;
        }
        Initialize();
    }

    private void Initialize()
    {
        SetCanvasValue();
        ResetPlayerStatus();
    }

    public void AfterLevelFinish()
    {
        _diamondsCollectedInLevel = 0;
        _diamondsLeft = _diamondsLevelTarget;
        SetCanvasValue();
    }


    public void ResetPlayerStatus()
    {
        _diamondsCollectedInLevel = 0;
        _levelControl.SetCurrentLevel(1);
        _numberOfLives = DefaultNumberOfLives;
        _energy = DefaultEnergy;
        _score = DefaultScore;
        _diamondsLevelTarget = _levelControl.GetTargetAmountOfDiamonds();
        _diamondsLeft = _diamondsLevelTarget;
        _serializePlayer.SavePlayerStatus();

        SetCanvasValue();
    }

    private void SetCanvasValue()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        _levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();

        _canvas.SetDiamondsLeft(_diamondsLeft);
        _canvas.SetEnergy(DefaultEnergy);
        _canvas.SetTotalScore(DefaultScore);
        _canvas.SetLevel(_levelControl.GetCurrentLevel());
        _canvas.SetLives(_numberOfLives);
    }

    public bool PlayerIsDead() { return _isDead; }

    public void SetPlayerAlive()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        _isDead = false;
        GetComponent<PlayerAnimationControlScript>().PlayerIdle();
    }

    public void SetPlayerDead(bool dead)
    {
        if (dead)
            DeductPlayerLife();
        _serializePlayer.SavePlayerStatus();
        _playerAudio.PlayerIsDyingSound();
        //        GetComponent<Rigidbody>().isKinematic = true;
        _isDead = dead;

        var obj = GameObject.Find("ButtonCtrl");
        obj.GetComponent<DeathCanvasScript>().ShowCanvas();
    }

    // lives
    public int GetNumberOfLives() { return _numberOfLives; }
    public void SetNumberOfLives(int lives) { _numberOfLives = lives; }

    public void AddPlayerLife()
    {
        _numberOfLives++;
        _canvas.SetLives(_numberOfLives);
    }

    public void DeductPlayerLife()
    {
        _numberOfLives -= 1;
    }

    // score
    public int GetPlayerScore() { return _score; }
    public void SetPlayerScore(int score) { _score = score; }

    public void AddPlayerScore(int score)
    {
        _score += score;
        _extraLifeScore += score;
        LevelCompletedScript.AddPoints(score);
        if (_extraLifeScore >= _extraLifeScoreThreshold)
        {
            AddPlayerLife();
            _extraLifeScore = 0;
        }
        _canvas.SetTotalScore(_score);

    }

    // diamonds
    public int GetDiamondsAmount() { return _diamondsCollectedInLevel; }
    public void AddDiamond()
    {
        _diamondsCollectedInLevel += 1;
        _playerAudio.PlayGetDiamondSound();
        LevelCompletedScript.AddDiamond();
        _diamondsLeft -= 1;
        if (_diamondsLevelTarget - _diamondsLeft == _diamondsLevelTarget && _diamondsLevelTarget != 0)
        {
            _canvas.SetDiamondsLeft(_diamondsLeft);
            //            _levelControl.SetNextLevel();
            _exitControl = GameObject.Find("Exit(Clone)").GetComponent<ExitControlScript>();
            _exitControl.OpenExit();
        }
        _canvas.SetDiamondsLeft(_diamondsLeft);
    }

    public void SetDiamondTarget(int target)
    {
        _diamondsLevelTarget = target;
        AfterLevelFinish();
    }

    // energy
    public int GetAmountOfEnergy() { return _energy; }
    public void SetPlayerEnergy(int energy) { _energy = energy; }
    public void AddEnergy()
    {
        _energy += 1;
        _playerAudio.PlayGetFoodSound();
        _canvas.SetEnergy(_energy);
    }
    public void DeductEnergy()
    {
        _energy -= 1;
        _playerAudio.PlayDestroyCrateSound();
        _canvas.SetEnergy(_energy);
    }
}
