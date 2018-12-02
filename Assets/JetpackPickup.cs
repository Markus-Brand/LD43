using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackPickup : MonoBehaviour
{
    private JetpackDisplay JetpackDisplay;
    
    private void Awake()
    {
        JetpackDisplay = GameObject.FindWithTag("JetpackDisplay").GetComponent<JetpackDisplay>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.HasComponent(out Player newPlayer))
        {
            JetpackDisplay.Pickup();
            Destroy(gameObject);
        }
    }
}
