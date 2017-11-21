using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3_Script : MonoBehaviour {
	private const int MapSize = 31;
	private const int MaxTunelCount = 450;
	private const int MinTunelLength = 2;
	private const int AmountOfCrates = 50;
	private const int AmountsOfMonster = 10;

	private int[] _levelGeneratorParameters;
	
	private const int TargetAmountOfDiamonds = 4;

	public static int GetTargetAmountOfDiamonds()
	{
		return TargetAmountOfDiamonds;
	}

	public int[] GetLevelGeneratorParametersArray()
	{
		Debug.Log("Level 3");
		_levelGeneratorParameters = new int[5];

		_levelGeneratorParameters[0] = MapSize;
		_levelGeneratorParameters[1] = MaxTunelCount;
		_levelGeneratorParameters[2] = MinTunelLength;
		_levelGeneratorParameters[3] = AmountOfCrates;
		_levelGeneratorParameters[4] = AmountsOfMonster;

		return _levelGeneratorParameters;
	}
}
