using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPlayerSurprise : MonoBehaviour {

	private PlayerRbMoveScript _playerRbMove;
	private BoxCollider _boxCollider;
	private MeshRenderer _meshRenderer;
	private GameControlScript _gameControl;

	private const float SlowMotionTime = 400f;

	private void Start ()
	{
		_gameControl = GameObject.Find("GameControl").GetComponent<GameControlScript>();
		_boxCollider = GetComponentInChildren<BoxCollider>();
		_meshRenderer = GetComponentInChildren<MeshRenderer>();
	}
	
	public void Set()
	{
		_gameControl.ResetCrateSpeed();
		_playerRbMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRbMoveScript>();
		_playerRbMove.SetPlayerSpeed(10);
		_boxCollider.enabled = false;
		_meshRenderer.enabled = false;
		
		Invoke("StopSlowMotion", SlowMotionTime * Time.deltaTime);
	}

	private void StopSlowMotion()
	{
			_playerRbMove.ResetSpeed();
			Destroy(gameObject);
	}
}
