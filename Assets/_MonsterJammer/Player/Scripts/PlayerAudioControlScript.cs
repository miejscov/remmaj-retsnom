using UnityEngine;

	public class PlayerAudioControlScript : MonoBehaviour
	{

		public AudioClip DestroyCrate;
		public AudioClip GetDiamond;
		public AudioClip PlayerIsDying;
		public AudioClip GetFood;
		public AudioClip ExtraLifeSound;
		private AudioSource _audioSource;
	
		private void Start ()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		public void PlayeExtraLifeSound()
		{
			_audioSource.PlayOneShot(ExtraLifeSound);
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
