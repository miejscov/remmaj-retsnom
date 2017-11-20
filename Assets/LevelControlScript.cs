using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
	
	private void Start ()
	{
		_currentLevel = 0;
		SetNextLevel();
//		SetLevel();
	}

	
	public void SetLevel()
	{
		LabirynthDestroyScript.DestroyLabirynth();
		_generator = Instantiate(Generator);
		_levelGenerator = _generator.GetComponent<LevelGeneratorScript>();
		switch (_currentLevel)
		{
			case 1:
				var level_1 = GetComponent<Level_1_Script>();
				_targetAmountOfDiamonds = Level_1_Script.GetTargetAmountOfDiamonds();
				_levelGenerator.SetLabirynthParameters(level_1.GetLevelGeneratorParametersArray());
				break;
			case 2:
				var level_2 = GetComponent<Level_2_Script>();
				_targetAmountOfDiamonds = Level_2_Script.GetTargetAmountOfDiamonds();
				_levelGenerator.SetLabirynthParameters(level_2.GetLevelGeneratorParametersArray());
				break;
			case 3:
				var level_3 = GetComponent<Level_3_Script>();
				_targetAmountOfDiamonds = Level_3_Script.GetTargetAmountOfDiamonds();
				_levelGenerator.SetLabirynthParameters(level_3.GetLevelGeneratorParametersArray());
				break;
			case 4:
				var level_4 = GetComponent<Level_4_Script>();
				_targetAmountOfDiamonds = Level_4_Script.GetTargetAmountOfDiamonds();
				_levelGenerator.SetLabirynthParameters(level_4.GetLevelGeneratorParametersArray());
				break;
		}
		_levelGenerator.GenerateLabirynth();
	}

	public void SetNextLevel()
	{
		Debug.Log("Set next level function");
		_currentLevel += 1;
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
}
