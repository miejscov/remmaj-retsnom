using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabirynthDestroyScript : MonoBehaviour {

	public static void DestroyLabirynth()
	{
		Destroy(GameObject.Find("LevelGenerator(Clone)"));
		DestroyAllObjectsWithTag("Monster");
		DestroyAllObjectsWithTag("Crate");
		DestroyAllObjectsWithTag("Wall");
		DestroyAllObjectsWithTag("Floor");
		DestroyAllObjectsWithTag("Entrance");
		DestroyAllObjectsWithTag("Ground");
		DestroyAllObjectsWithTag("Surprise");
		DestroyAllObjectsWithTag("MoveCollider");
		DestroyAllObjectsWithTag("Food");
		DestroyAllObjectsWithTag("Diamond");
		DestroyAllObjectsWithTag("Exit");
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
