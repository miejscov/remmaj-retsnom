using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTriggerPivotScript : MonoBehaviour {

    private Quaternion _initialRotation;

	// Use this for initialization
	void Start () {
        _initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.rotation = _initialRotation;
	}
}
