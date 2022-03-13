using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Gem : Draggable
{
    public GemSize size;
    public GemType type;

    public delegate void DragEndedCallback(Gem gem);
    public DragEndedCallback dragEndedCallback;
    public delegate void DragStartedCallback(Gem gem);
    public DragEndedCallback dragStartedCallback;

    private Renderer _renderer;
    private static Dictionary<GemType, Color> _colors;
    [CanBeNull] public GemSlot currentSlot;

    public override void Awake()
    {
        base.Awake();
        _renderer = GetComponent<Renderer>();
        _colors = new Dictionary<GemType, Color>
        {
            {GemType.Emerald, new Color32(137, 246, 143, 255)},
            {GemType.Ruby, new Color32(195,50,80, 255)},
            {GemType.Saphire, new Color32(90, 90, 220, 255)}
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
        dragStartedCallback(this);
    }
    public override void OnMouseUp()
    {
        base.OnMouseUp();
        dragEndedCallback(this);
    }

    public override string ToString()
    {
        return $"{size} {type}";
    }

    public string EffectDescription()
    {
        //TODO: Add actual effect descriptions
        return $"effect of {type}";
    }
}

public enum GemSize {Small, Medium, Large}
public enum GemType {Emerald, Ruby, Saphire}
