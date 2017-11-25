using UnityEngine;

public class MonsterKillPlayerScript : MonoBehaviour {

    private MonsterRbMoveScript _monsterRbMoveScript;
    private MonsterRotationScript _monsterRotationScript;

    private void Start()
    {
        _monsterRbMoveScript = GetComponent<MonsterRbMoveScript>();
        _monsterRotationScript = GetComponent<MonsterRotationScript>();
    }
    private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerStatusScript>().SetPlayerDead(true);
            _monsterRbMoveScript.IsStopped(true);
            _monsterRotationScript.SetStopped();
        }
    }
}
