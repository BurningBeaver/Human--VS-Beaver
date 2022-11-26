using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAutoScaler : MonoBehaviour
{
    public Transform p1, p2;
    public float scale;
    private Camera _camera;
    public float min, max;


    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        _camera.orthographicSize = Mathf.Clamp(Math.Max(p1.position.magnitude, p2.position.magnitude) * scale, min, max);
    }
}