using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class PlayerRayCastHit : MonoBehaviour
{
	private RaycastHit _hit;
	private string _objectTag;
	private bool _canMove = false;
	private bool _canHit = false;
	private GameObject _hitObject;
	
	public int CheckActionInDirection(Vector3 direction, Quaternion rotation)
	{
		var action = 0;
		_objectTag = PlayerRayHit(direction);

		if (_objectTag.Equals("Crate"))
		{
			var crateAction = CheckCrateAction(direction, rotation);
			
			switch (crateAction)
			{
				case 1:
					action = 1; // can hit
					break;
				case 2:
					action = 2; // can move
					break;
				default:
					action = 0; // can't move
					break;
			}
		}
		else if (_objectTag.Equals("Wall") || _objectTag.Equals("MoveCollider"))
			action = 0; 
		else action = 2;

		return action;
	}

	public GameObject HitObject()
	{
		return _hitObject;
	}
	
	private string PlayerRayHit(Vector3 direction)
	{
		_objectTag = "Untagged";
		_hitObject = null;
		if (Physics.Raycast(transform.position, direction, out _hit, .8f))
		{
			Debug.DrawRay(transform.position, direction, Color.green, 3f);
			_objectTag = _hit.transform.gameObject.tag;
			_hitObject = _hit.transform.gameObject;
			Debug.Log("hit obj" + _hitObject);
		}

		return _objectTag;
	}

	private int CheckCrateAction(Vector3 direction, Quaternion rotation)
	{
		var crateAction = _hit.transform.gameObject.GetComponentInChildren<CrateRayCheck>().CanMove(direction, rotation);
		return crateAction;
	}
}
