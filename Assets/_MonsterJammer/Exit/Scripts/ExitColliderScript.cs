﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitColliderScript : MonoBehaviour
{

	private LevelControlScript _levelControl;
	private PlayerRbMoveScript _playerRbMove;
	private CanvasScript _canvas;

	private void Start()
	{
		_levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
		_canvas = GameObject.Find("Canvas").GetComponent<CanvasScript>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.name.Equals("Player1(Clone)")) return;
//			_playerRbMove = other.gameObject.GetComponent<PlayerRbMoveScript>();
//			_playerRbMove.SetPlayerTargetPosition(transform.position + Vector3.right);
		other.gameObject.GetComponent<PlayerControlScript>().SetFreezePlayer(true);
		_canvas.SetCanvasVisibility(false);
		Invoke("NextLevel", 2f);
	}

	private void NextLevel()
	{
		_levelControl.SetNextLevel();
	}
}