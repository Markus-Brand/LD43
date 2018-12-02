using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{

    private const float TotalTime = 3;
    
    private float _timeLiving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeLiving += Time.deltaTime;
        
        transform.Translate(Vector3.up * Mathf.SmoothStep(0, 15, Math.Min(_timeLiving, TotalTime - _timeLiving)) * Time.deltaTime);
        if (_timeLiving >= TotalTime)
        {
            Destroy(gameObject);
        }
    }
}
