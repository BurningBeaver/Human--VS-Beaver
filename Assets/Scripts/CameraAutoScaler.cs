using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraAutoScaler : MonoBehaviour
{
    private bool isZoomIn;
    public Transform p1, p2;
    public float scale;
    private Camera _camera;
    public float min, max;


    private void Awake()
    {
        var cameraOrthographicSize = Mathf.Clamp(Math.Max(p1.position.magnitude, p2.position.magnitude) * scale, min, max);
        _camera = Camera.main;
        isZoomIn = true;
        _camera.DOOrthoSize(cameraOrthographicSize, 1.0f).From(max).OnComplete(() =>
        {
            isZoomIn = false;
        });
    }

    private void LateUpdate()
    {
        if(isZoomIn)return;
        var cameraOrthographicSize = Mathf.Clamp(Math.Max(p1.position.magnitude, p2.position.magnitude) * scale, min, max);
        _camera.orthographicSize =
            cameraOrthographicSize;
    }
}