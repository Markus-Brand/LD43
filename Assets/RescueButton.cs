using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RescueButton : MonoBehaviour
{
    public float _totalTime = 1;
    private float _timePressingE;

    public Image Overlay;

    public Dictionary<GameObject, Action> DoneHandler = new Dictionary<GameObject, Action>();
    
    // Update is called once per frame
    void Update()
    {
        var active = DoneHandler.Count > 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(active);
        }
        
        if (active && Input.GetKey(KeyCode.E))
        {
            _timePressingE += Time.deltaTime;
        }
        else
        {
            _timePressingE = 0;
        }

        var progress = _timePressingE / _totalTime;
        Overlay.fillAmount = progress;
        if (progress >= 1)
        {
            foreach (var action in DoneHandler)
            {
                action.Value();
            }
            DoneHandler = new Dictionary<GameObject, Action>();
        }
    }
}
