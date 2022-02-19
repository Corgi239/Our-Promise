using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector3 _grabPoint;


    public void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnMouseDown()
    {
        _isDragging = true;
        _grabPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    public void OnMouseUp()
    {
        _isDragging = false;
    }

    public void OnMouseDrag()
    {
        if (_isDragging)
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - _grabPoint - transform.position;
            transform.Translate(mousePosition);
        }
    }
}
