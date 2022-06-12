using System;
using System.Collections;
using System.Collections.Generic;
using GameData.GameDataScripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Gem : Draggable
{
    [SerializeField] private GameData.GameDataScripts.GameData gameData;
    public GemSize size;
    public GemType type;
    public GemCut cut;

    public delegate void DragEndedCallback(Gem gem);
    public DragEndedCallback dragEndedCallback;
    public delegate void DragStartedCallback(Gem gem);
    public DragStartedCallback dragStartedCallback;

    private SpriteRenderer _renderer;
    [CanBeNull] public GemSlot currentSlot;

    public override void Awake()
    {
        base.Awake();
        _renderer = GetComponent<SpriteRenderer>();
        currentSlot = GetComponentInParent<GemSlot>();
    }

    public void Start()
    {
        _renderer.sprite = gameData.Sprite[(type, size, cut)];
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
                effect = gameData.BodyEffect[type];
                break;
            case ArtifactMaterial.Wood:
                effect =  gameData.ReflexEffect[type];
                break;
            case ArtifactMaterial.Bone:
                effect = gameData.MindEffect[type];
                break;
        }
        return effect;
    }
}

public enum GemSize {Small, Medium, Large}
public enum GemType {Blue, Green, Purple, Red, Yellow}
public enum GemCut {Cushion, Oval, Trilliant}
