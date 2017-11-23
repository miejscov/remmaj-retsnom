using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public GameObject target;            
    public float smoothing = 5f;        
    private GameObject _entry;
    private GameObject _exit;
    private GameObject _player;

    private Vector3 _defaultCameraPos;
    private Quaternion _defaultRotation;
    private Vector3 _defaultOffset;

    Vector3 offset;                     

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _defaultCameraPos = transform.position;
        _defaultRotation = transform.rotation;
        
        
        target = _player;
        _defaultOffset = transform.position - target.transform.position;
        offset = _defaultOffset;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
           SetCameraOnEntry();
        }

        Vector3 targetCamPos = target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    public void SetCameraOnEntry()
    {
        _entry = GameObject.Find("Entrance(Clone)");
        offset = new Vector3(3, 4, -1);
        target = _entry;
        transform.rotation = Quaternion.Euler(45, -90, 0);
    }

    public void SetCameraOnExit()
    {
        _exit = GameObject.Find("Exit(Clone)");
        target = _exit;
        offset = new Vector3(-3, 4, -1);
        transform.rotation = Quaternion.Euler(45, 90, 0);
    }

    public void ResetCamera()
    {
        target = _player;
        offset = _defaultOffset;
        transform.rotation = _defaultRotation;
        transform.position = _defaultCameraPos;
    }
}
