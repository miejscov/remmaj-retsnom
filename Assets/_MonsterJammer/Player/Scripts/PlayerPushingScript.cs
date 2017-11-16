using UnityEngine;

	public class PlayerPushingScript : MonoBehaviour
	{
		private Animator _animator;


		private void Start()
		{
			_animator = GetComponent<Animator>();
		}

		private void OnCollisionEnter (Collision col)
		{
			if(col.gameObject.CompareTag("Wall"))
			{
				_animator.SetBool("WallContact", true);
			}
		}

		private void OnCollisionExit(Collision col)
		{
			if (col.gameObject.CompareTag("Wall"))
			_animator.SetBool("WallContact", false);
		}
	}
