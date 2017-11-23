using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControlScript : MonoBehaviour
{

	public GameObject Generator;
	private GameObject _generator;
	private int _currentLevel;

	private int _targetAmountOfDiamonds = 0;
	private int _numberOfLevels;

	private LevelGeneratorScript _levelGenerator;
	private LabirynthDestroyScript _labirynthDestroy;
	private PlayerStatusScript _playerStatus;
	
	private void Start ()
	{
		
		SetNextLevel();
//		SetLevel();
	}

    private void SetParametersOfLevel(Level lvl)
    {
        _targetAmountOfDiamonds = lvl.GetTargetAmountOfDiamonds();
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
               SetParametersOfLevel(new Level(17, 150, 2, 14, 1, 1)); // mapSize, maxTunelCount, minTunelLength, amountOfCrates, amountsOfMonster, targetAmountOfDiamonds
				break;
			case 2:
                SetParametersOfLevel(new Level(25, 450, 2, 30, 10, 2));
                break;
			case 3:
                SetParametersOfLevel(new Level(31, 450, 2, 50, 10, 1));
                break;
			case 4:
                SetParametersOfLevel(new Level(31, 450, 2, 50, 14, 1));
                break;
		}
		_levelGenerator.GenerateLabirynth();
		_playerStatus = GameObject.Find("Player1(Clone)").GetComponent<PlayerStatusScript>();
		_playerStatus.SetDiamondTarget(_targetAmountOfDiamonds);
		_playerStatus.AfterLevelFinish();
	}

	public void SetNextLevel()
	{
		Debug.Log("Set next level function");
		_currentLevel += 1;
        var obj = GameObject.Find("ButtonCtrl");
        Time.timeScale = 0f;
        obj.GetComponent<LevelCompletedScript>().ShowCanvas();
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


	public void ResetLevel()
	{
		var player = GameObject.Find("Player1(Clone)");
		player.GetComponent<PlayerAnimationControlScript>().PlayerIdle();
		player.GetComponent<PlayerStatusScript>().SetPlayerDead(false);
		
		SetLevel();
	}
}
