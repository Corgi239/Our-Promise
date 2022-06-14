using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionWatcher : MonoBehaviour
{
    private SceneChanger _changer;
    // Start is called before the first frame update
    void Start()
    {
        _changer = GetComponentInParent<SceneChanger>();
    }

    public void OnTransitionComplete()
    {
        _changer.OnTransitionComplete();
    }
}
