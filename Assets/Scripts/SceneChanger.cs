using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Animator transition;
    public float transitionTime = 1f;
    private static readonly int Start = Animator.StringToHash("Start");

    public void ToNextScene(string targetScene)
    {
        transition.speed = 1 / transitionTime;
        StartCoroutine(LoadSceneWithTransition(targetScene));
    }

    private IEnumerator LoadSceneWithTransition(string targetScene)
    {
        transition.SetTrigger(Start);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(targetScene);
    }
}
