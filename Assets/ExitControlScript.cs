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
	
	private void Start()
	{
		_gate = getChildGameObject(this.gameObject, "Tunnel_Gate");

	}

	public void OpenGate()
	{
		_gate.transform.position = Vector3.down * 2;
		Instantiate(Collider, (transform.position - Vector3.right), Quaternion.identity);
	}

	private GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}
	
	
	
	
}
