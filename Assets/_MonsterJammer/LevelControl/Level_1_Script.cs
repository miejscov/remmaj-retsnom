using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_Script : MonoBehaviour
{

	private int _mapSize = 10;
	private int _maxTunelCount = 450;
	private int _minTunelLength = 2;
	private int _amountOfCrates = 100;
	private int _amountsOfMonster = 10;

	private const int TargetAmountOfDiamonds = 1;

	private int _amountOfCratesOnStart = 10;
	private int _amountOfMonstersOnStart = 2;

	private int _wall = 20;

	public int GetTargetAmountOfDiamonds(){return TargetAmountOfDiamonds;}
}
