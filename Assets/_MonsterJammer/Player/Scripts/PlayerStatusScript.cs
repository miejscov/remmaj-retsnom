using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerStatusScript : MonoBehaviour
{
    private const int _defaultNumberOfLives = 3;
    private const int _defaultEnergy = 5;
    private const int _defaultScore = 0;
    
    private int _numberOfLives;
    private int _extraLifeScore = 0;
    private int _extraLifeScoreThreshold = 50;
    
    private bool _isDead = false;
    public int _energy;
    private int _score;
    private int _diamondsCollectedInLevel;
    private int _diamondsLevelTarget;
    private int _diamondsLeft =0;


    private CanvasScript _canvas;
    private LevelControlScript _levelControl;
    private PlayerAudioControlScript _playerAudio;
    private CapsuleCollider _capsuleCollider;
    private SerializePlayerStatus _serializePlayer;

    private void Start()
    {
        _serializePlayer = GetComponent<SerializePlayerStatus>();
        _playerAudio = GetComponent<PlayerAudioControlScript>();
        _levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
        _canvas = GameObject.Find("Canvas").GetComponent<CanvasScript>();

        if(ButtonContolScript.isStarting)
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

    private void AfterLevelFinish()
    {
//        _levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
        _diamondsCollectedInLevel = 0;
        _diamondsLeft = _diamondsLevelTarget;
        SetCanvasValue();
    }
    
    
    public void ResetPlayerStatus()
    {
        _diamondsCollectedInLevel = 0;
        _levelControl.SetCurrentLevel(1);
        _numberOfLives = _defaultNumberOfLives;
        _energy = _defaultEnergy;
        _score = _defaultScore;
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
        _canvas.SetEnergy(_defaultEnergy);
        _canvas.SetTotalScore(_defaultScore);
        _canvas.SetLevel(_levelControl.GetCurrentLevel());
        _canvas.SetLives(_numberOfLives);
        _canvas.SetDiamonds(_diamondsCollectedInLevel);
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
    public void SetPlayerScore(int score) {_score = score; }

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
        if (_diamondsLevelTarget - _diamondsLeft == _diamondsLevelTarget && _diamondsLevelTarget !=0)
        {
            _levelControl.SetNextLevel();
            AfterLevelFinish();
        }
        _canvas.SetDiamondsLeft(_diamondsLeft);
        _canvas.SetDiamonds(_diamondsCollectedInLevel);
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
