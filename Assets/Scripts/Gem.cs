using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Gem : Draggable
{
    [SerializeField] private GemData gemData;
    public GemSize size;
    public GemType type;
    public GemCut cut;

    public delegate void DragEndedCallback(Gem gem);
    public DragEndedCallback dragEndedCallback;
    public delegate void DragStartedCallback(Gem gem);
    public DragEndedCallback dragStartedCallback;

    private SpriteRenderer _renderer;
    [CanBeNull] public GemSlot currentSlot;

    public override void Awake()
    {
        base.Awake();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        _renderer.sprite = gemData.Sprite[(type, size, cut)];
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

    public string EffectDescription(ArtifactMaterial material)
    {
        string effect = "Unknown material";
        switch (material) {
            case ArtifactMaterial.Iron:
                effect = gemData.BodyEffect[type];
                break;
            case ArtifactMaterial.Wood:
                effect =  gemData.ReflexEffect[type];
                break;
            case ArtifactMaterial.Bone:
                effect = gemData.MindEffect[type];
                break;
        }
        return effect;
    }
}

public enum GemSize {Small, Medium, Large}
public enum GemType {Blue, Green, Purple, Red, Yellow}
public enum GemCut {Cushion, Oval, Trilliant}
