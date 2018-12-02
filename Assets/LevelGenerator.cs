using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{

    public Saveable Saveable;
    public JetpackPickup JetpackPickup;
    
    public GameObject platform;
    public MovingPlatform MovingPlatform;
    public GameObject longPlatform;

    public Player Player;

    public int height = 100;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < height; i++)
        {
            float random = Random.value;

            GameObject newPlatform;
            if (random < 0.2 && i != 0)
            {
                random = 1000;
                var movingPlatform = Instantiate(MovingPlatform, transform);
                float random1 = Random.Range(-3.0f, -1.0f);
                movingPlatform.MinX = random1;
                movingPlatform.MinX = -random1;
                movingPlatform.transform.localPosition = new Vector3(random1, i * 2f, 0);
                newPlatform = movingPlatform.gameObject;
            }
            else
            {
                newPlatform = Instantiate(platform, transform);
                newPlatform.transform.localPosition = new Vector3(Random.Range(-3.0f, 3.0f), i * 2f, 0);
            }
            if (i == 0)
            {
                Player.transform.position = newPlatform.transform.position + Vector3.up * 1f;
            }
            else
            {
                if (random < 0.375)
                {
                    var saveable = Instantiate(Saveable);
                    saveable.transform.position = newPlatform.transform.position + Vector3.up * 0.4f;
                } else if (random < 0.575)
                {
                    var jetpackPickup = Instantiate(JetpackPickup);
                    jetpackPickup.transform.position = newPlatform.transform.position + Vector3.up * 0.4f;
                }
            }
        }

        var endPlatform = Instantiate(longPlatform, transform);
        endPlatform.transform.localPosition = new Vector3(0, height * 2f, 0);
    }
}
