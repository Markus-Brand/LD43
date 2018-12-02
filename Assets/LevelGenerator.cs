using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public Saveable Saveable;
    public JetpackPickup JetpackPickup;
    
    public GameObject platform;
    public GameObject longPlatform;

    public Player Player;

    public int height = 100;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < height; i++)
        {
            var newPlatform = Instantiate(platform, transform);
            newPlatform.transform.localPosition = new Vector3(Random.Range(-3.0f, 3.0f), i * 2f, 0);
            if (i == 0)
            {
                Player.transform.position = newPlatform.transform.position + Vector3.up * 1f;
            }
            else
            {
                float random = Random.value;

                if (random < 0.2)
                {
                    var saveable = Instantiate(Saveable);
                    saveable.transform.position = newPlatform.transform.position + Vector3.up * 0.4f;
                } else if (random < 0.4)
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
