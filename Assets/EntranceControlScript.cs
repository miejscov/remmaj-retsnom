﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntranceControlScript : MonoBehaviour
{

	private Collider _collider;
	private GameObject _gate;
	private GameObject _player;
	private PlayerRbMoveScript _playerRbMove;
	private Vector3 _gateClosePosition;
	private Vector3 _gateOpenPosition;
	private PlayerRotationScript _playerRotation;
	private PlayerControlScript _playerControl;
	private CameraFollowScript _camera;
	private EntranceAudioScript _entranceAudio;	
	private Vector3 _targetPos;
	private bool _cancelGateAnim;
	
	private void Start()
	{
		_entranceAudio = GetComponent<EntranceAudioScript>();
		_camera = GameObject.Find("Main Camera(Clone)").GetComponent<CameraFollowScript>();
		
		_player = GameObject.Find("Player1(Clone)");
		_player.GetComponent<PlayerRotationScript>().TurnRight();
		_playerRbMove = _player.GetComponent<PlayerRbMoveScript>();
		_playerRbMove.SetPlayerSpeed(1f);
		_player.GetComponent<PlayerControlScript>().SetFreezePlayer(true);
		
		_camera.SetCameraOnEntry();
		_gate = GetChildGameObject(gameObject, "Tunnel_Gate");
		_targetPos = _gateClosePosition = _gate.transform.position;
		if (_cancelGateAnim)
			CancelGateAnim();
		else
			Invoke("OpenGate", 1f);
	}

	private void OpenGate()
	{
		_entranceAudio.PlayOpeningGateSound();
		_targetPos = _gate.transform.position + Vector3.down * 2;
		if (_cancelGateAnim)
			CancelGateAnim();
		else
			Invoke("PlayerIsComming", 1f);
	}

	private void PlayerIsComming()
	{
		_entranceAudio.StopAudio();
		_playerRbMove.SetPlayerTargetPosition(_gate.transform.position + Vector3.right);
		
		if (_cancelGateAnim) 
			CancelGateAnim();
		else
			Invoke("CloseGate", 2f);
	}
	
	

	private void CloseGate()
	{
		_entranceAudio.PlayClosingGateSound();
		_playerRbMove.ResetSpeed();
		_targetPos = _gateClosePosition;
		if (_cancelGateAnim) 
			CancelGateAnim();
		else
			Invoke("ResetCamera", 2f);
	}

	private void ResetCamera()
	{
		_player.GetComponent<PlayerControlScript>().SetFreezePlayer(false);
		_camera.ResetCamera();
		_entranceAudio.StopAudio();
	}

	private GameObject GetChildGameObject(GameObject fromGameObject, string withName) {
		var ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		return (from t in ts where t.gameObject.name == withName select t.gameObject).FirstOrDefault();
	}

	private void Update()
	{
//		if (!_cancelGateAnim)
//		{
//			if (Input.anyKeyDown)
//				CancelGateAnim();
//		}
		
		if (_cancelGateAnim) return;
		_gate.transform.position = Vector3.MoveTowards(_gate.transform.position, _targetPos, 1 * Time.deltaTime);
	}

	private void CancelGateAnim()
	{
		_cancelGateAnim = true;
		_entranceAudio.StopAudio();
//		ResetCamera();
		_playerRbMove.SetPlayerPosition(_gateClosePosition + Vector3.right);
		_gate.transform.position = _gateClosePosition;
		_playerRbMove.ResetSpeed();
		_player.GetComponent<PlayerControlScript>().SetFreezePlayer(false);

	}
}
