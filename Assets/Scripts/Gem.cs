using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Draggable
{
    public GemSize size;
    public GemType type;

    public delegate void DragEndedCallback(Gem gem);
    public DragEndedCallback dragEndedCallback;

    private Renderer _renderer;
    private static Dictionary<GemType, Color> _colors;

    public override void Awake()
    {
        base.Awake();
        _renderer = GetComponent<Renderer>();
        _colors = new Dictionary<GemType, Color>
        {
            {GemType.Emerald, new Color32(137, 246, 143, 255)},
            {GemType.Ruby, new Color32(155,17,30, 255)}
        };
    }

    public void Start()
    {
        Color color = _colors[type];
        _renderer.material.color = color;
    }

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
