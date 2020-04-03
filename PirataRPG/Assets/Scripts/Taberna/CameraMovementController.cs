using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    GameObject _player;
    Vector3 _newPosition;
    const float _UPPERLIMIT = 2.25f, _LOWERLIMIT = -2.25f;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        _newPosition = gameObject.transform.position;
    }

    void Update()
    {
        _newPosition.y = Mathf.Clamp(_player.transform.position.y, _LOWERLIMIT, _UPPERLIMIT);
        transform.position = _newPosition;
    }
}
