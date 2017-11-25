using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;

public class ExitControlScript : MonoBehaviour
{

	private Collider _collider;
//	private Transform[] ts;
	private GameObject _gate;
	public GameObject Collider;
	private PlayerRbMoveScript _playerRbMove;
	private CameraFollowScript _camera;
	private Vector3 _targetPos;
	private EntranceAudioScript _audio;
	private bool _isExitOpen;
	private bool _cancelAnimation;
	
	private void Start()
	{
		_audio = GetComponent<EntranceAudioScript>();
		_gate = getChildGameObject(this.gameObject, "Tunnel_Gate");
		_camera = GameObject.Find("Main Camera(Clone)").GetComponent<CameraFollowScript>();
		_targetPos = _gate.transform.position;
	}

	public void OpenExit()
	{
		_isExitOpen = true;
		_camera.SetCameraOnExit();
		Invoke("OpenGate", 1f);
	}

	private void OpenGate()
	{
		_audio.PlayOpeningGateSound();
		_targetPos = _gate.transform.position + Vector3.down *2;
		Instantiate(Collider, (transform.position), Quaternion.identity);
		Invoke("ResetCamera", 2f);
	}
	
	private void ResetCamera()
	{
		_audio.StopAudio();
		if (_cancelAnimation) return;
		_camera.ResetCamera();
	}

	private GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}

	private void Update()
	{
		_gate.transform.position = Vector3.MoveTowards(_gate.transform.position, _targetPos, 1 * Time.deltaTime);
		if (!_isExitOpen || _cancelAnimation) return;
		if (!Input.anyKeyDown) return;
		_cancelAnimation = true;
		_camera.ResetCamera();
	}
}
