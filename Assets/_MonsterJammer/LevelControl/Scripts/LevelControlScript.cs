using UnityEngine;

public class LevelControlScript : MonoBehaviour
{
	public GameObject Generator;

	private GameObject _generator;
	private int _setting = 0; 
	private int _currentLevel;
	private int _targetAmountOfDiamonds = 0;
	private int _numberOfLevels;
	private LevelGeneratorScript _levelGenerator;
	private LabirynthDestroyScript _labirynthDestroy;
	private PlayerStatusScript _playerStatus;
	private GameControlAudioScript _gameControlAudio;
	
	private void Start ()
	{
		SetNextLevel();
	}

    private void SetParametersOfLevel(Level lvl)
    {
        _targetAmountOfDiamonds = lvl.GetTargetAmountOfDiamonds();
	    EnergyOnLevel = lvl.EnergyOnLevel;
        _levelGenerator.SetLabirynthParameters(lvl.GetLevelGeneratorParametersArray());
    }

	public void SetLevel()
	{
		LabirynthDestroyScript.DestroyLabirynth();
		_generator = Instantiate(Generator);
		_levelGenerator = _generator.GetComponent<LevelGeneratorScript>();
		switch (_currentLevel)
		{
			case 1:
                // mapSize, maxTunelCount, minTunelLength, amountOfCrates, amountsOfMonsterA, amountsOfMonsterB, amountsOfMonsterC, targetAmountOfDiamonds, energyOnLevelStart
                SetParametersOfLevel(new Level(17, 150, 2, 22, 2, 0, 2, 2, 3)); 
				break;
			case 2:
                SetParametersOfLevel(new Level(25, 450, 2, 30, 4, 2, 1, 2, 5));
                break;
			case 3:
                SetParametersOfLevel(new Level(31, 450, 2, 50, 10, 4, 1, 8, 7));
                break;
			case 4:
                SetParametersOfLevel(new Level(31, 450, 2, 50, 14, 3, 2, 10, 3));
                break;
				default: Debug.Log("no more level");
					break;
		}
		_levelGenerator.GenerateLabirynth();
		_playerStatus = GameObject.Find("Player1(Clone)").GetComponent<PlayerStatusScript>();
		_playerStatus.DiamondsLevelTarget = _targetAmountOfDiamonds;
		_playerStatus.SetPlayerEnergy(EnergyOnLevel);
		_playerStatus.AfterLevelFinish();
		
		_gameControlAudio = GameObject.Find("GameControl").GetComponent<GameControlAudioScript>();
		if(_gameControlAudio != null)
			_gameControlAudio.PlaySettingAudioClip(_setting);
	}

	public void SetNextLevel()
	{
		_currentLevel += 1;
        var obj = GameObject.Find("ButtonCtrl");
        Time.timeScale = 0f;
        obj.GetComponent<LevelCompletedScript>().ShowCanvas();
        SetNextSetting();
        SetLevel();
	}

	public int GetTargetAmountOfDiamonds()
	{
		return _targetAmountOfDiamonds;
	}

	public int GetCurrentLevel()
	{
		return _currentLevel;
	}

	public void SetCurrentLevel(int level)
	{
		_currentLevel = level;
	}

    public void SetCurrentSetting(int setting)
    {
        _setting = setting;
    }

	public int EnergyOnLevel { get; private set; }

	public void ResetLevel()
	{
		var player = GameObject.Find("Player1(Clone)");
		player.GetComponent<PlayerStatusScript>().SetPlayerAlive();
		player.GetComponent<PlayerAnimationControlScript>().PlayerIdle();

		SetLevel();
	}

	private void SetNextSetting()
	{
		_setting += 1;
		GetComponent<SettingsControlScript>().SetCurrentSetting = _setting;
	}
}
