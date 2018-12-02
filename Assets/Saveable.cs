using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saveable : MonoBehaviour
{
    private bool following = false;
    private Player _player;
    public ParticleSystem BloodEffect;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.HasComponent(out Player newPlayer))
        {
            _player = newPlayer;
            if (!following)
            {
                _player.AddSaveable(this);
                following = true;
            }
        }
        else
        {
            if (following)
            {
                _player.RemoveSaveable(this);
            }

            Instantiate(BloodEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
