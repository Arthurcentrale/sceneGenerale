﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float cameraFollowSpeed = 1.0f;

    private Vector3 cameraOffset;

    void Start()
    {
        SetCamera();
    }

    void SetCamera()
    {
        transform.LookAt(target.position);
        cameraOffset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetCamPos = target.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, cameraFollowSpeed * Time.deltaTime);
    }
}