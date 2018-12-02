using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saws : MonoBehaviour
{
    private float _timeLiving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeLiving += Time.deltaTime;
        transform.Translate(Vector3.up * (float) Math.Sqrt(_timeLiving) * Time.deltaTime * 0.5f);
    }
}
