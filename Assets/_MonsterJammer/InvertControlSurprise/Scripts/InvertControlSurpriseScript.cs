using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertControlSurpriseScript : MonoBehaviour
{

	private PlayerControlScript _playerControl;
	private BoxCollider _boxCollider;
	private MeshRenderer _meshRenderer;

	private const float SurpriseTime = 400f;

	private void Start ()
	{
		_boxCollider = GetComponentInChildren<BoxCollider>();
		_meshRenderer = GetComponentInChildren<MeshRenderer>();
	}
	
	public void Set()
	{
		_playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlScript>();
		_playerControl.SetInvertControl(true);
		_boxCollider.enabled = false;
		_meshRenderer.enabled = false;
		
		Invoke("StopInvertControl", SurpriseTime * Time.deltaTime);
	}

	private void StopInvertControl()
	{
		_playerControl.SetInvertControl(false);
		Destroy(gameObject);
	}
}
