using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressDisplay : MonoBehaviour
{
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.up * _player.transform.position.y;
    }
}
