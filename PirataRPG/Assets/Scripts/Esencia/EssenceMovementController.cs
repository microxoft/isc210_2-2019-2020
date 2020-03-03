using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceMovementController : MonoBehaviour
{
    float _XAcceleration = -9.8f;
    float _XCurrentSpeed = 0;
    Vector3 _deltaPos;

    private void Awake()
    {
        _deltaPos = new Vector3();
    }

    void Update()
    {
        _deltaPos.x = _XCurrentSpeed * Time.deltaTime + (_XAcceleration * Mathf.Pow(Time.deltaTime, 2))/2;
        gameObject.transform.Translate(_deltaPos);

        _XCurrentSpeed += _XAcceleration * Time.deltaTime;
    }
}
