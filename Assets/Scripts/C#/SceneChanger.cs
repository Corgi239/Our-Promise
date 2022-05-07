using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private String targetSceneName;

    public void OnMouseUpAsButton()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
