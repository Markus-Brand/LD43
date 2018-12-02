using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private bool airbound;
    private bool _startedJumping;

    private List<Saveable> _saveables;
    private List<Vector3> _trail;
    public int NumSaved;

    private const int FramesDelayPerSaveable = 20;
    
    public GameObject JetPack;
    private GameObject _myJetpack;
    private bool _flying;
    public ParticleSystem LandedParticles;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        airbound = true;
        _trail = new List<Vector3>();
        _trail.Add(transform.position);
        _saveables = new List<Saveable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_myJetpack)
        {
            GetComponent<Rigidbody2D>().simulated = true;
            transform.position = _myJetpack.transform.position + Vector3.up * 0.6f;
        }
        else if(!GetComponent<Collider2D>().enabled)
        {
            var velocity = GetComponent<Rigidbody2D>().velocity;
            velocity.x = 0;
            GetComponent<Rigidbody2D>().velocity = velocity;
            GetComponent<Collider2D>().enabled = true;
        }
        
        UpdateTrail();

        UpdateSaveables();

        UpdateAnimation();

        UpdateJump();
    }

    private void UpdateSaveables()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _saveables.Count > 0)
        {
            var saved = _saveables[0];
            saved.Save();
            NumSaved++;
            _saveables.Remove(saved);
        } 
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _myJetpack = Instantiate(JetPack, transform.position, Quaternion.identity);
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<Collider2D>().enabled = false;
            foreach (var savable in _saveables)
            {
                savable.LeaveBehind();
            }
            _saveables = new List<Saveable>();
        }
        for (int i = 0; i < _saveables.Count; i++)
        {
            var trailIndex = _trail.Count - (int)((i + 0.5f) * FramesDelayPerSaveable) - 1;
            if(trailIndex >= 0)
            {
                _saveables[i].SetGroundPosition(_trail[trailIndex]);
            }
            else
            {
                _saveables[i].SetGroundPosition(_trail[0]);
            }
        }
    }

    private void UpdateJump()
    {
        if (_startedJumping && _animator.GetCurrentAnimatorStateInfo(0).IsName("rising"))
        {
            _startedJumping = false;
            _rigidbody2D.AddForce(new Vector2(0, 500));
            airbound = true;
        }

        if (WantsToJump() && !airbound && !_startedJumping && _rigidbody2D.velocity.y <= 0.01f)
        {
            _startedJumping = true;
            _animator.SetTrigger("jump");
        }
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("yVelocity", _rigidbody2D.velocity.y);
        var velocityX = _rigidbody2D.velocity.x;
        _animator.SetFloat("AbsXVelocity", Mathf.Abs(velocityX));
        if (Mathf.Abs(velocityX) > 0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = velocityX < 0;
        }

        _rigidbody2D.AddForce(new Vector2(15 * xInput(), 0));
    }

    private void UpdateTrail()
    {
        _trail.Add(transform.Find("ground").position);
        if (_trail.Count > FramesDelayPerSaveable * (_saveables.Count + 1))
        {
            _trail.RemoveAt(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.HasComponent<Saveable>())
        {
            return;
        }
        if (other.contacts.Any(contact => contact.normal.y > 0.8))
        {
            _animator.SetTrigger("landed");
            Instantiate(LandedParticles, other.contacts[0].point, Quaternion.identity);
            Instantiate(LandedParticles, other.contacts[0].point, Quaternion.Euler(0, 0, -125));
            airbound = false;
            _startedJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.HasComponent<Saveable>())
        {
            return;
        }
        airbound = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.HasComponent<Saveable>())
        {
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool WantsToJump()
    {
        return Input.GetKey(KeyCode.W);
    }
    
    private int xInput()
    {
        var movement = 0;
        if (Input.GetKey(KeyCode.A))
        {
            movement--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement++;
        }
        return movement;
    }

    public void AddSaveable(Saveable saveable)
    {
        _saveables.Add(saveable);
    }

    public void RemoveSaveable(Saveable saveable)
    {
        _saveables.Remove(saveable);
    }
}
