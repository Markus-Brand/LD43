using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JetpackDisplay : MonoBehaviour
{
    private int _numJetpacks;
    public TextMeshProUGUI JetpackText;
    
    // Update is called once per frame
    void Update()
    {
        foreach (var child in transform.GetComponentsInChildren<RectTransform>(true))
        {
            if (child.gameObject != gameObject)
            {
                child.gameObject.SetActive(Ready());
            }
        }

        JetpackText.text = "Jetpack x" + _numJetpacks;
    }

    public void Pickup()
    {
        _numJetpacks++;
    }

    public bool Ready()
    {
        return _numJetpacks > 0;
    }
    
    public void Use()
    {
        _numJetpacks--;
    }
}
