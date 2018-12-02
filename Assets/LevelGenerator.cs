using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public Saveable Saveable;
    
    public GameObject platform;

    public int height = 100;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < height; i++)
        {
            var newPlatform = Instantiate(platform, transform);
            newPlatform.transform.localPosition = new Vector3(Random.Range(-3.0f, 3.0f), i * 1.5f, 0);
            var saveable = Instantiate(Saveable);
            saveable.transform.position = newPlatform.transform.position + Vector3.up * 0.3f;
        }
    }
}
