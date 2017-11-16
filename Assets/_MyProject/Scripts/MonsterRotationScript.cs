using UnityEngine;


public class MonsterRotationScript : MonoBehaviour
{
	private MonsterRbMoveScript _monsterRbMoveScript;
		
	private Quaternion _direction;
    public Transform _player;
	public float RotationSpeed = 10F;
    public float _rotationThreshold = 30;
    public bool _rotationOk = false;
    public float _rotationComparison;
    public bool _stopped;

    private Quaternion _left, _right, _forward, _back;
	void Start ()
	{
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		_monsterRbMoveScript = GetComponent<MonsterRbMoveScript>();
			
		_left = Quaternion.Euler(0, -90, 0);
		_right = Quaternion.Euler(0, 90, 0);
		_forward = Quaternion.Euler(0, 0, 0);
		_back = Quaternion.Euler(0, 180, 0);
	}
	
	private void Update()
	{
//			if(!_playerRbMoveScript.IsMoving())
			TurnInDirection(_direction);
        _rotationOk = IsRotationOk();
	}

    public void TurnTowardsPlayer()
    {
        SetDirection(Quaternion.LookRotation(new Vector3(_player.position.x - transform.position.x, 0f, _player.position.z - transform.position.z)));
    }

	public void TurnLeft()
	{
		SetDirection(_left);
	}

	public void TurnRight()
	{
		SetDirection(_right);
	}

	public void TurnBack()
	{
		SetDirection(_back);
	}

	public void Forward()
	{
		SetDirection(_forward);
	}

	private void SetDirection(Quaternion direction)
	{
		_direction = direction;
	}
		
	private void TurnInDirection(Quaternion direction)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, direction, Time.deltaTime * RotationSpeed);
	}

	public bool IsRotating()
	{
		var isRotating = !(transform.rotation.Equals(_direction));
		return isRotating;
	}

    public bool IsRotationOk()
    {
        bool isRotationOk = Mathf.DeltaAngle(transform.rotation.y, _direction.y) < _rotationThreshold;
        _rotationComparison = Mathf.DeltaAngle(transform.rotation.y, _direction.y);
//            Debug.Log("Rotation difference to forward = " + Vector3.Angle(transform.forward, Vector3.forward));
        return isRotationOk;
    }
    public void SetStopped()
    {
        _stopped = true;
    }
}

