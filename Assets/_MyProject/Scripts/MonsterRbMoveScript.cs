using System;
using UnityEngine;


public class MonsterRbMoveScript : MonoBehaviour
{
	//private Rigidbody transform;
	public Vector3 _target;
    public Transform _player;
//	public float _toVel = 19f; //2.5f;
//	public float _maxVel = 6f; //15.0f;
//	public float _maxForce = 147f; //40.0f;
//	public float _gain = 36f; //5f;
    public float _moveSpeed = 10f;
    public bool _stopped;
    private Vector3 velocity = Vector3.zero;
    private float _yPos;

    private void Start ()
	{
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _yPos = transform.position.y;
//		transform = GetComponent<Rigidbody>();
		_target = transform.position;
        _stopped = false;
	}

	private void Update () 
	{
        if (!_stopped)
        {
            MoveMonster();
        }

	}

	public bool IsMovingX()
	{
		var isMoving = !(Math.Abs(transform.position.x - _target.x) < .2f);
		return isMoving;
	}
		
	public bool IsMovingZ()
	{
		var isMoving = !(Math.Abs(transform.position.z - _target.z) < .2f);
		return isMoving;
	}

	public bool IsMoving()
	{
		var isMoving = false;
		if ((Math.Abs(transform.position.x - _target.x) < .2f) && (Math.Abs(transform.position.z - _target.z) < .2f))
			isMoving = false;
		else
		{
			isMoving = true;
		}
		return isMoving;
	}
	
	private void MoveMonster(){
        //var dist = _target - transform.position;
        //dist.y = 0;
        //var tgtVel = Vector3.ClampMagnitude(_toVel * dist, _maxVel);
        //var error = tgtVel - _rb.velocity;
        //var force = Vector3.ClampMagnitude(_gain * error, _maxForce);
        //_rb.AddForce(force);

       // _target.y = transform.position.y;
        transform.position = Vector3.Slerp(transform.position, _target, _moveSpeed * Time.deltaTime);
//        transform.position = Vector3.SmoothDamp(transform.position, _target, ref velocity, _moveSpeed * Time.deltaTime);
    }

    public void MoveTowardsPlayer()
    {
        _target = _player.position;
        //_target.x = Mathf.Round(_player.position.x);
        //_target.z = Mathf.Round(_player.position.z);
        _target.y = _yPos;
    }

    public void MoveForward()
	{
		if (!IsMovingX())

		_target.z = Mathf.Round(transform.position.z) + 1;
        _target.x = Mathf.Round(transform.position.x);
    }

	public void MoveBackward()
	{
		if (!IsMovingX())
		_target.z = Mathf.Round(transform.position.z) - 1;
        _target.x = Mathf.Round(transform.position.x);
    }

	public void MoveLeft()
	{
		if (!IsMovingZ())
		_target.x = Mathf.Round(transform.position.x) - 1;
        _target.z = Mathf.Round(transform.position.z);

    }

	public void MoveRight()
	{
		if (!IsMovingZ())
		_target.x = Mathf.Round(transform.position.x) + 1;
        _target.z = Mathf.Round(transform.position.z);

    }

    public void IsStopped(bool value)
    {
        _stopped = value;
    }
}

