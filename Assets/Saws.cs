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
        var speed = (float) Math.Sqrt(_timeLiving) * 0.45f;
        speed = Mathf.Min(speed, 199.5f - transform.position.y);
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
