using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabirynthDestroyScript : MonoBehaviour {

	public static void DestroyLabirynth()
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

	private static void DestroyAllObjectsWithTag(string tagToDestroy)
	{
		var objectsToDestroy = GameObject.FindGameObjectsWithTag(tagToDestroy);

		foreach(var obj in objectsToDestroy)
		{
			Destroy(obj);
		}
	}
}
