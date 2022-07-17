using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shelf : Draggable
{
    [SerializeField] private float yRange;
    
    public override void OnMouseDrag()
    {
        if (isDragging)
        {
            float yMovement = mainCamera.ScreenToWorldPoint(Input.mousePosition).y - grabPoint.y - transform.position.y;
            Vector3 newPos = transform.localPosition + new Vector3(0, yMovement, 0);
            newPos.y = Mathf.Clamp(newPos.y, -yRange, 0);
            transform.localPosition = newPos;
        }
    }
}
