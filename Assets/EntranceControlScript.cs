using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceControlScript : MonoBehaviour
{

	private Collider _collider;
	private Transform[] ts;
	private GameObject _gate;
	private GameObject player;
	private PlayerRbMoveScript _playerRbMove;
	private Vector3 _gateClosePosition;
	private Vector3 _gateOpenPosition;
	private PlayerRotationScript _playerRotation;
	private PlayerControlScript _playerControl;
	private CameraFollowScript _camera;
	
	private void Start()
	{
		_camera = GameObject.Find("Main Camera(Clone)").GetComponent<CameraFollowScript>();
		_camera.SetCameraOnEntry();
		player = GameObject.Find("Player1(Clone)");
		player.GetComponent<PlayerRotationScript>().TurnRight();
		_playerRbMove = player.GetComponent<PlayerRbMoveScript>();
		_playerRbMove.SetPlayerSpeed(1f);
		player.GetComponent<PlayerControlScript>().SetFreezePlayer(true);
		_gate = getChildGameObject(this.gameObject, "Tunnel_Gate");
		_gateClosePosition = _gate.transform.position;
		Invoke("OpenGate", 2f);
	}

	public void OpenGate()
	{
		_playerRbMove.SetPlayerTargetPosition(_gate.transform.position + Vector3.right);
		_gate.transform.position = Vector3.down * 2;
		Invoke("CloseGate", 3f);
	}

	private void CloseGate()
	{
		Debug.Log("Close entry");
		_gate.transform.position = _gateClosePosition;
		_playerRbMove.ResetSpeed();
		player.GetComponent<PlayerControlScript>().SetFreezePlayer(false);
		Invoke("ResetCamera", 1f);
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
