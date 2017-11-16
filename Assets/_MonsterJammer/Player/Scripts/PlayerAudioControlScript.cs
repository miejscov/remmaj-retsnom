using UnityEngine;

	public class PlayerAudioControlScript : MonoBehaviour
	{

		public AudioClip DestroyCrate;
		public AudioClip GetDiamond;
		public AudioClip PlayerIsDying;
		public AudioClip GetFood;
		private AudioSource _audioSource;
	
		private void Start ()
		{
			_audioSource = GetComponent<AudioSource>();
		}


		public void PlayDestroyCrateSound()
		{
			_audioSource.PlayOneShot(DestroyCrate);
		}

		public void PlayGetDiamondSound()
		{
			_audioSource.PlayOneShot(GetDiamond);
		}

		public void PlayGetFoodSound()
		{
			_audioSource.PlayOneShot(GetFood);
		}

		public void PlayerIsDyingSound()
		{
			_audioSource.PlayOneShot(PlayerIsDying);
		}
	}
