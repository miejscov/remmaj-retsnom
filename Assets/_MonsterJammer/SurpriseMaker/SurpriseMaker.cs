using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseMaker : MonoBehaviour
{
	public GameObject[] SurprisePrefabArray;
	public List<GameObject> _objectList;
		
	private GameObject Temp;

	public int _chanceForNothing = 10;

	private void Start()
	{
		AddArrayToList(SurprisePrefabArray, _objectList);
	}

	public void PlaceBonus(Vector3 position)
	{
		if (_objectList.Count <= 0) return;
		var index = RandomBonus();
		
		if (index >= _objectList.Count) return;
		Temp = Instantiate(_objectList[index], position, Quaternion.identity);
		
		if (Temp.name.Contains("ExtraLife"))
			_objectList.RemoveAt(index);
	}

	private int RandomBonus()
	{
		var i = Random.Range(0, _objectList.Count + _chanceForNothing);

		return i;
	}

	private static void AddArrayToList(IEnumerable<GameObject> array, ICollection<GameObject> list)
	{
		foreach (var t in array)
			list.Add(t);
	}
}
