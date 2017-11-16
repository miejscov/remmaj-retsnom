using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveColliderBoxScript : MonoBehaviour {

    public float _lifeTime = 600f;
    public float _countDownSpeed = 1f;
    public float _currentLifeTime;
	// Use this for initialization
	void Start () {
        _currentLifeTime = _lifeTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (_currentLifeTime > 0)
        {
            _currentLifeTime -= Time.deltaTime * _countDownSpeed;
        } else
        {
            Destroy(this.gameObject);
        }
	}
}
