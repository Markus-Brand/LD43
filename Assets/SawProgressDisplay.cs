using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawProgressDisplay : MonoBehaviour
{
    private GameObject _saws;

    // Start is called before the first frame update
    void Start()
    {
        _saws = GameObject.FindWithTag("Saws");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.up * _saws.transform.position.y;
    }
}
