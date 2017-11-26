using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerStatusScript : MonoBehaviour
{
    private const int DefaultNumberOfLives = 3;
    private const int DefaultScore = 0;

    private int _numberOfLives;
    private int _extraLifeScore = 0;

    private int _extraLifeScoreThreshold = 200;

    private PlayerRbMoveScript _playerRbMove;

    private bool _isDead = false;
    public int _energy;
    private int _score;
    private int _diamondsCollectedInLevel;
    private int _diamondsLevelTarget;
    private int _diamondsLeft = 0;

    private CanvasScript _canvas;
    private PlayerAudioControlScript _audio;
    private LevelControlScript _levelControl;
    private PlayerAudioControlScript _playerAudio;
    private CapsuleCollider _capsuleCollider;
    private SerializePlayerStatus _serializePlayer;
    private ExitControlScript _exitControl;
    private PlayerControlScript _playerControl;
    private PlayerCollisionScript _playerCollision;
    private PlayerAnimationControlScript _playerAnimation;

    private void Start()
    {
        _playerAnimation = GetComponent<PlayerAnimationControlScript>();
        _playerCollision = GetComponent<PlayerCollisionScript>();
        _playerRbMove = GetComponent<PlayerRbMoveScript>();
        _playerControl = GetComponent<PlayerControlScript>();
        _audio = GetComponent<PlayerAudioControlScript>();
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
        _energy = _levelControl.EnergyOnLevel;
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
//        _canvas.SetCanvasVisibility(true);
        _canvas.TargetDiamondAmountInLevel = _diamondsLevelTarget;
        _canvas.SetColectedDiamond(_diamondsCollectedInLevel);
        _canvas.SetEnergy(_energy);
        _canvas.SetTotalScore(_score);
        _canvas.SetLevel(_levelControl.GetCurrentLevel());
        _canvas.SetLives(_numberOfLives);
    }

    public bool PlayerIsDead() { return _isDead; }

    public void SetPlayerAlive()
    {
        SetCanvasValue();
        _playerAnimation.PlayerIdle();
        _playerRbMove._isStopped = true;
        _playerCollision.ResetOnCrate();
        _playerControl.SetFreezePlayer(true);
//        GetComponent<Rigidbody>().isKinematic = false;
        _isDead = false;
//        GetComponent<PlayerAnimationControlScript>().PlayerIdle();
    }

    public void SetPlayerDead()
    {
        _playerRbMove.IsStopped = true;
        _playerControl.SetFreezePlayer(true);
        
        DeductPlayerLife();
        _serializePlayer.SavePlayerStatus();
        _playerAudio.PlayerIsDyingSound();
//                GetComponent<Rigidbody>().isKinematic = true;
        _isDead = true;

        _canvas.SetCanvasVisibility(false);
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
            _audio.PlayeExtraLifeSound();
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
            _canvas.SetColectedDiamond(_diamondsCollectedInLevel);
            //            _levelControl.SetNextLevel();
            _exitControl = GameObject.Find("Exit(Clone)").GetComponent<ExitControlScript>();
            _exitControl.OpenExit();
        }
        _canvas.SetColectedDiamond(_diamondsCollectedInLevel);
    }

    public int DiamondsLevelTarget
    {
        set { _diamondsLevelTarget = value; }
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
