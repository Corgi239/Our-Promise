using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    protected Camera mainCamera;
    protected bool isDragging;
    protected Vector3 grabPoint;


    public virtual void Awake()
    {
        mainCamera = Camera.main;
    }

    public virtual void OnMouseDown()
    {
        isDragging = true;
        grabPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    public virtual void OnMouseUp()
    {
        isDragging = false;
    }

    public virtual void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - grabPoint - transform.position;
            transform.Translate(mousePosition);
        }
    }
}
