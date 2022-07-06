using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonWithHighlight : MonoBehaviour
{
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite highlightedSprite;
    [SerializeField] private string soundName;
    public UnityEvent OnPress;
    private SpriteRenderer _renderer;

    public void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        OnPress.AddListener(delegate { AudioManager.instance.PlayRandomSound(soundName); });
    }

    public void OnMouseEnter()
    {
        _renderer.sprite = highlightedSprite;
    }

    public void OnMouseExit()
    {
        _renderer.sprite = baseSprite;
    }

    public void OnMouseUpAsButton()
    {
        OnPress.Invoke();
    }
}
