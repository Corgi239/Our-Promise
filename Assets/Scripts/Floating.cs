using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float floatingSpeed = 1f;
    public float floatingAmplitude = 0.1f;
    private Transform _transform;
    private Vector3 _startingPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _startingPosition = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = _startingPosition +
                              new Vector3(0, Mathf.Sin(Time.time * floatingSpeed) * floatingAmplitude, 0);
    }
}
