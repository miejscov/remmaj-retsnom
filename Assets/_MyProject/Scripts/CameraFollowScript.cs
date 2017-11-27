using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    private GameObject _target;
    private const float Smoothing = 5f;
    private GameObject _entry;
    private GameObject _exit;
    private GameObject _player;

    private Vector3 _defaultCameraPos;
    private Quaternion _defaultRotation;
    private Vector3 _defaultOffset;

    Vector3 _offset;                     

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _defaultCameraPos = transform.position;
        _defaultRotation = transform.rotation;
        
        _target = _player;
        _defaultOffset = transform.position - _target.transform.position - new Vector3(2f, 0f, 1f);
        _offset = _defaultOffset;
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
           SetCameraOnEntry();
        }

        var targetCamPos = _target.transform.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, Smoothing * Time.deltaTime);
    }

    public void SetCameraOnEntry()
    {
        _entry = GameObject.Find("Entrance(Clone)");
        _offset = new Vector3(3, 4, 0);
        _target = _entry;
        transform.rotation = Quaternion.Euler(45, -90, 0);
    }

    public void SetCameraOnExit()
    {
        _exit = GameObject.Find("Exit(Clone)");
        _target = _exit;
        _offset = new Vector3(-3, 4, 0);
        transform.rotation = Quaternion.Euler(45, 90, 0);
    }

    public void ResetCamera()
    {
        _target = _player;
        _offset = _defaultOffset;
        transform.rotation = _defaultRotation;
        transform.position = _defaultCameraPos;
    }
}
