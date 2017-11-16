using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondControlScript : MonoBehaviour
{

	private MeshRenderer _renderer;
	public GameObject DestroyParticle;
    private GameObject _collider;
	
	private void Start()
	{
		_renderer = GetComponent<MeshRenderer>();
		Invoke("EnableMeshRenderer", 80f * Time.deltaTime);
	}

	private void EnableMeshRenderer()
	{
		_renderer.enabled = true;
	}

	private void DisableMeshRenderer()
	{
		_renderer.enabled = false;
	}
	
	public void DestroyDiamond()
	{
		GameObject tempGameObject = (GameObject)Instantiate(DestroyParticle,transform.position , Quaternion.identity);
		DisableMeshRenderer();
		Destroy(tempGameObject,.3f);
        Destroy(_collider);
		Invoke("DestroyGameObject", .01f);
	}

	private void DestroyGameObject()
	{
		Destroy(gameObject);
	}

    public void SetDiamondsCollider(GameObject coll)
    {
        _collider = coll;
    }
}
