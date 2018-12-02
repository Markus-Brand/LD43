using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

    public float ParalaxFactor;
    protected Vector3 Offset;
    
    // Start is called before the first frame update
    protected void Start()
    {
        Offset = transform.localPosition;
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.localPosition = -transform.parent.position / ParalaxFactor + Offset;
    }
}
