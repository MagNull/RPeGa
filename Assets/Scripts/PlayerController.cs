using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform start;
    
    [Header("Mouse Look")]
    [SerializeField] private float mouseSens = 1;
    private Camera _camera;
    private float rotationX;
    private float rotationY;
    
    [Header("Player Move")]
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float airSpeed = 0.5f;
    private CharacterController _characterController;
    private Vector3 movement;
    
    [Header("Gravity")]
    [SerializeField] private float gravityForce = -19.62f;
    private Vector3 velocity;

    private void Start()
    {
        _camera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        
        rotationX = _camera.transform.rotation.x;
        rotationY = transform.rotation.y;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        Rotate();
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = start.position;
        }
    }


    private void Rotate()
    {
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        _camera.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
        
        rotationX -= Input.GetAxis("Mouse Y") * mouseSens;
        rotationY += Input.GetAxis("Mouse X") * mouseSens;
        rotationX = Mathf.Clamp(rotationX, -60, 60);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            movement.y = Mathf.Sqrt(jumpHeight * -2f * gravityForce);
        }
    }
    
    private void Move()
    {
        if (_characterController.isGrounded)
        {
            movement = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
            movement = transform.TransformDirection(movement);
        }
        Jump();
        movement.y += gravityForce * Time.deltaTime;
        _characterController.Move(movement * Time.deltaTime);
    }
}
