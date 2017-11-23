﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public GameObject target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.
    private GameObject _entry;
    private GameObject _exit;
    private GameObject _player;

    private Vector3 _defaultCameraPos;
    private Quaternion _defaultRotation;
    private Vector3 _defaultOffset;

    Vector3 offset;                     // The initial offset from the target.

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _defaultCameraPos = transform.position;
        _defaultRotation = transform.rotation;
        
        
        target = _player;
        _defaultOffset = transform.position - target.transform.position;
        offset = _defaultOffset;
        Debug.Log("def off" + _defaultOffset);
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
           SetCameraOnEntry();
        }

        Vector3 targetCamPos = target.transform.position + offset;
//        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetCamPos, 4f);
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
