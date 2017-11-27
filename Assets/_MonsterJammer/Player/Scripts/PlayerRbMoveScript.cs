using System;
using UnityEngine;

public class PlayerRbMoveScript : MonoBehaviour
{
    private Vector3 _targetPosition;
    private GameControlScript _gameControl;
    public float MoveSpeed;

    private float _actualSpeed;
    public bool _isStopped;
    
    private void Start()
    {
        _gameControl = GameObject.Find("GameControl").GetComponent<GameControlScript>();
        MoveSpeed = _gameControl.GetDefaultPlayerSpeed();
        _actualSpeed = MoveSpeed;
        _targetPosition = transform.position;
    }
   
    private void FixedUpdate ()
    {
        if (_isStopped) return;
        Move();
    }
		
    public bool IsMoving()
    {
        var isMoving = false;
        if ((Math.Abs(transform.position.x - _targetPosition.x) < 0.05f) && (Math.Abs(transform.position.z - _targetPosition.z) < 0.05f))
            isMoving = false;
        else
            isMoving = true;

        return isMoving;
    }
	
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _actualSpeed * Time.deltaTime);
    }

    public void MoveForward()
    {
        _targetPosition.z = Mathf.Round(transform.position.z) + 1;
    }

    public void MoveBackward()
    {
        _targetPosition.z = Mathf.Round(transform.position.z) - 1;
    }

    public void MoveLeft()
    {
        _targetPosition.x = Mathf.Round(transform.position.x) - 1;
    }

    public void MoveRight()
    {
        _targetPosition.x = Mathf.Round(transform.position.x) + 1;
    }

    public void SetPlayerSpeed(float speed)
    {
        _actualSpeed = speed;
    }

    public void ResetSpeed()
    {
        _actualSpeed = MoveSpeed;
    }

    public void SetPlayerPosition(Vector3 position)
    {
        SetPlayerTargetPosition(position);
        var pos = new Vector3(Mathf.Round(position.x), 0f, Mathf.Round(position.z));
        transform.position = pos;
        _targetPosition = transform.position;
    }

    public void SetPlayerTargetPosition(Vector3 pos)
    {
        _targetPosition = new Vector3(Mathf.Round(pos.x), transform.position.y, Mathf.Round(pos.z));
    }
    

    public bool IsStopped
    {
        set
        {
            _isStopped = value;
            _targetPosition = transform.position;
        }
    }
}








































//using System;
//using UnityEngine;
//
//public class PlayerRbMoveScript : MonoBehaviour
//{
//	private Rigidbody _rb;
//	private Vector3 _target;
//	public float _toVel = 19f; //2.5f;			//private
//	public float _maxVel = 6f; //15.0f;			//
//	public float _maxForce = 147f; //40.0f;		//
//	public float _gain = 36f; //5f;				//
//	private Vector3 _lastCorrectPosition;
//
//	private void Start ()
//	{
//		_rb = GetComponent<Rigidbody>();
//		_target = _lastCorrectPosition = _rb.position;
//	}
//
//	private void FixedUpdate () 
//	{
//		MovePlayer();
//	}
//
//	public bool IsMoving()
//	{
//		var isMoving = false;
//		if ((Math.Abs(_rb.position.x - _target.x) < 0.1f) && (Math.Abs(_rb.position.z - _target.z) < 0.1f))
//			isMoving = false;
//		else
//		{
//			isMoving = true;
//		}
//		return isMoving;
//	}
//		
//	public bool IsMovingForAnimator()
//	{
//		var isMoving = false;
//		if ((Math.Abs(_rb.position.x - _target.x) < 0.4f) && (Math.Abs(_rb.position.z - _target.z) < 0.4f))
//			isMoving = false;
//		else
//		{
//			isMoving = true;
//		}
//		return isMoving;
//	}
//	
//	private void MovePlayer(){
//		var dist = _target - transform.position;
//		dist.y = 0;
//		var tgtVel = Vector3.ClampMagnitude(_toVel * dist, _maxVel);
//		var error = tgtVel - _rb.velocity;
//		var force = Vector3.ClampMagnitude(_gain * error, _maxForce);
//		_rb.AddForce(force);
//	}
//
//	public void MoveForward()
//	{
//			_target.z = Mathf.Round(_rb.position.z) + 1;
//	}
//
//	public void MoveBackward()
//	{
//			_target.z = Mathf.Round(_rb.position.z) - 1;
//			
//	}
//
//	public void MoveLeft()
//	{
//			_target.x = Mathf.Round(_rb.position.x) - 1;
//	}
//
//	public void MoveRight()
//	{
//			_target.x = Mathf.Round(_rb.position.x) + 1;
//	}
//}
