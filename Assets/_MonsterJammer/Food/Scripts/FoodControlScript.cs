using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodControlScript : MonoBehaviour {


    private GameObject _collider;

    public void DestroyFood()
	{
        //animation
        //effect
        Destroy(_collider);
		Destroy(gameObject);
	}


    public void SetFoodCollider(GameObject coll)
    {
        _collider = coll;
    }
}
