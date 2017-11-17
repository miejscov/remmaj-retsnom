using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabirynthDestroyScript : MonoBehaviour {

	public void DestroyLabirynth()
	{
		DestroyAllObjectsWithTag("Wall");
		DestroyAllObjectsWithTag("Monster");
		DestroyAllObjectsWithTag("Ground");
		DestroyAllObjectsWithTag("Crate");
		DestroyAllObjectsWithTag("Floor");
		DestroyAllObjectsWithTag("Food");
		DestroyAllObjectsWithTag("Diamond");
		DestroyAllObjectsWithTag("MoveCollider");
	}

	private void DestroyAllObjectsWithTag(string tagToDestroy)
	{
		GameObject[] objectsToDestroy;

		objectsToDestroy = GameObject.FindGameObjectsWithTag(tagToDestroy);
 
		foreach(var obj in objectsToDestroy)
		{
			Destroy(obj);
		}
	}
}
