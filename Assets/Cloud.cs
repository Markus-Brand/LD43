using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Paralax
{
    
    public Sprite[] Sprites;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length)];
        GetComponent<SpriteRenderer>().sortingOrder = (int)((10 - ParalaxFactor) * 100);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (transform.localPosition.y < -10)
        {
            Offset += Vector3.up * 20;
        }
        if (transform.localPosition.y > 10)
        {
            Offset += Vector3.down * 20;
        }
    }
}
