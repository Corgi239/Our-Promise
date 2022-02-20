using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector3 _grabPoint;


    public virtual void Awake()
    {
        _mainCamera = Camera.main;
    }

    public virtual void OnMouseDown()
    {
        _isDragging = true;
        _grabPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    public virtual void OnMouseUp()
    {
        _isDragging = false;
    }

    public virtual void OnMouseDrag()
    {
        if (_isDragging)
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - _grabPoint - transform.position;
            transform.Translate(mousePosition);
        }
    }
}
