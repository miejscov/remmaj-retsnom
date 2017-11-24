using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitColliderScript : MonoBehaviour
{

	private LevelControlScript _levelControl;
	private PlayerRbMoveScript _playerRbMove;

	private void Start()
	{
		_levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name.Equals("Player1(Clone)"))
		{
//			_playerRbMove = other.gameObject.GetComponent<PlayerRbMoveScript>();
//			_playerRbMove.SetPlayerTargetPosition(transform.position + Vector3.right);
			other.gameObject.GetComponent<PlayerControlScript>().SetFreezePlayer(true);
			Invoke("NextLevel", 4f);
		}
	}

	private void NextLevel()
	{
		_levelControl.SetNextLevel();
	}
}
