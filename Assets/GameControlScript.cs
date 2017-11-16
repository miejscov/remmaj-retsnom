﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlScript : MonoBehaviour {

	public float DefaultPlayerSpeed = 4f;
	public float DefaultCrateSpeed = 4f;

	private float _currentCrateSpeed;

	private void Start()
	{
		_currentCrateSpeed = DefaultCrateSpeed;
	}
	
	public float GetDefaultPlayerSpeed()
	{
		return DefaultPlayerSpeed;
	}

	public float GetCrateSpeed()
	{
		return _currentCrateSpeed;
	}

	public void ResetCrateSpeed()
	{
		_currentCrateSpeed = DefaultCrateSpeed;
	}

	public void SetCurrentCrateSpeed(float speed)
	{
		_currentCrateSpeed = speed;
	}
	
}
