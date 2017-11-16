using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveColliderTriggerScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.parent.gameObject);
    }
}
