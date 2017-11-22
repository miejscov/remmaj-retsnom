using UnityEngine;

public class CrateRayCheck : MonoBehaviour {

	private RaycastHit _hit;
	private string _objectTag;
	private bool _canMove;
	private GameObject _hitObject;
	public GameObject CubeCollider;
	private GameObject _tempCubeCollider;
	
	public int CanMove(Vector3 direction, Quaternion rotation)
	{
		_objectTag = RayHit(direction);
		var action = 0;

		if (_objectTag.Equals("Crate") || _objectTag.Equals("Wall") || _objectTag.Equals("Diamond") || _objectTag.Equals("Food") || _objectTag.Equals("Surprise") || _objectTag.Equals("Exit"))
		{
			action = 1; //destroy crate
		}
		else if (_objectTag.Equals("Monster") || _objectTag.Equals("MoveCollider"))
        {
            action = 0; // can't move crate
		}
		else  // crate can move
		{
			action = 2;
			CreateCubeCollider(direction, rotation);
		}
		return action;
	}
	
	private string RayHit(Vector3 direction)
	{
		_objectTag = "Untagged";
		if (Physics.Raycast(transform.position, direction, out _hit, 1f))
		{
			_objectTag = _hit.transform.gameObject.tag;
		}
		return _objectTag;
	}
	
	private void CreateCubeCollider(Vector3 direction, Quaternion rotation)
	{
		if (_tempCubeCollider == null)
		{
			const float yPos = 0.5f;
			_tempCubeCollider = Instantiate(CubeCollider, new Vector3(Mathf.Round(transform.position.x), yPos, Mathf.Round(transform.position.z)) + direction, rotation);
//			Physics.IgnoreCollision(_tempCubeCollider.GetComponent<Collider>(), GetComponentInParent<Collider>());
		}
	}
}