using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hello world!!!

public class MonsterControlScript : MonoBehaviour
{
    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private Vector3 _playerPosition;
	private MonsterRotationScript _monsterRotationScript;
	private MonsterRbMoveScript _monsterRbMoveScript;
    private MonsterAudioScript _monsterAudioScript;
    public PlayerStatusScript _playerScript;
    public bool _blockedFront, _blockedRight, _blockedBack, _blockedLeft;
    public Transform _boxPrefab;
    public float _moveColliderErrorMargin = 0.1f;
    public float _rayCastLength = 1f;
    public float _sinkSpeed = 0.1f;
    public float _sinkDistance = -2f;
    public float _dieTime = 1f;
    public bool _debugControl = false;
    public float _maxThinkingTime = 1f;
    public float _startTime = 5f;
    public float _playerCheckTime = 20f;
    public int _monsterDir;
    public float _distanceFromPlayer;
    public float _playerAttackDistance = 1.2f;
    private const int DIR_FRONT = 1;
    private const int DIR_RIGHT = 2;
    private const int DIR_BACK = 3;
    private const int DIR_LEFT = 4;

    public bool _attacking = false;
    public bool _die = false;
    private float _startPostionY;
    private bool _sinking;
    public float _thinkingTime;
    private bool _itemsOnMap = false;
	private Animator _animator;

	private void Start ()
	{
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatusScript>();
        _monsterAudioScript = GetComponent<MonsterAudioScript>();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _startTime = Random.Range(0.0f, 10.0f) / 10 + _startTime;
    	_animator = GetComponent<Animator>();
        _monsterRotationScript = GetComponent<MonsterRotationScript>();
		_monsterRbMoveScript = GetComponent<MonsterRbMoveScript>();
        _startPostionY = transform.position.y;
        _thinkingTime = _startTime;
        _sinking = false;
    }

    private void Update()
    {
        if (!_die)
        {
            if (_debugControl)
            {
                CheckKeyControl();
            } else
            {
                MonsterControl();
            }
        }
        else
        {
            if (_sinking) {
             
                if (transform.position.y > _sinkDistance)
                {
                    if (!_itemsOnMap)
                    {
                        GetComponent<ItemGeneratorScript>().generateDiamonds();
                        GetComponent<ItemGeneratorScript>().GeneratePrefAtPlace("food");
                        _itemsOnMap = true;
                    }
                    //               Debug.Log("Sinking!!!");
                    transform.Translate(Vector3.down * _sinkSpeed * Time.deltaTime);
                } 
            }

        }
	}

    private void MonsterControl()
    {
        if (_attacking)
        {
            if (!_playerScript.PlayerIsDead())
            {
                _monsterRbMoveScript.MoveTowardsPlayer();
            }
            else {
                _monsterRbMoveScript.IsStopped(true);
            }
            _monsterRotationScript.TurnTowardsPlayer();
        } else if (_thinkingTime <= 0f)
        {
            CheckForPlayer();
            if (CheckIfJammed()) return;
               
            _monsterDir = Random.Range(1, 5);
            _thinkingTime = _maxThinkingTime;
            if (!_blockedFront && _monsterDir == DIR_FRONT) MoveForward();
            else if (!_blockedRight && _monsterDir == DIR_RIGHT) MoveRight();
            else if (!_blockedBack && _monsterDir == DIR_BACK) MoveBack();
            else if (!_blockedLeft && _monsterDir == DIR_LEFT) MoveLeft();

    } else
        {
            CheckForPlayer();
            _thinkingTime -= Time.deltaTime;
        }
    }

