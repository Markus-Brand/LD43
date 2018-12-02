using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saveable : MonoBehaviour
{
    private bool following = false;
    private Player _player;
    public ParticleSystem BloodEffect;
    public ParticleSystem SpecificEffect;
    private bool _saved;

    public GameObject JetPack;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_saved) return;
        if (other.gameObject.HasComponent(out Player newPlayer))
        {
            _player = newPlayer;
            if (!following)
            {
                _player.AddSaveable(this);
                GetComponent<Rigidbody2D>().simulated = false;
                following = true;
            }
        }
        else
        {
            if (following)
            {
                _player.RemoveSaveable(this);
            }

            var saveablePosition = gameObject.transform.position;

            var effectPosition = Camera.main.transform.Find("EffectPosition");
            
            if (effectPosition.transform.position.y > saveablePosition.y)
            {
                var instantiate = Instantiate(BloodEffect, effectPosition);
                instantiate.transform.localPosition = Vector3.zero;
                instantiate.Pause();
                instantiate.Play();
                Instantiate(SpecificEffect, effectPosition).transform.localPosition = Vector3.zero;
            }
            else
            {
                Instantiate(BloodEffect, saveablePosition, Quaternion.identity);
                Instantiate(SpecificEffect, saveablePosition, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        _saved = true;
        var jetpack = Instantiate(JetPack, transform.position, Quaternion.identity);
        transform.parent = jetpack.transform;
    }

    public void LeaveBehind()
    {
        following = false;
        GetComponent<Rigidbody2D>().simulated = true;
    }

    public void SetGroundPosition(Vector3 position)
    {
        transform.position = position - transform.Find("ground").transform.localPosition;
    }
}
