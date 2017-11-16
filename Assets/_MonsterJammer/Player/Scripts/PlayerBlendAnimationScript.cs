using UnityEngine;

	public class PlayerBlendAnimationScript : MonoBehaviour
	{
		private Animator _animator;

		private float _speed = .001f;

		private void Start()
		{
			if (_animator == null) 
				_animator = GetComponent<Animator>();
		}


		private void Update()
		{
			SetFloatSmoothly(_speed);
			Debug.Log("Float smooth");
		}
	
	
		private void SetFloatSmoothly(float speed)
		{

//			speed += Time.deltaTime;
			speed -= Time.deltaTime;
			speed = Mathf.Clamp(speed, 0f, 1f);
			
			_animator.SetFloat("AnimationBlend", speed);
		}
	
	
		public void SetAnimationBlendFloat(float speed)
		{
			_speed = speed;
		}
	}
