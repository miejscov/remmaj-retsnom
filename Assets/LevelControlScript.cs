using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelControlScript : MonoBehaviour
{

	private int _currentLevel;
	private PlayerStatusScript _playerStatus;

	private int _targetAmountOfDiamonds = 0;
	private int _numberOfLevels;
	private Component _level;
	
	
	// Use this for initialization
	void Start ()
	{
		_playerStatus = GameObject.Find("Player1(Clone)").GetComponent<PlayerStatusScript>();
		_currentLevel = 1;
		SetLevel(_currentLevel);
	}

	// Update is called once per frame
	void Update () {
		if (_targetAmountOfDiamonds >= _playerStatus.GetDiamondsAmount())
		{
			SetNextLevel();
		}
	}
	
	public void SetLevel(int level)
	{
		switch (_currentLevel)
		{
			case 1:
				_targetAmountOfDiamonds = GetComponent<Level_1_Script>().GetTargetAmountOfDiamonds();
				break;
			case 2:
				_targetAmountOfDiamonds = GetComponent<Level_2_Script>().GetTargetAmountOfDiamonds();
				break;
			case 3 :
				_targetAmountOfDiamonds = GetComponent<Level_3_Script>().GetTargetAmountOfDiamonds();
				break;
		}

	
	}


	public void ResetLevel(Component level)
	{
		
	}

	public void SetNextLevel()
	{
		_currentLevel += 1;
		SetLevel(_currentLevel);
	}
}
