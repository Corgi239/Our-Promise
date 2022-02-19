using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Draggable
{
    public GemSize size;
    public GemType type;

    public delegate void DragEndedCallback(Gem gem);
    public DragEndedCallback dragEndedCallback;

    public override void OnMouseDown()
    {
        base.OnMouseDown();
        transform.parent = null;
    }
    public override void OnMouseUp()
    {
        base.OnMouseUp();
        dragEndedCallback(this);
    }
}

public enum GemSize {Small, Medium, Large};
public enum GemType {Emerald, Ruby}