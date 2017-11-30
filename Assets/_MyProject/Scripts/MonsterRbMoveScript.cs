using System;
using UnityEngine;


public class MonsterRbMoveScript : MonoBehaviour
{
	public Vector3 _target;
    public Transform _player;
    public float _moveSpeed = 10f;
    public bool _stopped;
    private float _yPos;

    private void Start ()
	{
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _yPos = transform.position.y;
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

	private bool IsMovingX()
	{
		var isMoving = !(Math.Abs(transform.position.x - _target.x) < .2f);
		return isMoving;
	}

	private bool IsMovingZ()
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
        transform.position = Vector3.Slerp(transform.position, _target, _moveSpeed * Time.deltaTime);
    }

    public void MoveTowardsPlayer()
    {
        _target = _player.position;
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

