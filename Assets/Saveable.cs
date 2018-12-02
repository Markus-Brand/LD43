using UnityEngine;

public class Saveable : MonoBehaviour
{
    private bool following = false;
    private Player _player;
    public ParticleSystem BloodEffect;
    public ParticleSystem SpecificEffect;
    private bool _saved;

    public GameObject JetPack;

    private RescueButton _button;

    private void Start()
    {
        _button = GameObject.FindWithTag("RescueButton").GetComponent<RescueButton>();
        GameObject.FindWithTag("Player").GetComponent<Player>().RegisterSaveable(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_saved) return;
        if (other.gameObject.HasComponent(out Player newPlayer))
        {
            _player = newPlayer;
            if (!following)
            {
                _button.DoneHandler[gameObject] = () =>
                {
                    _player.AddSaveable(this);
                    GetComponent<Rigidbody2D>().simulated = false;
                    following = true;
                };
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
                var blood = Instantiate(BloodEffect, effectPosition);
                blood.transform.localPosition = Vector3.zero;
                var specific = Instantiate(SpecificEffect, effectPosition);
                specific.transform.localPosition = Vector3.zero;
            }
            else
            {
                Instantiate(BloodEffect, saveablePosition, Quaternion.identity);
                Instantiate(SpecificEffect, saveablePosition, Quaternion.identity);
            }
            
            GameObject.FindWithTag("Player").GetComponent<Player>().NumKilled++;
            GameObject.FindWithTag("Player").GetComponent<Player>().UnRegisterSaveable(this);
            Camera.main.GetComponent<FollowingCamera>().MakeUndirectedShake(0.7f, 0.6f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.HasComponent(out Player newPlayer))
        {
            _button.DoneHandler.Remove(gameObject);
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
