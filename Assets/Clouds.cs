using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{

    public Cloud Cloud;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            var cloud = Instantiate(Cloud, transform);
            cloud.transform.localPosition =
                new Vector2(Random.Range(-12, 12), Random.Range(-9, 9));
            var distance = Random.Range(-1.0f, 1.0f);
            cloud.ParalaxFactor = 3 + distance;
            cloud.transform.localScale *= (distance + 10) / 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
