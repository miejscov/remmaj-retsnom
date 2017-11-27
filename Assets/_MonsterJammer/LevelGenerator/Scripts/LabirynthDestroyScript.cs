using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabirynthDestroyScript : MonoBehaviour {

	public static void DestroyLabirynth()
	{
		Destroy(GameObject.Find("LevelGenerator"));
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
		
		

//		GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);
// 
//		for (int i = 0; i < GameObjects.Length; i++)
//		{
//			if (!GameObjects[i].CompareTag("GameController") && !GameObjects[i].CompareTag("Player") && !GameObjects[i].CompareTag("MainCamera") && !GameObjects[i].CompareTag("Untagged"))
//			{
//				Debug.Log("destroy tag: " + GameObjects[i].tag + "name: " + GameObjects[i].name);
//				Destroy(GameObjects[i]);
//			}
//		}
		
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
