using UnityEngine;

public class SlowDownPlayerSurprise : MonoBehaviour
{

	private PlayerRbMoveScript _playerRbMove;
	private BoxCollider _boxCollider;
	private MeshRenderer _meshRenderer;
	private GameControlScript _gameControl;

	private const float SlowMotionTime = 400f;

	private void Start ()
	{
		_gameControl = GameObject.Find("GameControl").GetComponent<GameControlScript>();
		_boxCollider = GetComponent<BoxCollider>();
		_meshRenderer = GetComponent<MeshRenderer>();
	}
	
	public void Set()
	{
		_playerRbMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRbMoveScript>();
		
		_playerRbMove.SetPlayerSpeed(1);
		_gameControl.SetCurrentCrateSpeed(1);
		
		_boxCollider.enabled = false;
		_meshRenderer.enabled = false;
		Invoke("StopSlowMotion", SlowMotionTime * Time.deltaTime);
	}

	private void StopSlowMotion()
	{
		_playerRbMove.ResetSpeed();
		_gameControl.ResetCrateSpeed();
		Destroy(gameObject);
	}
}
