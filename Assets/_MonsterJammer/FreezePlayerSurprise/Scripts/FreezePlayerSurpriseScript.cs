using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayerSurpriseScript : MonoBehaviour {

	private BoxCollider _boxCollider;
	private MeshRenderer _meshRenderer;
	private GameObject _player;

	private Animator _playerAnimator;

	private const float FreezeTime = 300f;

	private void Start ()
	{
		_boxCollider = GetComponentInChildren<BoxCollider>();
		_meshRenderer = GetComponentInChildren<MeshRenderer>();
	}
	
	public void Set()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
		_boxCollider.enabled = false;
		_meshRenderer.enabled = false;

		_player.GetComponent<PlayerControlScript>().SetFreezePlayer(true);
		_player.GetComponent<Animator>().SetBool("Freeze", true);
		Invoke("UnfreezePlayer", FreezeTime * Time.deltaTime);
	}

	private void UnfreezePlayer()
	{
		_player.GetComponent<Animator>().SetBool("Freeze", false);
		_player.GetComponent<PlayerControlScript>().SetFreezePlayer(false);
		Destroy(gameObject);
	}
}
