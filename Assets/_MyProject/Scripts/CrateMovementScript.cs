
using System;
using UnityEngine;

	public class CrateMovementScript : MonoBehaviour
	{
		private GameControlScript _gameControl;
		private Vector3 _targetPosition;
		public float _moveSpeed = 4f;


		private void Start()
		{
			_gameControl = GameObject.Find("GameControl").GetComponent<GameControlScript>();
			_targetPosition = transform.position;
		}
	
		private void Update () 
		{
			Move();
		}
	
		private void Move()
		{
			transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
		}

		public void MoveForward()
		{
			_moveSpeed = _gameControl.GetCrateSpeed();
			_targetPosition.z = Mathf.Round(transform.position.z) + 1;
		}

		public void MoveBack()
		{
			_moveSpeed = _gameControl.GetCrateSpeed();
			_targetPosition.z = Mathf.Round(transform.position.z) - 1;
		}

		public void MoveLeft()
		{
			_moveSpeed = _gameControl.GetCrateSpeed();
			_targetPosition.x = Mathf.Round(transform.position.x) - 1;
		}

		public void MoveRight()
		{
			_moveSpeed = _gameControl.GetCrateSpeed();
			_targetPosition.x = Mathf.Round(transform.position.x) + 1;
		}
	}






























//using System;
//using UnityEngine;
//
//	public class CrateMovementScript : MonoBehaviour
//	{
//		public Vector3 _targetPosition;
//		private const float _toVel = 19f;
//		private const float _maxVel = 6.0f;
//		private const float _maxForce = 147.0f;
//		private const float _gain = 36f;
//		private Rigidbody _rb;
//
//		private AudioSource _audio;
//		private Transform _object;
//		private Vector3 _lastPosition;
//		private Vector3 _offset;
//		private const float Threshold = 0.4f;
//
//		private bool _move;
//	
//		private void Start() 
//		{
//			_rb = GetComponent<Rigidbody>();
//			_object = GetComponent<Transform>();
//			_audio = GetComponent<AudioSource>();
//			_lastPosition = _targetPosition = _object.position;
//		}
//	
//		private void Update () 
//		{
//			RbMove();
//			if (IsAtTargetPosition())
//			{
//				_move = false;
//				_audio.Play();
//			}
//			
//
//			if (!_move)
//			{
//				DetectMoveDirection();
//			}
//			else
//			{
//				_lastPosition = _targetPosition;
//			}
//		}
//
//		private void DetectMoveDirection()
//		{
//			if (_move == true) return;
//		
//			_offset = _object.position - _lastPosition;
//	
//			if (_offset.x > Threshold)
//				MoveRight();
//			else if (_offset.x < -Threshold)
//				MoveLeft();
//			else if (_offset.z > Threshold)
//				MoveForward();
//			else if (_offset.z < - Threshold)
//				MoveBack();			
//		}
//	
//		private void RbMove()
//		{
//			var dist = _targetPosition - transform.position;
//			dist.y = 0;
//			var tgtVel = Vector3.ClampMagnitude(_toVel * dist, _maxVel);
//			var error = tgtVel - _rb.velocity;
//			var force = Vector3.ClampMagnitude(_gain * error, _maxForce);
//			_rb.AddForce(force);
//		}
//	
//		private bool IsAtTargetPosition()
//		{
//			var atPosition = (Math.Abs(_rb.position.x - _targetPosition.x) < .3f) &&
//			                 (Math.Abs(_rb.position.z - _targetPosition.z) < .3f);
//			return atPosition;
//		}
//
//		private void MoveRight()
//		{
//			_targetPosition.x = Mathf.Round(_lastPosition.x)+ 1;
//			_move = true;
//		}
//
//		private void MoveLeft()
//		{
//			_targetPosition.x = Mathf.Round(_lastPosition.x)- 1;
//			_move = true;
//		}
//
//		private void MoveForward()
//		{
//			_targetPosition.z = Mathf.Round(_rb.position.z)+ 1;
//			_move = true;
//		}
//
//		private void MoveBack()
//		{
//			_targetPosition.z = Mathf.Round(_rb.position.z)-1;
//			_move = true;
//		}
//	}
