using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Animator transition;
    public float transitionTime = 1f;
    public UnityEvent transitionComplete;
    public UnityEvent transitionStarted;
    private static readonly int Start = Animator.StringToHash("Start");

    public void OnTransitionComplete()
    {
        transitionComplete.Invoke();
    }

    public void ToNextScene(string targetScene)
    {
        transitionStarted.Invoke();
        StartCoroutine(LoadSceneWithTransition(targetScene));
    }
    
    public void Awake()
    {
        transition.speed = 1 / transitionTime;
    }

    private IEnumerator LoadSceneWithTransition(string targetScene)
    {
        transition.SetTrigger(Start);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(targetScene);
    }

    
}
