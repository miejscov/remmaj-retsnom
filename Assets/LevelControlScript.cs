using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelControlScript : MonoBehaviour
{

	private int _currentLevel;

	private int _targetAmountOfDiamonds = 0;
	private int _numberOfLevels;
	private Component _level;
	
	
	// Use this for initialization
	void Start ()
	{
		_currentLevel = 1;
		SetLevel(_currentLevel);
	}

	// Update is called once per frame
	
	public void SetLevel(int level)
	{
		
		switch (level)
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

	public int GetTargetAmountOfDiamonds()
	{
		return _targetAmountOfDiamonds;
	}
}
