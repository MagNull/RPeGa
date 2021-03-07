using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 1;
    [SerializeField] private Transform _followTarget;
    private Vector3 _rotation;


    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            _rotation.x += -Input.GetAxis("Mouse Y") * _sensitivity;
            _rotation.y += Input.GetAxis("Mouse X") * _sensitivity;
            _rotation.x = Mathf.Clamp(_rotation.x, -30, 30);    

            transform.rotation = Quaternion.Euler(0, _rotation.y, 0);
            _followTarget.rotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);
        }
    }
}
