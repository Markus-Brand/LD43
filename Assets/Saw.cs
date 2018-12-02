using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private float _timeAlive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeAlive += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, _timeAlive * 720f);
    }
}
