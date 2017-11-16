using System;
using UnityEngine;

	public class PlayerRotationScript : MonoBehaviour
	{
		private Quaternion _direction;
		public float RotationSpeed = 10F;
		private Quaternion _left, _right, _forward, _back;

		private void Start ()
		{
			_left = Quaternion.Euler(0, -90, 0);
			_right = Quaternion.Euler(0, 90, 0);
			_forward = Quaternion.Euler(0, 0, 0);
			_back = Quaternion.Euler(0, 180, 0);
		}
	
		private void FixedUpdate()
		{
			TurnInDirection(_direction);
		}
		
		public void TurnLeft() 
		{ 
			_direction = _left; 
		}

		public void TurnRight() 
		{ 
			_direction = _right;
		}

		public void TurnBack()
		{
			_direction = _back;
		}

		public void Forward()
		{
			_direction = _forward;
		}
		
		private void TurnInDirection(Quaternion direction)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, direction, Time.deltaTime * RotationSpeed);
		}
	}












