using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondControlScript1 : MonoBehaviour
{

	private MeshRenderer _renderer;
    private Collider _collider;
    //	public GameObject DestroyParticle;
    private GameObject _collider2;


    private void Start()
	{
        _renderer = GetComponentInChildren<MeshRenderer>();
        _collider = GetComponent<Collider>();
        Invoke("EnableMeshRenderer", 80f * Time.deltaTime);
        Invoke("EnableCollider", 40f * Time.deltaTime);
    }

    private void EnableMeshRenderer()
	{
		_renderer.enabled = true;
	}

    private void EnableCollider()
    {
        _collider.enabled = true;
    }

    private void DisableMeshRenderer()
	{
		_renderer.enabled = false;
	}
	
	
	public void DestroyDiamond()
	{
//		GameObject tempGameObject = (GameObject)Instantiate(DestroyParticle,transform.position , Quaternion.identity);
		DisableMeshRenderer();
        Destroy(_collider);
//		Destroy(tempGameObject,.3f);
		Invoke("DestroyGameObject", .01f);
	}

	private void DestroyGameObject()
	{
		Destroy(gameObject);
	}

    public void SetDiamondsCollider(GameObject coll)
    {
        _collider2 = coll;
    }
}
