using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeButton : MonoBehaviour
{
    [SerializeField] private SceneChanger changer;
    [SerializeField] private string targetScene;
    public void OnMouseUpAsButton()
    {
        changer.ToNextScene(targetScene);
    }
}
