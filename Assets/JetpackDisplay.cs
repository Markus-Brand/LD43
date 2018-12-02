using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JetpackDisplay : MonoBehaviour
{
    public float _totalTime = 1;
    private float _timeWaiting;

    public Image Overlay;

    public TextMeshProUGUI ContolsText;
    
    // Update is called once per frame
    void Update()
    {
        _timeWaiting += Time.deltaTime;
        
        Overlay.fillAmount = Progress();
        ContolsText.gameObject.SetActive(Ready());
    }

    private float Progress()
    {
        return _timeWaiting / _totalTime;
    }

    public bool Ready()
    {
        return Progress() > 1;
    }
    
    public void Use()
    {
        _timeWaiting = 0;
    }
}
