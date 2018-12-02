using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingPlatform : MonoBehaviour
{
    public float MinX;
    public float MaxX;

    private float _timeLiving;

    private void Start()
    {
        _timeLiving = Random.Range(0.0f, 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _timeLiving += Time.deltaTime;
        var transformPosition = transform.position;
        transformPosition.x = (float) (MinX + (((Math.Sin(_timeLiving) + 1) / 2) * (MaxX - MinX)));
        transform.position = transformPosition;
    }
}
