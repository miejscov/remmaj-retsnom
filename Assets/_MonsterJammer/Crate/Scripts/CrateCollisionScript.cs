using UnityEngine;

	public class CrateCollisionScript : MonoBehaviour
	{
		private bool _onWall = false;
		private bool _onCrate = false;
		private bool _onPlayer = false;

		public GameObject _objectInTrigger;

//		private void OnCollisionEnter(Collision other)
//		{
//			{
//				if (other.gameObject.CompareTag("Player"))
//				{
//					ResetColision();
//					_onPlayer = true;
//					_objectInTrigger = other.gameObject;
//				}
//					
//				else if (other.gameObject.CompareTag("Wall"))
//				{
//					_onWall = true;
//				}
//				
//				else if (other.gameObject.CompareTag("Crate"))
//				{
//					_onCrate = true;
//				}
//			}
//		}


//		private void OnCollisionExit(Collision other)
//		{
//			if (other.gameObject.CompareTag("Player"))
//			{
//				_onPlayer = false;
//				_objectInTrigger = null;
//			}
//		
//			else if (other.gameObject.CompareTag("Wall"))
//			{
//				_onWall = false;
//			}
//		}
//
//		private void ResetColision()
//		{
//			_onCrate = false;
//		}
//		
////		public bool OnWall()
////		{
////			return _onWall;
////		}
////
////		public bool OnCrate()
////		{
////			return _onCrate;
////		}
////
////		public bool OnPlayer()
////		{
////			return _onPlayer;
////		}
	}
