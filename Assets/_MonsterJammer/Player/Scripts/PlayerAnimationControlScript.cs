using UnityEngine;

public class PlayerAnimationControlScript : MonoBehaviour
{
	private PlayerCollisionScript _playerCollision;
	private PlayerRbMoveScript _playerRbMove;
	private PlayerStatusScript _playerStatus;
	private Animator _animator;

	private void Start ()
	{
		_playerCollision = GetComponent<PlayerCollisionScript>();
		_playerRbMove = GetComponent<PlayerRbMoveScript>();
		_playerStatus = GetComponent<PlayerStatusScript>();
		_animator = GetComponent<Animator>();
	}

	private void Update () {

		if (_playerStatus.PlayerIsDead())
		{
			_animator.Play("DeadBackward");
		}

		if (_playerCollision.OnCrate())
		{
			_animator.SetBool("IsPushing", true); 
		}
		else
		{
			_animator.SetBool("IsPushing", false);
			_animator.SetBool("IsWalking", _playerRbMove.IsMoving());
		}
	}

		public void PlayHitAnimation()
		{
			_animator.StopPlayback();
			_animator.Play("Standing_1H_Magic_Attack_02");
		}

		public void PlayerWalk()
		{
			_animator.Play("PlayerForward2");
		}


		public void PlayerIdle()
		{
			_animator.StopPlayback();
			_animator.Play("Idle_Neutral_1");
		}

		public void PlayCantPush()
		{
//			_animator.StopPlayback();
			_animator.Play("CantPush");
		}

		public void PlayGrab()
		{
			_animator.Play("Grab");
		}
	}
