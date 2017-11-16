using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
	private bool _canPush = false;
	private bool _onCrate;
	private bool _canDie = false;
	private bool _slowDownUpPlayerFlag;
	private GameObject _crateInTrigger;
	private PlayerStatusScript _playerStatus;
	
	public bool OnCrate() {return _onCrate;}

	public void ResetOnCrate(){_onCrate = false;}

	private void Start()
	{
		_playerStatus = GetComponent<PlayerStatusScript>();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Crate"))
			_onCrate = true;
			
		else if (other.gameObject.CompareTag("Diamond"))
		{
			_playerStatus.AddDiamond();
			_playerStatus.AddPlayerScore(20);
			other.gameObject.GetComponent<DiamondControlScript1>().DestroyDiamond();
		}
		else if (other.gameObject.CompareTag("Food"))
		{
			_playerStatus.AddEnergy();
			_playerStatus.AddPlayerScore(10);
			other.gameObject.GetComponent<FoodControlScript>().DestroyFood();
		}
		else if (other.gameObject.name.Contains("ExtraLife"))
		{
			_playerStatus.AddPlayerLife();
			other.gameObject.GetComponent<ExtraLifeScript>().DestroyExtraLifeObject();
		}
		else if (other.gameObject.name.Contains("SlowDownPlayer"))
		{
			_playerStatus.AddPlayerScore(5);
			other.gameObject.GetComponent<SlowDownPlayerSurprise>().Set();
		}
		else if (other.gameObject.name.Contains("SpeedUpPlayer"))
		{
			_playerStatus.AddPlayerScore(5);
			other.gameObject.GetComponent<SpeedUpPlayerSurprise>().Set();
		}
		else if (other.gameObject.name.Contains("InvertControl"))
        {
        	_playerStatus.AddPlayerScore(5);
        	other.gameObject.GetComponent<InvertControlSurpriseScript>().Set();
        }
        else if (other.gameObject.name.Contains("FreezePlayer"))
        {
        	_playerStatus.AddPlayerScore(5);
        	other.gameObject.GetComponent<FreezePlayerSurpriseScript>().Set();
        }
	}

	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.CompareTag("Crate"))
			_onCrate = false;
	}
}