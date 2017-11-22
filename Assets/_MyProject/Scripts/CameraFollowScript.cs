using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    private GameObject target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.
    private GameObject _entry;
    private GameObject _exit;
    private GameObject _player;
    

    private Vector3 _defaultCameraPos;
    private Quaternion _defaultRotation;
    private Vector3 _defaultOffset;
    private Transform thisTransform;

    Vector3 offset;                     // The initial offset from the target.


    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        _defaultCameraPos = this.transform.position;
        _defaultRotation = this.transform.rotation;
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _entry = GameObject.Find("Entrance(Clone)");
        _exit = GameObject.Find("Exit(Clone)");
        
        target = _player;
        _defaultOffset = transform.position - target.transform.position;
        offset = _defaultOffset;
        Debug.Log("def off" + _defaultOffset);
    }

    private void FixedUpdate()
    {

        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.transform.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    public void SetCameraOnEntry()
    {
        offset = new Vector3(3, 4, -1);
        target = _entry;
        transform.rotation = Quaternion.Euler(45, -90, 0);
    }

    public void SetCameraOnExit()
    {
        target = _exit;
    }

    public void ResetCamera()
    {
        target = _player;
        offset = _defaultOffset;
        transform.rotation = _defaultRotation;
        transform.position = _defaultCameraPos;
    }
    
    
}
