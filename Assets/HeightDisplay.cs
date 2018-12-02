using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeightDisplay : MonoBehaviour
{
    public Player Player;

    private TextMeshProUGUI _text;

    private int maxHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHeight = 0;
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        maxHeight = Math.Max((int) Player.transform.position.y / 2, maxHeight);
        _text.text = "Height: " + maxHeight;
    }
}
