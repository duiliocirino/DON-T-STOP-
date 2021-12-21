using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;
    private Vector3 _initialOffset;
    private Vector3 _cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _initialOffset = transform.position - _player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var playerPosition = _player.position;
        _cameraPosition.x = playerPosition.x + _initialOffset.x;
        _cameraPosition.z = playerPosition.z + _initialOffset.z;
        transform.position = new Vector3(_cameraPosition.x, transform.position.y, _cameraPosition.z);
    }
}
