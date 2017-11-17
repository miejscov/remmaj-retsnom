using System;
using System.Collections;
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

	public void SetNextLevel()
	{
		DestroyLabirynth();
	}
	
	public void DestroyLabirynth()
	{
		DestroyAllObjectsWithTag("Wall");
		DestroyAllObjectsWithTag("Monster");
		DestroyAllObjectsWithTag("Ground");
		DestroyAllObjectsWithTag("Crate");
		DestroyAllObjectsWithTag("Floor");
		DestroyAllObjectsWithTag("Food");
		DestroyAllObjectsWithTag("Diamond");
		DestroyAllObjectsWithTag("MoveCollider");
	}

	private void DestroyAllObjectsWithTag(String tag)
	{
		GameObject[] objectsToDestroy;

		objectsToDestroy = GameObject.FindGameObjectsWithTag(tag);
 
		foreach(var obj in objectsToDestroy)
		{
			Destroy(obj);
		}
	}
}
