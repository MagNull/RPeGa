using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravityForce = -9.81f;
    
    [Inject]
    private InputHandler inputHandler;
    
    private CharacterController _characterController;
    private Vector3 movement = Vector3.zero;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        inputHandler.OnMove += Move;
    }

    private void OnDisable()
    {
        inputHandler.OnMove -= Move;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            movement.y = Mathf.Sqrt(jumpHeight * -2f * gravityForce);
        }
    }

    private void Move(float speed)
    {
        if (_characterController.isGrounded)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0,
                Input.GetAxis("Vertical"));
            if (movement.magnitude > 1) movement.Normalize();
            movement = transform.TransformDirection(movement * speed);
        }

        movement.y += gravityForce * Time.deltaTime;
        Jump();
        _characterController.Move(movement * Time.deltaTime);
    }
}
