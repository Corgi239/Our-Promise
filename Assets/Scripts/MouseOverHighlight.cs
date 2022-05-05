using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverHighlight : MonoBehaviour
{
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite highlightedSprite;
    private SpriteRenderer _renderer;

    public void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseEnter()
    {
        _renderer.sprite = highlightedSprite;
    }

    public void OnMouseExit()
    {
        _renderer.sprite = baseSprite;
    }
}
