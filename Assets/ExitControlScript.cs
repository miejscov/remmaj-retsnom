using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitControlScript : MonoBehaviour
{

	private Collider _collider;
	private Transform[] ts;
	private GameObject _gate;
	public GameObject Collider;
	private PlayerRbMoveScript _playerRbMove;
	private CameraFollowScript _camera;
	private Vector3 _targetPos;
	private EntranceAudioScript _audio;
	
	
	private void Start()
	{
		_audio = GetComponent<EntranceAudioScript>();
		_gate = getChildGameObject(this.gameObject, "Tunnel_Gate");
		_camera = GameObject.Find("Main Camera(Clone)").GetComponent<CameraFollowScript>();
		_targetPos = _gate.transform.position;
	}

	public void OpenExit()
	{
		_camera.SetCameraOnExit();
		Invoke("OpenGate", 1f);
	}

	private void OpenGate()
	{
		_audio.PlayOpeningGateSound();
		_targetPos = _gate.transform.position + Vector3.down *2;
		Instantiate(Collider, (transform.position - Vector3.right), Quaternion.identity);
		Invoke("ResetCamera", 2f);
	}
	
	
	
	private void ResetCamera()
	{
		_camera.ResetCamera();
		_audio.StopAudio();
	}

	private GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}

	private void Update()
	{
		Move();
		Debug.Log("target po exi" + _targetPos);
		Debug.Log("pos ex" + transform.position);
	}

	private void Move()
	{
		_gate.transform.position = Vector3.MoveTowards(_gate.transform.position, _targetPos, 1 * Time.deltaTime);
	}
}
