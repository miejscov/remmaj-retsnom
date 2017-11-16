using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTriggerFrontScript : MonoBehaviour {

    private GameObject _parent;

    private void Start()
    {
        _parent = transform.parent.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Colliding with" + other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Jamming")) ;
        {
            //            Debug.Log("Monster Front collision with a Wall.");
            _parent.SendMessage("SetTrigger", "blockedFront");

        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("Trigger Colliding with" + other.gameObject.name);
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Jamming")) ;
    //    {
    //        //            Debug.Log("Monster Front collision with a Wall.");
    //        _parent.SendMessage("SetTrigger", "blockedFront");

    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Trigger Exiting Collision with" + other.gameObject.name);
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Jamming"))
    //    {
    //        //           Debug.Log("Monster Exiting Front collision with a Wall.");
    //        _parent.SendMessage("SetTrigger", "freeFront");
    //    }
    //}
}
