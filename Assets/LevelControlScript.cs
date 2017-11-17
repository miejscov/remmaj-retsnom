using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelControlScript : MonoBehaviour
{

	private int _currentLevel;

	private int _targetAmountOfDiamonds = 0;
	private int _numberOfLevels;

	private LevelGeneratorScript _levelGenerator;
	private LabirynthDestroyScript _labirynthDestroy;
	
	// Use this for initialization
	void Start ()
	{
		var levelGenerator = GameObject.Find("LevelGenerator");
		_levelGenerator = levelGenerator.GetComponent<LevelGeneratorScript>();
		_labirynthDestroy = levelGenerator.GetComponent<LabirynthDestroyScript>();
		_currentLevel = 1;
		SetLevel(_currentLevel);
	}

	
	public void SetLevel(int level)
	{
		
		switch (level)
		{
			case 1:
				Level_1_Script level_1 = GetComponent<Level_1_Script>();
				_targetAmountOfDiamonds = level_1.GetTargetAmountOfDiamonds();
				_levelGenerator.SetLabirynthParameters(20, 300, 2, 10, 1);
				break;
			case 2:
				_targetAmountOfDiamonds = GetComponent<Level_2_Script>().GetTargetAmountOfDiamonds();
				break;
			case 3 :
				_targetAmountOfDiamonds = GetComponent<Level_3_Script>().GetTargetAmountOfDiamonds();
				break;
		}
		_levelGenerator.GenerateLabirynth();
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

	public int GetCurrentLevel()
	{
		return _currentLevel;
	}
}
