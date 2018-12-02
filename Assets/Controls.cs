using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    private bool wPressed;
    private bool aPressed;
    private bool dPressed;

    private float _timeFading;

    // Update is called once per frame
    void Update()
    {
        wPressed = wPressed || Input.GetKey(KeyCode.W); 
        aPressed = aPressed || Input.GetKey(KeyCode.A); 
        dPressed = dPressed || Input.GetKey(KeyCode.D); 
        if (wPressed && aPressed && dPressed)
        {
            _timeFading += Time.deltaTime;
            Color color = new Color(1, 1, 1, 1 - _timeFading);
            GetComponent<Image>().color = color;
        }
    }
}
