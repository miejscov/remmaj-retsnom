using UnityEngine;

	public class PlayerControlScript : MonoBehaviour
	{
		private PlayerRotationScript _playerRotation;
		private PlayerRbMoveScript _playerRbMove;
		private PlayerStatusScript _playerStatus;
		private PlayerRayCastHit _playerRay;
		private PlayerAnimationControlScript _playerAnimation;
		private Animator _animator;
		
		Vector3 direction;
		Quaternion rotation;
		private bool _isOnDirection = false;
		
		private bool _invertControl;
		private bool _freezePlayer;
		
		public void SetInvertControl(bool invert)
		{
			_invertControl = invert;
		}

		public void SetFreezePlayer(bool freeze)
		{
			_freezePlayer = freeze;
		}

		private void Start ()
		{
			_playerRotation = GetComponent<PlayerRotationScript>();
			_playerRbMove = GetComponent<PlayerRbMoveScript>();
			_playerStatus = GetComponent<PlayerStatusScript>();
			_playerRay = GetComponentInChildren<PlayerRayCastHit>();
			_playerAnimation = GetComponent<PlayerAnimationControlScript>();
			_animator = GetComponent<Animator>();
		}
		
		private void Update ()
		{
			if(_playerStatus.PlayerIsDead() == false)
				CheckControlKeys();
		}

		private void CheckControlKeys()
		{
			if (_freezePlayer) return;
			if (_playerRbMove.IsMoving()) return;
			if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Standing_1H_Magic_Attack_02")) return;
			
			if (Input.GetKey(KeyCode.UpArrow))
			{
				if (_invertControl == false)
				{
					MoveForward();
				}
				else
				{
					MoveBack();
				}
			}
			
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				if (_invertControl == false)
				{
					MoveBack();
				}
				else
				{
					MoveForward();
				}
			}
			
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				if (_invertControl == false)
				{
					MoveRight();
				}
				else
				{
					MoveLeft();
				}
			}

			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				if (_invertControl == false)
				{
					MoveLeft();
				}
				else
				{
					MoveRight();
				}
			}
		}

		private void MoveForward()
		{
			direction = Vector3.forward;
			rotation = Quaternion.Euler(0, 0, 0);
				
			_playerRotation.Forward();
			if (_playerRay.CheckActionInDirection(direction, rotation) == 1 && _playerStatus.GetAmountOfEnergy() > 0) // can hit
			{
				_playerAnimation.PlayHitAnimation();
				_playerRay.HitObject().GetComponent<CrateControlScript>().DestroyCrate();
			}
			else if (_playerRay.CheckActionInDirection(direction, rotation) == 2) //can move
			{
				if (_playerRay.HitObject() != null && _playerRay.HitObject().gameObject.tag.Equals("Crate"))
				{
					_playerRay.HitObject().GetComponent<CrateMovementScript>().MoveForward();
				}
				_playerRbMove.MoveForward();
			}
			else if (_playerRay.CheckActionInDirection(direction, rotation) == 0) //can't move
			{
				_playerAnimation.PlayCantPush();
			}
		}

		private void MoveBack()
		{
			direction = Vector3.back;
			rotation = Quaternion.Euler(0, 180, 0);
				
			_playerRotation.TurnBack();
			if (_playerRay.CheckActionInDirection(direction, rotation) == 1 && _playerStatus.GetAmountOfEnergy() > 0) // can hit
			{
				_playerAnimation.PlayHitAnimation();
				_playerRay.HitObject().GetComponent<CrateControlScript>().DestroyCrate();
					
			}
			else if (_playerRay.CheckActionInDirection(direction, rotation) == 2) // can move
			{
				if (_playerRay.HitObject() != null && _playerRay.HitObject().gameObject.tag.Equals("Crate"))
				{
					_playerRay.HitObject().GetComponent<CrateMovementScript>().MoveBack();
				}
				_playerRbMove.MoveBackward();
			}
			else if (_playerRay.CheckActionInDirection(direction, rotation) == 0)
			{
				_playerAnimation.PlayCantPush();
			}
		}

		private void MoveRight()
		{
			direction = Vector3.right;
			rotation = Quaternion.Euler(0, 90, 0);
				
			_playerRotation.TurnRight();
			if (_playerRay.CheckActionInDirection(direction, rotation) == 1 && _playerStatus.GetAmountOfEnergy() > 0) // can hit
			{
				_playerAnimation.PlayHitAnimation();
				_playerRay.HitObject().GetComponent<CrateControlScript>().DestroyCrate();
			}
			else if (_playerRay.CheckActionInDirection(direction, rotation) == 2)
			{
				if (_playerRay.HitObject() != null && _playerRay.HitObject().gameObject.tag.Equals("Crate"))
				{
					_playerRay.HitObject().GetComponent<CrateMovementScript>().MoveRight();
				}
				_playerRbMove.MoveRight();
			}
			else if (_playerRay.CheckActionInDirection(direction, rotation) == 0)
			{
				_playerAnimation.PlayCantPush();
			}
		}
		
		private void MoveLeft()
		{
			direction = Vector3.left;
			rotation = Quaternion.Euler(0, -90, 0);
				
			_playerRotation.TurnLeft();
			if (_playerRay.CheckActionInDirection(direction, rotation) == 1 && _playerStatus.GetAmountOfEnergy() > 0) // can hit
			{
				_playerAnimation.PlayHitAnimation();
				_playerRay.HitObject().GetComponent<CrateControlScript>().DestroyCrate();
			}
			else if (_playerRay.CheckActionInDirection(direction, rotation) == 2)
			{
				if (_playerRay.HitObject() != null && _playerRay.HitObject().gameObject.tag.Equals("Crate"))
				{
					_playerRay.HitObject().GetComponent<CrateMovementScript>().MoveLeft();
				}
				_playerRbMove.MoveLeft();
			}
			else if(_playerRay.CheckActionInDirection(direction, rotation) == 0)
			{
				_playerAnimation.PlayCantPush();
			}
		}
	}






















































