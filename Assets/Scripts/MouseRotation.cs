using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1;
    [SerializeField] private Transform followTarget;
    private Vector3 rotation;


    private void Update()
    {
        rotation.x += -Input.GetAxis("Mouse Y") * sensitivity;
        rotation.y += Input.GetAxis("Mouse X") * sensitivity;
        rotation.x = Mathf.Clamp(rotation.x, -30, 30);
    
    
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
        followTarget.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
    }
}
