using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerExplorationLevel : MonoBehaviour
{
    Vector3 _movementSpeed = new Vector3(5, 5),
        _runningSpeed = new Vector3(9, 9);
    Rigidbody _rigidbody;
    Vector3 _newPosition = new Vector3();
    Animator _animator;
    SpriteRenderer _renderer;
    // These variables are for the enemy:
    bool _isEnemy;
    const float _ENEMYMOVEDISTANCE = 5f, _ENEMYATTACKDISTANCE = 2f, _ENEMYRUNNINGSPEED = 10f;
    GameObject _player;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreLayerCollision(8, 10);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isEnemy = gameObject.tag == "Enemy";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!_isEnemy)
        {
            _newPosition.x = Input.GetAxis("Horizontal") * (Input.GetButton("Fire3") ? _runningSpeed.x : _movementSpeed.x);
            _newPosition.y = Input.GetAxis("Vertical") * (Input.GetButton("Fire3") ? _runningSpeed.y : _movementSpeed.y);

            _animator.SetFloat("speed", _newPosition.magnitude);

            //_rigidbody.MovePosition(transform.position + _newPosition * Time.deltaTime);
            _rigidbody.velocity = _newPosition;

            _animator.SetBool("attack", Input.GetButton("Fire1"));

            _renderer.flipX = _newPosition.x < 0;
        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < _ENEMYATTACKDISTANCE)
            {
                _animator.SetBool("attack", true);
            }
            else
                _animator.SetBool("attack", false);

            if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < _ENEMYMOVEDISTANCE)
            {
                // Move towards the player.
                _newPosition = Vector3.MoveTowards(gameObject.transform.position, _player.transform.position, _ENEMYRUNNINGSPEED * Time.deltaTime);
                
                _rigidbody.MovePosition(_newPosition);

                _renderer.flipX = _newPosition.x < transform.position.x;

                _animator.SetFloat("speed", _ENEMYRUNNINGSPEED);
            }
            else
                _animator.SetFloat("speed", 0);
        }
    }
}