    private bool CheckIfJammed()
    {
        int jammedDirections;
        jammedDirections = 0;
        ResetTriggers();
        RaycastHit hit = new RaycastHit();
        Vector3 rayOrigin = transform.position + Vector3.up * 0.35f;
        if (Physics.Raycast(rayOrigin, Vector3.forward, out hit, _rayCastLength))
        {
            _blockedFront = true;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Jamming")) jammedDirections++;
        }
        if (Physics.Raycast(rayOrigin, Vector3.right, out hit, _rayCastLength))
        {
            _blockedRight = true;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Jamming")) jammedDirections++;
        }
        if (Physics.Raycast(rayOrigin, -Vector3.forward, out hit, _rayCastLength))
        {
            _blockedBack = true;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Jamming")) jammedDirections++;
        }
        if (Physics.Raycast(rayOrigin, -Vector3.right, out hit, _rayCastLength))
        {
            _blockedLeft = true;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Jamming")) jammedDirections++;
        }

        if (jammedDirections > 3)
        {
            Die();
            return true;
        }
        return false;
    }

    private void Die()
    {
        _monsterAudioScript.PlayDie();
        _die = true;
        _monsterRbMoveScript.IsStopped(true);
        _monsterRotationScript.SetStopped();
        _animator.SetTrigger("IsDying");
    }

    public void StartSinking()
    {
        _collider.enabled = false;
        _sinking = true;
        Destroy(gameObject, 2f);
    }


    private void ResetTriggers()
    {
        _blockedFront = false;
        _blockedRight = false;
        _blockedBack = false;
        _blockedLeft = false;
    }

    private void CheckForPlayer()
    {
        _distanceFromPlayer = Vector3.Distance(transform.position, _playerScript.transform.position);
        if (_distanceFromPlayer < _playerAttackDistance && !_playerScript.PlayerIsDead() && !_attacking)
        {
            if (!_playerScript.PlayerIsDead()) AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        _monsterAudioScript.PlayAttack();
        _attacking = true;
        _rb.isKinematic = false;
        _animator.SetTrigger("IsAttacking");
    }
    private void MoveForward()
    {
        float yPos = _boxPrefab.localScale.y / 2;
        Transform box = Instantiate(_boxPrefab, new Vector3(Mathf.Round(transform.position.x), yPos, Mathf.Round(transform.position.z) + 1f), Quaternion.identity);
        Physics.IgnoreCollision(box.GetComponent<Collider>(), GetComponent<Collider>());
        _animator.SetTrigger("IsWalking");
        _monsterRotationScript.Forward();
        _monsterRbMoveScript.MoveForward();
    }

    private void MoveRight()
    {
        float yPos = _boxPrefab.localScale.y / 2;
        Transform box = Instantiate(_boxPrefab, new Vector3(Mathf.Round(transform.position.x) + 1f, yPos, Mathf.Round(transform.position.z)), Quaternion.Euler(0, 90, 0));
        Physics.IgnoreCollision(box.GetComponent<Collider>(), GetComponent<Collider>());
        _animator.SetTrigger("IsWalking");
        _monsterRotationScript.TurnRight();
        _monsterRbMoveScript.MoveRight();
    }

    private void MoveBack()
    {
        float yPos = _boxPrefab.localScale.y / 2;
        Transform box = Instantiate(_boxPrefab, new Vector3(Mathf.Round(transform.position.x), yPos, Mathf.Round(transform.position.z) - 1f), Quaternion.Euler(0, 180, 0));
        Physics.IgnoreCollision(box.GetComponent<Collider>(), GetComponent<Collider>());
        _animator.SetTrigger("IsWalking");
        _monsterRotationScript.TurnBack();
        _monsterRbMoveScript.MoveBackward();
    }

    private void MoveLeft()
    {
        float yPos = _boxPrefab.localScale.y / 2;
        Transform box = Instantiate(_boxPrefab, new Vector3(Mathf.Round(transform.position.x) - 1f, yPos, Mathf.Round(transform.position.z)), Quaternion.Euler(0, -90, 0));
        Physics.IgnoreCollision(box.GetComponent<Collider>(), GetComponent<Collider>());
        _animator.SetTrigger("IsWalking");
        _monsterRotationScript.TurnLeft();
        _monsterRbMoveScript.MoveLeft();
    }

    private void CheckKeyControl()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
            MoveForward();

		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
            MoveBack();
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
            MoveRight();
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
				
            MoveLeft();
				
		}
	}

	private void SetAnimator()
	{
        if (!_attacking && !_die) _animator.SetBool("IsWalking", _monsterRbMoveScript.IsMoving());
	}
}

