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
	
	private void Start()
	{
		_gate = getChildGameObject(this.gameObject, "Tunnel_Gate");
		_camera = GameObject.Find("Main Camera(Clone)").GetComponent<CameraFollowScript>();
	}

	public void OpenExit()
	{
		_camera.SetCameraOnExit();
		Invoke("OpenGate", 2f);
	}

	private void OpenGate()
	{
		_gate.transform.position = Vector3.down * 2;
		Instantiate(Collider, (transform.position - Vector3.right), Quaternion.identity);
		Invoke("ResetCamera", 2f);
	}
	
	
	
	private void ResetCamera()
	{
		_camera.ResetCamera();
	}

	private GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}
	
	
	
	
}