//using System.Runtime.Remoting.Messaging;
//using UnityEditor;
//using UnityEngine;
//
//	public class PlayerControlScript : MonoBehaviour
//	{
//		private PlayerRotationScript _playerRotation;
//		private PlayerRbMoveScript _playerRbMove;
//		private PlayerStatusScript _playerStatus;
//		private PlayerRayCastHit _playerRay;
//		private PlayerAnimationControlScript _playerAnimation;
//		private Animator _animator;
//		
//		private bool _isOnDirection = false;
//
//		private void Start ()
//		{
//			_playerRotation = GetComponent<PlayerRotationScript>();
//			_playerRbMove = GetComponent<PlayerRbMoveScript>();
//			_playerStatus = GetComponent<PlayerStatusScript>();
//			_playerRay = GetComponentInChildren<PlayerRayCastHit>();
//			_playerAnimation = GetComponent<PlayerAnimationControlScript>();
//			_animator = GetComponent<Animator>();
//		}
//		
//		private void Update ()
//		{
//			if(_playerStatus.PlayerIsDead() == false)
//				CheckControlKeys();
//		}
//
//		private void CheckControlKeys()
//		{
//			if (_playerRbMove.IsMoving()) return;
//			if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Grab")) return;
//			if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Standing_1H_Magic_Attack_02")) return;
//			
//			Vector3 direction;
//			Quaternion rotation;
//			
//			if (Input.GetKey(KeyCode.UpArrow))
//			{
//				direction = Vector3.forward;
//				rotation = Quaternion.Euler(0, 0, 0);
//				
//				_playerRotation.Forward();
//				if (_playerRay.CheckActionInDirection(direction, rotation) == 1 && _playerStatus.GetAmountOfEnergy() > 0) // can hit
//				{
//					_playerAnimation.PlayHitAnimation();
//				}
//				else if (_playerRay.CheckActionInDirection(direction, rotation) == 2) //can move
//				{
//					if (_playerRay.HitObject() != null && _playerRay.HitObject().gameObject.tag.Equals("Crate"))
//					{
//					   	_playerRay.HitObject().GetComponent<CrateMovementScript>().MoveForward();
//					}
//						_playerRbMove.MoveForward();
//				}
//				else if (_playerRay.CheckActionInDirection(direction, rotation) == 0) //can't move
//				{
//					_playerAnimation.PlayCantPush();
//				}
//			}
//			
//			
//			
//			else if (Input.GetKey(KeyCode.DownArrow))
//			{
//				direction = Vector3.back;
//				rotation = Quaternion.Euler(0, 180, 0);
//				
//				_playerRotation.TurnBack();
//				if (_playerRay.CheckActionInDirection(direction, rotation) == 1 && _playerStatus.GetAmountOfEnergy() > 0) // can hit
//				{
//					_playerAnimation.PlayHitAnimation();
//				}
//				else if (_playerRay.CheckActionInDirection(direction, rotation) == 2) // can move
//				{
//					if (_playerRay.HitObject() != null && _playerRay.HitObject().gameObject.tag.Equals("Crate"))
//					{
//						_playerRay.HitObject().GetComponent<CrateMovementScript>().MoveBack();
//					}
//					_playerRbMove.MoveBackward();
//				}
//				else if (_playerRay.CheckActionInDirection(direction, rotation) == 0)
//				{
//					_playerAnimation.PlayCantPush();
//				}
//			}
//			
//			
//			
//			
//			
//			
//			else if (Input.GetKey(KeyCode.RightArrow))
//			{
//				direction = Vector3.right;
//				rotation = Quaternion.Euler(0, 90, 0);
//				
//				_playerRotation.TurnRight();
//				if (_playerRay.CheckActionInDirection(direction, rotation) == 1 && _playerStatus.GetAmountOfEnergy() > 0) // can hit
//				{
//					_playerAnimation.PlayHitAnimation();
//				}
//				else if (_playerRay.CheckActionInDirection(direction, rotation) == 2)
//				{
//					if (_playerRay.HitObject() != null && _playerRay.HitObject().gameObject.tag.Equals("Crate"))
//					{
//						_playerRay.HitObject().GetComponent<CrateMovementScript>().MoveRight();
//					}
//					_playerRbMove.MoveRight();
//				}
//				else if (_playerRay.CheckActionInDirection(direction, rotation) == 0)
//				{
//					_playerAnimation.PlayCantPush();
//				}
//			}
//			
//			
//			
//			
//			
//			
//			else if (Input.GetKey(KeyCode.LeftArrow))
//			{
//				direction = Vector3.left;
//				rotation = Quaternion.Euler(0, -90, 0);
//				
//				_playerRotation.TurnLeft();
//				if (_playerRay.CheckActionInDirection(direction, rotation) == 1 && _playerStatus.GetAmountOfEnergy() > 0) // can hit
//				{
//					_playerAnimation.PlayHitAnimation();
//				}
//				else if (_playerRay.CheckActionInDirection(direction, rotation) == 2)
//				{
//					if (_playerRay.HitObject() != null && _playerRay.HitObject().gameObject.tag.Equals("Crate"))
//					{
//						_playerRay.HitObject().GetComponent<CrateMovementScript>().MoveLeft();
//					}
//					_playerRbMove.MoveLeft();
//				}
//				else if(_playerRay.CheckActionInDirection(direction, rotation) == 0)
//				{
//					_playerAnimation.PlayCantPush();
//				}
//			}
//		}
//	}
